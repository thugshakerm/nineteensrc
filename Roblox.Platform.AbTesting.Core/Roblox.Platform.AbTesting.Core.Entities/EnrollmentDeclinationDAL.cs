using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Roblox.Common;
using Roblox.Entities.Mssql;
using Roblox.MssqlDatabases;

namespace Roblox.Platform.AbTesting.Core.Entities;

/// <summary>
/// This class contains methods not generated by the dbwireuptool. If someone wants to regenerate the entity files, we want these to not be overwritten.
/// </summary>
internal class EnrollmentDeclinationDAL
{
	private const RobloxDatabase _Database = RobloxDatabase.RobloxAbTesting;

	internal long ID { get; set; }

	internal int SubjectTypeID { get; set; }

	internal long SubjectTargetID { get; set; }

	internal int VersionID { get; set; }

	internal int DeclinationReasonTypeID { get; set; }

	internal DateTime Created { get; set; }

	internal static EntityHelper.GetOrCreateDALWrapper<EnrollmentDeclinationDAL> GetOrCreateEnrollmentDeclination(int subjectTypeID, long subjectTargetID, int versionID, int declinationReasonTypeID)
	{
		if (subjectTypeID == 0)
		{
			return null;
		}
		if (subjectTargetID == 0L)
		{
			return null;
		}
		if (versionID == 0)
		{
			return null;
		}
		if (declinationReasonTypeID == 0)
		{
			return null;
		}
		SqlParameter[] queryParameters = new SqlParameter[5]
		{
			new SqlParameter("@CreatedNewEntity", SqlDbType.Bit)
			{
				Direction = ParameterDirection.Output
			},
			new SqlParameter("@SubjectTypeID", subjectTypeID),
			new SqlParameter("@SubjectTargetID", subjectTargetID),
			new SqlParameter("@VersionID", versionID),
			new SqlParameter("@DeclinationReasonTypeID", declinationReasonTypeID)
		};
		return RobloxDatabase.RobloxAbTesting.GetOrCreate("EnrollmentDeclinations_GetOrCreateEnrollmentDeclination", BuildDAL, queryParameters);
	}

	private static EnrollmentDeclinationDAL BuildDAL(IDictionary<string, object> record)
	{
		return new EnrollmentDeclinationDAL
		{
			ID = (long)record["ID"],
			SubjectTypeID = (int)record["SubjectTypeID"],
			SubjectTargetID = (long)record["SubjectTargetID"],
			VersionID = (int)record["VersionID"],
			DeclinationReasonTypeID = (int)record["DeclinationReasonTypeID"],
			Created = (DateTime)record["Created"]
		};
	}

	internal void Delete()
	{
		RobloxDatabase.RobloxAbTesting.Delete("EnrollmentDeclinations_DeleteEnrollmentDeclinationByID", ID);
	}

	internal void Insert()
	{
		SqlParameter[] queryParameters = new SqlParameter[6]
		{
			new SqlParameter("@ID", SqlDbType.BigInt)
			{
				Direction = ParameterDirection.Output
			},
			new SqlParameter("@SubjectTypeID", SubjectTypeID),
			new SqlParameter("@SubjectTargetID", SubjectTargetID),
			new SqlParameter("@VersionID", VersionID),
			new SqlParameter("@DeclinationReasonTypeID", DeclinationReasonTypeID),
			new SqlParameter("@Created", Created)
		};
		ID = RobloxDatabase.RobloxAbTesting.Insert<long>("EnrollmentDeclinations_InsertEnrollmentDeclination", queryParameters);
	}

	internal void Update()
	{
		SqlParameter[] queryParameters = new SqlParameter[6]
		{
			new SqlParameter("@ID", ID),
			new SqlParameter("@SubjectTypeID", SubjectTypeID),
			new SqlParameter("@SubjectTargetID", SubjectTargetID),
			new SqlParameter("@VersionID", VersionID),
			new SqlParameter("@DeclinationReasonTypeID", DeclinationReasonTypeID),
			new SqlParameter("@Created", Created)
		};
		RobloxDatabase.RobloxAbTesting.Update("EnrollmentDeclinations_UpdateEnrollmentDeclinationByID", queryParameters);
	}

	internal static EnrollmentDeclinationDAL Get(long id)
	{
		return RobloxDatabase.RobloxAbTesting.Get("EnrollmentDeclinations_GetEnrollmentDeclinationByID", id, BuildDAL);
	}

	internal static ICollection<EnrollmentDeclinationDAL> MultiGet(ICollection<long> ids)
	{
		return RobloxDatabase.RobloxAbTesting.MultiGet("EnrollmentDeclinations_GetEnrollmentDeclinationsByIDs", ids, BuildDAL);
	}

	internal static EnrollmentDeclinationDAL GetEnrollmentDeclinationBySubjectTypeIDSubjectTargetIDAndVersionID(int subjectTypeID, long subjectTargetID, int versionID)
	{
		if (subjectTypeID == 0)
		{
			return null;
		}
		if (subjectTargetID == 0L)
		{
			return null;
		}
		if (versionID == 0)
		{
			return null;
		}
		SqlParameter[] queryParameters = new SqlParameter[3]
		{
			new SqlParameter("@SubjectTypeID", subjectTypeID),
			new SqlParameter("@SubjectTargetID", subjectTargetID),
			new SqlParameter("@VersionID", versionID)
		};
		return RobloxDatabase.RobloxAbTesting.Lookup("EnrollmentDeclinations_GetEnrollmentDeclinationBySubjectTypeIDSubjectTargetIDAndVersionID", BuildDAL, queryParameters);
	}
}
