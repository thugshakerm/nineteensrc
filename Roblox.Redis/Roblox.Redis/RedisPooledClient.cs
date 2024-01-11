using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roblox.Hashing;
using Roblox.Instrumentation;
using Roblox.Redis.Properties;
using StackExchange.Redis;

namespace Roblox.Redis;

public class RedisPooledClient : RedisClientBase<RedisPooledClientOptions>
{
	private RedisConnectionMultiplexerPool[] _Pools;

	private ConsistentHash<ConsistentHashConnectionWrapperBase> _NodeProvider;

	private readonly ParallelOptions _ParallelOptions = new ParallelOptions
	{
		MaxDegreeOfParallelism = 10
	};

	private readonly ISelfHealingConnectionMultiplexerSettings _BadStateSelfHealingConnectionMultiplexerSettings;

	private readonly Func<DateTime> _GetCurrentTimeFunc;

	public RedisPooledClient(ICounterRegistry counterRegistry, IEnumerable<string> redisEndpoints, string performanceMonitorCategory, Action<Exception> exceptionHandler = null, RedisPooledClientOptions clientOptions = null, ISelfHealingConnectionMultiplexerSettings badStateSelfHealingConnectionMultiplexerSettings = null, Func<DateTime> getCurrentTimeFunc = null)
		: base(counterRegistry, performanceMonitorCategory, clientOptions ?? new RedisPooledClientOptions(), exceptionHandler)
	{
		_BadStateSelfHealingConnectionMultiplexerSettings = badStateSelfHealingConnectionMultiplexerSettings ?? SelfHealingConnectionMultiplexerSettings.Default;
		_GetCurrentTimeFunc = getCurrentTimeFunc ?? ((Func<DateTime>)(() => DateTime.UtcNow));
		ChangePools(Enumerable.ToArray(redisEndpoints));
	}

	public RedisPooledClient(ICounterRegistry counterRegistry, RedisEndpoints redisEndpoints, string performanceMonitorCategory, Action<Exception> exceptionHandler = null, RedisPooledClientOptions clientOptions = null, ISelfHealingConnectionMultiplexerSettings badStateSelfHealingConnectionMultiplexerSettings = null, Func<DateTime> getCurrentTimeFunc = null)
		: this(counterRegistry, redisEndpoints?.Endpoints, performanceMonitorCategory, exceptionHandler, clientOptions, badStateSelfHealingConnectionMultiplexerSettings, getCurrentTimeFunc)
	{
	}

	public override IDatabase GetDatabase(string partitionKey)
	{
		if (_Pools.Length == 1)
		{
			return _Pools[0].GetConnectionMultiplexer().GetDatabase();
		}
		return _NodeProvider.GetNode(partitionKey).Database;
	}

	public override IServer GetServer(string partitionKey)
	{
		if (_Pools.Length == 1)
		{
			return RedisClientBase<RedisPooledClientOptions>.GetServerFromMultiplexer(_Pools[0].GetConnectionMultiplexer());
		}
		return _NodeProvider.GetNode(partitionKey).Server;
	}

	public override ISubscriber GetSubscriber(string partitionKey)
	{
		if (_Pools.Length == 1)
		{
			return _Pools[0].Subscriber;
		}
		return _NodeProvider.GetNode(partitionKey).Subscriber;
	}

	public override IDictionary<IDatabase, IReadOnlyCollection<string>> GetDatabases(IReadOnlyCollection<string> partitionKeys)
	{
		if (partitionKeys == null || Enumerable.Any(partitionKeys, (string pk) => pk == null))
		{
			throw new ArgumentNullException("partitionKeys");
		}
		return ConsistentHashConnectionWrapperBase.GetDatabasesByConsistentHashingAlgorithm(partitionKeys, _NodeProvider);
	}

	public override IReadOnlyCollection<IDatabase> GetAllDatabases()
	{
		return Enumerable.ToList(Enumerable.Select(_Pools, (RedisConnectionMultiplexerPool p) => p.GetConnectionMultiplexer().GetDatabase()));
	}

	public override IReadOnlyCollection<ISubscriber> GetAllSubscribers()
	{
		return Enumerable.ToList(Enumerable.Select(_Pools, (RedisConnectionMultiplexerPool p) => p.Subscriber));
	}

	public override IReadOnlyCollection<IServer> GetAllServers()
	{
		return Enumerable.ToList(Enumerable.Select(_Pools, (RedisConnectionMultiplexerPool p) => RedisClientBase<RedisPooledClientOptions>.GetServerFromMultiplexer(p.GetConnectionMultiplexer())));
	}

	public override void Refresh(string[] redisEndpoints)
	{
		ChangePools(redisEndpoints);
		OnRefreshed();
	}

	public override void Close()
	{
		Parallel.ForEach(_Pools, _ParallelOptions, delegate(RedisConnectionMultiplexerPool p)
		{
			p.Close();
		});
	}

	private void ChangePools(string[] redisEndpoints)
	{
		RedisConnectionMultiplexerPool[] array = _Pools ?? Array.Empty<RedisConnectionMultiplexerPool>();
		RedisConnectionMultiplexerPool[] keepingOldPools = Enumerable.ToArray(Enumerable.Where(array, (RedisConnectionMultiplexerPool op) => Enumerable.Contains(redisEndpoints, GetMultiplexerDescriptor(op.PrimaryConnection))));
		IEnumerable<RedisConnectionMultiplexerPool> enumerable = Enumerable.Except(array, keepingOldPools);
		Task<RedisConnectionMultiplexerPool>[] array2 = Enumerable.ToArray(Enumerable.Select(Enumerable.Where(redisEndpoints, (string re) => Enumerable.All(keepingOldPools, (RedisConnectionMultiplexerPool kop) => kop.PrimaryConnection.GetEndPoints()[0].ToString() != re)), (string endpoint) => CreatePoolAsync(endpoint, base.RedisClientOptions)));
		Task[] tasks = array2;
		Task.WaitAll(tasks);
		_Pools = Enumerable.ToArray(Enumerable.Concat(keepingOldPools, Enumerable.Select(array2, (Task<RedisConnectionMultiplexerPool> t) => t.Result)));
		List<RedisConnectionMultiplexerPoolWrapper> nodes = Enumerable.ToList(Enumerable.Select(_Pools, (RedisConnectionMultiplexerPool p) => new RedisConnectionMultiplexerPoolWrapper(p, p.BaseConfiguration)));
		ConsistentHash<ConsistentHashConnectionWrapperBase> consistentHash = new ConsistentHash<ConsistentHashConnectionWrapperBase>();
		consistentHash.Init(nodes);
		Interlocked.Exchange(ref _NodeProvider, consistentHash);
		foreach (RedisConnectionMultiplexerPool item in enumerable)
		{
			item.Dispose();
		}
		static string GetMultiplexerDescriptor(IConnectionMultiplexer cm)
		{
			return cm.GetEndPoints()[0].ToString();
		}
	}

	private async Task<RedisConnectionMultiplexerPool> CreatePoolAsync(string redisEndpoint, RedisPooledClientOptions clientOptions)
	{
		ConfigurationOptions configurationOptions = GetConfigurationOptions(redisEndpoint);
		RedisConnectionMultiplexerPool pool = new RedisConnectionMultiplexerPool(configurationOptions, clientOptions);
		await pool.ConnectAsync().ConfigureAwait(continueOnCapturedContext: false);
		return pool;
	}
}
