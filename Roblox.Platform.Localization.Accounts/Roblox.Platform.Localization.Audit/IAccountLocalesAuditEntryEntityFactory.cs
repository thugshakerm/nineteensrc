using System;
using System.Collections.Generic;
using Roblox.Platform.Localization.Accounts;

namespace Roblox.Platform.Localization.Audit;

internal interface IAccountLocalesAuditEntryEntityFactory
{
	/// <summary>
	/// Gets an <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" /> by its ID.
	/// </summary>
	/// <param name="id">The ID.</param>
	/// <returns>The <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" /> with the given ID, or null if none existed.</returns>
	IAccountLocalesAuditEntryEntity GetById(long id);

	/// <summary>
	/// Gets the <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" /> with the given PublicId.
	/// </summary>
	/// <param name="publicId">The PublicId.</param>
	/// <returns>The <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" /> with the given PublicId, or null if none existed.</returns>
	IAccountLocalesAuditEntryEntity GetByPublicId(Guid publicId);

	/// <summary>
	/// Gets individual <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" /> by thier PublicIds and returns them in a <see cref="T:System.Collections.Generic.IReadOnlyCollection`1" />.
	/// </summary>
	/// <param name="publicIds">The publicIds.</param>
	/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> of <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" />.</returns>
	IReadOnlyCollection<IAccountLocalesAuditEntryEntity> MultiGetByPublicIds(IEnumerable<Guid> publicIds);

	/// <summary>
	/// Gets a page of <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" />s by their AuditId.
	/// </summary>
	/// <param name="auditId">The AuditId.</param>
	/// <param name="count">The number of entities to get.</param>
	/// <param name="exclusiveStartId">The exclusive key at which to begin getting entities.</param>
	/// <exception cref="T:Roblox.Platform.Core.PlatformArgumentException">Thrown if <paramref name="count" /> is non-positive.</exception>
	/// <returns>The page of <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" />s.</returns>
	IReadOnlyCollection<IAccountLocalesAuditEntryEntity> MultiGetByAuditId(long auditId, int count, long? exclusiveStartId = null);

	/// <summary>
	/// Creates a new audit entry record based on the given <see cref="T:Roblox.Platform.Localization.Accounts.IAccountLocaleEntity" />.
	/// </summary>
	/// <param name="entity">The entity to base the audit on.</param>
	/// <returns>The newly created <see cref="T:Roblox.Platform.Localization.Audit.IAccountLocalesAuditEntryEntity" />.</returns>
	IAccountLocalesAuditEntryEntity Create(IAccountLocaleEntity entity);
}
