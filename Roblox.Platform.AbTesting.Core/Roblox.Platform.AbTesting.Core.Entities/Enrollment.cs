using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Roblox.Caching;
using Roblox.Caching.Interfaces;
using Roblox.Common;
using Roblox.Data.Interfaces;
using Roblox.Platform.AbTesting.Core.Properties;

namespace Roblox.Platform.AbTesting.Core.Entities;

/// <summary>
/// This class contains methods not generated by the dbwireuptool. If someone wants to regenerate the entity files, we want these to not be overwritten.
/// </summary>
[ExcludeFromCodeCoverage]
internal class Enrollment : IRobloxEntity<long, EnrollmentDAL>, ICacheableObject<long>, ICacheableObject, IRemoteCacheableObject
{
	private EnrollmentDAL _EntityDAL;

	public static CacheInfo EntityCacheInfo = new CacheInfo(new CacheabilitySettings(collectionsAreCacheable: false, countsAreCacheable: false, entityIsCacheable: true, idLookupsAreCacheable: true, hasUnqualifiedCollections: false), typeof(Enrollment).ToString(), isNullCacheable: true);

	public long ID => _EntityDAL.ID;

	internal int SubjectTypeID
	{
		get
		{
			return _EntityDAL.SubjectTypeID;
		}
		set
		{
			_EntityDAL.SubjectTypeID = value;
		}
	}

	internal long SubjectTargetID
	{
		get
		{
			return _EntityDAL.SubjectTargetID;
		}
		set
		{
			_EntityDAL.SubjectTargetID = value;
		}
	}

	internal int VersionID
	{
		get
		{
			return _EntityDAL.VersionID;
		}
		set
		{
			_EntityDAL.VersionID = value;
		}
	}

	internal int VariationID
	{
		get
		{
			return _EntityDAL.VariationID;
		}
		set
		{
			_EntityDAL.VariationID = value;
		}
	}

	internal DateTime Created
	{
		get
		{
			return _EntityDAL.Created;
		}
		set
		{
			_EntityDAL.Created = value;
		}
	}

	internal DateTime Updated
	{
		get
		{
			return _EntityDAL.Updated;
		}
		set
		{
			_EntityDAL.Updated = value;
		}
	}

	public CacheInfo CacheInfo => EntityCacheInfo;

	/// <summary>
	/// Gets or creates the <see cref="T:Roblox.Platform.AbTesting.Core.IEnrollment" /> with the given, version ID, subject type ID, subject target ID, and variation ID.
	/// </summary>
	/// <param name="versionId">The ID of the <see cref="T:Roblox.Platform.AbTesting.Core.IVersion" />.</param>
	/// <param name="subjectTypeId">The type ID of the <see cref="T:Roblox.Platform.AbTesting.Core.ISubject" />.</param>
	/// <param name="subjectTargetId">The target ID of the <see cref="T:Roblox.Platform.AbTesting.Core.ISubject" />.</param>
	/// <param name="variationId">The ID of the <see cref="T:Roblox.Platform.AbTesting.Core.IVariation" /></param>
	/// <param name="created">Indicates if a new enrollment was created</param>
	/// <returns>The <see cref="T:Roblox.Platform.AbTesting.Core.IEnrollment" /> with the given version ID, subject type ID, subject target ID, and variation ID.</returns>
	public static Enrollment GetOrCreate(int subjectTypeId, long subjectTargetId, int versionId, int variationId, out bool created)
	{
		bool wasCreated = false;
		if (Settings.Default.IsEnrollmentRemoteCacheEnabled)
		{
			Enrollment orCreateEntityWithRemoteCache = EntityHelper.GetOrCreateEntityWithRemoteCache<long, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeId, subjectTargetId, versionId), () => EntityHelper.DoGetOrCreate<long, EnrollmentDAL, Enrollment>(delegate
			{
				EntityHelper.GetOrCreateDALWrapper<EnrollmentDAL> orCreateEnrollment2 = EnrollmentDAL.GetOrCreateEnrollment(subjectTypeId, subjectTargetId, versionId, variationId);
				wasCreated = orCreateEnrollment2.CreatedNewEntity;
				return orCreateEnrollment2;
			}), Get);
			created = wasCreated;
			return orCreateEntityWithRemoteCache;
		}
		Enrollment orCreateEntity = EntityHelper.GetOrCreateEntity<long, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeId, subjectTargetId, versionId), () => EntityHelper.DoGetOrCreate<long, EnrollmentDAL, Enrollment>(delegate
		{
			EntityHelper.GetOrCreateDALWrapper<EnrollmentDAL> orCreateEnrollment = EnrollmentDAL.GetOrCreateEnrollment(subjectTypeId, subjectTargetId, versionId, variationId);
			wasCreated = orCreateEnrollment.CreatedNewEntity;
			return orCreateEnrollment;
		}));
		created = wasCreated;
		return orCreateEntity;
	}

	public Enrollment()
	{
		_EntityDAL = new EnrollmentDAL();
	}

	public Enrollment(EnrollmentDAL enrollmentDAL)
	{
		_EntityDAL = enrollmentDAL;
	}

	public object GetSerializable()
	{
		return _EntityDAL;
	}

	internal void Delete()
	{
		if (Settings.Default.IsEnrollmentRemoteCacheEnabled)
		{
			EntityHelper.DeleteEntityWithRemoteCache(this, _EntityDAL.Delete);
		}
		else
		{
			EntityHelper.DeleteEntity(this, _EntityDAL.Delete);
		}
	}

	internal void Save()
	{
		if (Settings.Default.IsEnrollmentRemoteCacheEnabled)
		{
			EntityHelper.SaveEntityWithRemoteCache(this, delegate
			{
				_EntityDAL.Created = DateTime.Now;
				_EntityDAL.Updated = _EntityDAL.Created;
				_EntityDAL.Insert();
			}, delegate
			{
				_EntityDAL.Updated = DateTime.Now;
				_EntityDAL.Update();
			});
		}
		else
		{
			EntityHelper.SaveEntity(this, delegate
			{
				_EntityDAL.Created = DateTime.Now;
				_EntityDAL.Updated = _EntityDAL.Created;
				_EntityDAL.Insert();
			}, delegate
			{
				_EntityDAL.Updated = DateTime.Now;
				_EntityDAL.Update();
			});
		}
	}

	internal static Enrollment Get(long id)
	{
		return EntityHelper.GetEntity<long, EnrollmentDAL, Enrollment>(EntityCacheInfo, id, () => EnrollmentDAL.Get(id));
	}

	internal static ICollection<Enrollment> MultiGet(ICollection<long> ids)
	{
		return EntityHelper.MultiGetEntity<long, EnrollmentDAL, Enrollment>(ids, EntityCacheInfo, EnrollmentDAL.MultiGet);
	}

	public static Enrollment GetOrCreate(int subjectTypeId, long subjectTargetId, int versionId, int variationId)
	{
		if (Settings.Default.IsEnrollmentRemoteCacheEnabled)
		{
			return EntityHelper.GetOrCreateEntityWithRemoteCache<long, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeId, subjectTargetId, versionId), () => DoGetOrCreate(subjectTypeId, subjectTargetId, versionId, variationId), Get);
		}
		return EntityHelper.GetOrCreateEntity<long, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeId, subjectTargetId, versionId), () => DoGetOrCreate(subjectTypeId, subjectTargetId, versionId, variationId));
	}

	private static Enrollment DoGetOrCreate(int subjectTypeId, long subjectTargetId, int versionId, int variationId)
	{
		return EntityHelper.DoGetOrCreate<long, EnrollmentDAL, Enrollment>(() => EnrollmentDAL.GetOrCreateEnrollment(subjectTypeId, subjectTargetId, versionId, variationId));
	}

	public static Enrollment GetBySubjectTypeIDSubjectTargetIDAndVersionID(int subjectTypeID, long subjectTargetID, int versionID)
	{
		if (Settings.Default.IsEnrollmentRemoteCacheEnabled)
		{
			return EntityHelper.GetEntityByLookupWithRemoteCache<long, EnrollmentDAL, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeID, subjectTargetID, versionID), () => EnrollmentDAL.GetEnrollmentBySubjectTypeIDSubjectTargetIDAndVersionID(subjectTypeID, subjectTargetID, versionID), Get);
		}
		return EntityHelper.GetEntityByLookup<long, EnrollmentDAL, Enrollment>(EntityCacheInfo, GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(subjectTypeID, subjectTargetID, versionID), () => EnrollmentDAL.GetEnrollmentBySubjectTypeIDSubjectTargetIDAndVersionID(subjectTypeID, subjectTargetID, versionID));
	}

	public void Construct(EnrollmentDAL dal)
	{
		_EntityDAL = dal;
	}

	public IEnumerable<string> BuildEntityIDLookups()
	{
		yield return GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(SubjectTypeID, SubjectTargetID, VersionID);
	}

	public IEnumerable<StateToken> BuildStateTokenCollection()
	{
		yield break;
	}

	private static string GetLookupCacheKeysBySubjectTypeIDSubjectTargetIDVersionID(int subjectTypeId, long subjectTargetId, int versionId)
	{
		return $"SubjectTypeID:{subjectTypeId}_SubjectTargetID:{subjectTargetId}_VersionID:{versionId}";
	}
}
