using System;
using System.Diagnostics.CodeAnalysis;
using Roblox.Entities;

namespace Roblox.Platform.Localization.Audit;

[ExcludeFromCodeCoverage]
internal class AccountLocalesChangeAgentTypeCachedMssqlEntity : IAccountLocalesChangeAgentTypeEntity, IUpdateableEntity<byte>, IEntity<byte>
{
	public byte Id { get; set; }

	public string Value { get; set; }

	public string Description { get; set; }

	public DateTime Created { get; set; }

	public DateTime Updated { get; set; }

	public void Update()
	{
		AccountLocalesChangeAgentTypeCAL cal = AccountLocalesChangeAgentTypeCAL.Get(Id);
		if (cal == null)
		{
			throw new InvalidOperationException("Attempted update on unpersisted entity.");
		}
		cal.Value = Value;
		cal.Description = Description;
		cal.Save();
		Updated = cal.Updated;
	}

	public void Delete()
	{
		(AccountLocalesChangeAgentTypeCAL.Get(Id) ?? throw new InvalidOperationException("Attempted delete on unpersisted entity.")).Delete();
	}
}