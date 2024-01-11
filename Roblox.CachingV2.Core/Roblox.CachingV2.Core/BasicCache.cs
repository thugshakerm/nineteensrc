using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roblox.CachingV2.Core;

public sealed class BasicCache : CacheBase<BasicMetadata, BasicSetArgs>
{
	public BasicCache(IRawCache rawCache, string name)
		: base(rawCache, name)
	{
	}

	protected override MetadataCacheGetResult<TValue, BasicMetadata> DoGet<TValue>(string key)
	{
		return ConvertToMetadataResult(base.RawCache.Get<TValue>(key));
	}

	protected override async Task<MetadataCacheGetResult<TValue, BasicMetadata>> DoGetAsync<TValue>(string key, CancellationToken cancellationToken)
	{
		return ConvertToMetadataResult(await base.RawCache.GetAsync<TValue>(key, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
	}

	protected override SetResult<BasicMetadata> DoSet<TValue>(SetEntry<TValue, BasicSetArgs> entry)
	{
		NullChecker.ThrowIfNull(entry, "entry");
		RawSetEntry<TValue> entry2 = new RawSetEntry<TValue>(entry.Key, entry.Value, entry.SetArgs.Expiration);
		base.RawCache.Set(entry2);
		return new SetResult<BasicMetadata>(entry.Key, new BasicMetadata());
	}

	protected override async Task<SetResult<BasicMetadata>> DoSetAsync<TValue>(SetEntry<TValue, BasicSetArgs> entry, CancellationToken cancellationToken)
	{
		NullChecker.ThrowIfNull(entry, "entry");
		RawSetEntry<TValue> entry2 = new RawSetEntry<TValue>(entry.Key, entry.Value, entry.SetArgs.Expiration);
		await base.RawCache.SetAsync(entry2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		return new SetResult<BasicMetadata>(entry.Key, new BasicMetadata());
	}

	protected override IEnumerable<MetadataCacheGetResult<TValue, BasicMetadata>> DoMultiGet<TValue>(IReadOnlyCollection<string> keys)
	{
		return Enumerable.Select(base.RawCache.MultiGet<TValue>(keys), ConvertToMetadataResult);
	}

	protected override async Task<IEnumerable<MetadataCacheGetResult<TValue, BasicMetadata>>> DoMultiGetAsync<TValue>(IReadOnlyCollection<string> keys, CancellationToken cancellationToken)
	{
		return Enumerable.Select(await base.RawCache.MultiGetAsync<TValue>(keys, cancellationToken).ConfigureAwait(continueOnCapturedContext: false), ConvertToMetadataResult);
	}

	protected override IEnumerable<SetResult<BasicMetadata>> DoMultiSet<TValue>(IReadOnlyCollection<SetEntry<TValue, BasicSetArgs>> entries)
	{
		if (entries == null || Enumerable.Any(entries, (SetEntry<TValue, BasicSetArgs> e) => e == null))
		{
			throw new ArgumentNullException("entries");
		}
		RawSetEntry<TValue>[] entries2 = Enumerable.ToArray(Enumerable.Select(entries, (SetEntry<TValue, BasicSetArgs> e) => new RawSetEntry<TValue>(e.Key, e.Value, e.SetArgs.Expiration)));
		base.RawCache.MultiSet((IReadOnlyCollection<RawSetEntry<TValue>>)(object)entries2);
		return Enumerable.Select(entries, (SetEntry<TValue, BasicSetArgs> e) => new SetResult<BasicMetadata>(e.Key, new BasicMetadata()));
	}

	protected override async Task<IEnumerable<SetResult<BasicMetadata>>> DoMultiSetAsync<TValue>(IReadOnlyCollection<SetEntry<TValue, BasicSetArgs>> entries, CancellationToken cancellationToken)
	{
		if (entries == null || Enumerable.Any(entries, (SetEntry<TValue, BasicSetArgs> e) => e == null))
		{
			throw new ArgumentNullException("entries");
		}
		RawSetEntry<TValue>[] entries2 = Enumerable.ToArray(Enumerable.Select(entries, (SetEntry<TValue, BasicSetArgs> e) => new RawSetEntry<TValue>(e.Key, e.Value, e.SetArgs.Expiration)));
		await base.RawCache.MultiSetAsync((IReadOnlyCollection<RawSetEntry<TValue>>)(object)entries2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		return Enumerable.Select(entries, (SetEntry<TValue, BasicSetArgs> e) => new SetResult<BasicMetadata>(e.Key, new BasicMetadata()));
	}

	private static MetadataCacheGetResult<TValue, BasicMetadata> ConvertToMetadataResult<TValue>(CacheGetResult<TValue> result)
	{
		return new MetadataCacheGetResult<TValue, BasicMetadata>(result.Key, result.Entry, result.IsFound, result.IsFound ? new BasicMetadata() : null);
	}
}
