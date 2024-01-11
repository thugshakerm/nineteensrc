using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roblox.Hashing;
using Roblox.Instrumentation;
using StackExchange.Redis;

namespace Roblox.Redis;

public class RedisClient : RedisClientBase<RedisClientOptions>
{
	private IConnectionMultiplexer[] _Multiplexers;

	private ConsistentHash<ConsistentHashConnectionWrapperBase> _NodeProvider;

	public RedisClient(ICounterRegistry counterRegistry, IEnumerable<string> redisEndpoints, string performanceMonitorCategory, Action<Exception> exceptionHandler = null, RedisClientOptions redisClientOptions = null)
		: base(counterRegistry, performanceMonitorCategory, redisClientOptions ?? new RedisClientOptions(), exceptionHandler)
	{
		if (redisEndpoints == null)
		{
			throw new ArgumentNullException("redisEndpoints");
		}
		ChangeMultiplexers(Enumerable.ToArray(redisEndpoints));
	}

	public RedisClient(ICounterRegistry counterRegistry, RedisEndpoints redisEndpoints, string performanceMonitorCategory, Action<Exception> exceptionHandler = null, RedisClientOptions redisClientOptions = null)
		: this(counterRegistry, redisEndpoints?.Endpoints, performanceMonitorCategory, exceptionHandler, redisClientOptions)
	{
	}

	public override IDatabase GetDatabase(string partitionKey)
	{
		if (_Multiplexers.Length == 1)
		{
			return _Multiplexers[0].GetDatabase();
		}
		return _NodeProvider.GetNode(partitionKey).Database;
	}

	public override IServer GetServer(string partitionKey)
	{
		if (_Multiplexers.Length == 1)
		{
			return RedisClientBase<RedisClientOptions>.GetServerFromMultiplexer(_Multiplexers[0]);
		}
		return _NodeProvider.GetNode(partitionKey).Server;
	}

	public override ISubscriber GetSubscriber(string partitionKey)
	{
		if (_Multiplexers.Length == 1)
		{
			return _Multiplexers[0].GetSubscriber();
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
		return Enumerable.ToList(Enumerable.Select(_Multiplexers, (IConnectionMultiplexer d) => d.GetDatabase()));
	}

	public override IReadOnlyCollection<ISubscriber> GetAllSubscribers()
	{
		return Enumerable.ToList(Enumerable.Select(_Multiplexers, (IConnectionMultiplexer m) => m.GetSubscriber()));
	}

	public override IReadOnlyCollection<IServer> GetAllServers()
	{
		return Enumerable.ToList(Enumerable.Select(_Multiplexers, RedisClientBase<RedisClientOptions>.GetServerFromMultiplexer));
	}

	public override void Refresh(string[] redisEndpoints)
	{
		ChangeMultiplexers(redisEndpoints);
		OnRefreshed();
	}

	public override void Close()
	{
		Parallel.ForEach(_Multiplexers, delegate(IConnectionMultiplexer m)
		{
			m.Close(allowCommandsToComplete: false);
		});
	}

	private void ChangeMultiplexers(string[] redisEndpoints)
	{
		IConnectionMultiplexer[] oldPools = _Multiplexers ?? Array.Empty<ConnectionMultiplexer>();
		IConnectionMultiplexer[] array = Enumerable.ToArray(Enumerable.Where(oldPools, (IConnectionMultiplexer o) => Enumerable.Any(redisEndpoints, (string re) => re == GetMultiplexerDescriptor(o))));
		IEnumerable<IConnectionMultiplexer> enumerable = Enumerable.Except(oldPools, array);
		Task<IConnectionMultiplexer>[] array2 = Enumerable.ToArray(Enumerable.Select(Enumerable.Where(redisEndpoints, (string re) => Enumerable.All(oldPools, (IConnectionMultiplexer o) => GetMultiplexerDescriptor(o) != re)), base.ConnectMultiplexerAsync));
		Task[] tasks = array2;
		Task.WaitAll(tasks);
		_Multiplexers = Enumerable.ToArray(Enumerable.Concat(array, Enumerable.Select(array2, (Task<IConnectionMultiplexer> m) => m.Result)));
		List<RedisConnectionWrapper> nodes = Enumerable.ToList(Enumerable.Select(_Multiplexers, (IConnectionMultiplexer cm) => new RedisConnectionWrapper(cm, GetConfigurationFromMultiplexer(cm))));
		ConsistentHash<ConsistentHashConnectionWrapperBase> consistentHash = new ConsistentHash<ConsistentHashConnectionWrapperBase>();
		consistentHash.Init(nodes);
		Interlocked.Exchange(ref _NodeProvider, consistentHash);
		foreach (IConnectionMultiplexer item in enumerable)
		{
			item.Dispose();
		}
		static string GetMultiplexerDescriptor(IConnectionMultiplexer cm)
		{
			return cm.GetEndPoints()[0].ToString();
		}
	}

	private static ConfigurationOptions GetConfigurationFromMultiplexer(IConnectionMultiplexer multiplexer)
	{
		return ConfigurationOptions.Parse(multiplexer.Configuration);
	}
}
