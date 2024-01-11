using System;

namespace Roblox.Platform.Throttling;

internal class Namespace : INamespace
{
	public long Id { get; private set; }

	public string Value { get; private set; }

	public DateTime Created { get; private set; }

	public DateTime Updated { get; private set; }

	public Namespace(long id, string value, DateTime created, DateTime updated)
	{
		Updated = updated;
		Created = created;
		Value = value;
		Id = id;
	}
}
