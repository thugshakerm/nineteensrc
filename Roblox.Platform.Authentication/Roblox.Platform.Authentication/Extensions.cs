using Roblox.Passwords.Client;
using Roblox.Platform.Membership;

namespace Roblox.Platform.Authentication;

public static class Extensions
{
	internal static void VerifyIsNotNull(this ISessionToken sessionToken)
	{
		if (sessionToken == null)
		{
			throw new InvalidSessionTokenException();
		}
	}

	public static void DeleteAllSessions(this IUser user)
	{
		user.VerifyIsNotNull();
		SessionTokenFactory.DeleteAll(user);
	}

	/// <summary>
	/// This method tells us whether this user has setup a password for their Roblox Account or not
	/// </summary>
	/// <param name="authenticatedUser">Authenticated Roblox User object</param>
	/// <returns>A boolean that tells whether the user has set a password or not</returns>
	public static bool HasValidPasswordSet(this IUser authenticatedUser)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Invalid comparison between Unknown and I4
		PasswordStatusResult passwordStatus = AccountPasswordHash.PasswordsClient.GetPasswordStatus((PasswordOwnerType)1, authenticatedUser.AccountId);
		if ((int)passwordStatus.SetStatus != 1)
		{
			return (int)passwordStatus.SetStatus == 3;
		}
		return true;
	}

	public static void InvalidateAllAccountSecurityTickets(this IUser user)
	{
		user.VerifyIsNotNull();
		AccountSecurityTicket.InvalidateAllAccountSecurityTickets(Account.Get(user.AccountId));
	}
}
