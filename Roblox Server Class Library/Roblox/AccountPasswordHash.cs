using System;
using Roblox.Instrumentation;
using Roblox.Passwords.Client;
using Roblox.Properties;
using Roblox.RequestContext;

namespace Roblox;

[Serializable]
[Obsolete("Please use IPasswordsClient instead.")]
public class AccountPasswordHash
{
	public static IPasswordsClient PasswordsClient = (IPasswordsClient)new PasswordsClient(StaticCounterRegistry.Instance, (Func<string>)(() => Settings.Default.PasswordsClientMasterApiKey), (IRequestContextLoader)null);

	public long ID { get; }

	public long AccountID { get; }

	private AccountPasswordHash(long id, long accountId)
	{
		ID = id;
		AccountID = accountId;
	}

	internal static void CreateNew(long accountId, string newPassword)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		PasswordsClient.SetPassword((PasswordOwnerType)1, accountId, newPassword);
	}

	public static AccountPasswordHash GetCurrent(long accountId)
	{
		if (accountId <= 0)
		{
			return null;
		}
		PasswordStatusResult passwordStatus = PasswordsClient.GetPasswordStatus((PasswordOwnerType)1, accountId);
		if (passwordStatus.Id.HasValue)
		{
			return new AccountPasswordHash(passwordStatus.Id.Value, accountId);
		}
		return null;
	}
}
