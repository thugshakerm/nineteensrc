using System;
using System.Diagnostics.CodeAnalysis;
using Roblox.Entities;

namespace Roblox.Platform.UniverseSettings;

[ExcludeFromCodeCoverage]
internal class UniverseAvatarAnimationTypeCachedMssqlEntity : IUniverseAvatarAnimationTypeEntity, IUpdateableEntity<byte>, IEntity<byte>
{
	public byte Id { get; set; }

	public string Value { get; set; }

	public DateTime Created { get; set; }

	public DateTime Updated { get; set; }

	public void Update()
	{
		UniverseAvatarAnimationTypeEntity cal = UniverseAvatarAnimationTypeEntity.Get(Id);
		if (cal == null)
		{
			throw new InvalidOperationException("Attempted update on unpersisted entity.");
		}
		cal.Value = Value;
		cal.Save();
		Updated = cal.Updated;
	}

	public void Delete()
	{
		(UniverseAvatarAnimationTypeEntity.Get(Id) ?? throw new InvalidOperationException("Attempted delete on unpersisted entity.")).Delete();
	}
}
