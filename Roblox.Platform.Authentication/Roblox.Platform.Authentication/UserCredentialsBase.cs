using System;
using Roblox.Passwords.Client;
using Roblox.Platform.Authentication.Properties;
using Roblox.Platform.Membership;

namespace Roblox.Platform.Authentication;

/// <summary>
/// A base implementation for all user credentials classes.
/// </summary>
internal abstract class UserCredentialsBase : CredentialsBase, IRobloxUserCredentials, IRobloxCredentials, ICredentials
{
	private readonly IPasswordsClient _PasswordsClient;

	private readonly IAuthenticationSettings _Settings;

	public abstract CredentialsType CredentialsType { get; }

	public abstract string CredentialValue { get; }

	protected IUser User { get; set; }

	protected UserCredentialsBase(IUserFactory userFactory, IPasswordsClient passwordsClient, IAuthenticationSettings settings)
		: base(userFactory)
	{
		_PasswordsClient = passwordsClient ?? throw new ArgumentNullException("passwordsClient");
		_Settings = settings ?? throw new ArgumentNullException("settings");
	}

	/// <summary>
	/// Gets the <see cref="T:Roblox.Platform.Membership.IUser" /> for the current credentials.
	/// </summary>
	/// <returns></returns>
	public abstract IUser GetUser();

	/// <summary>
	/// Verifies the current credentials.
	/// </summary>
	/// <returns></returns>
	public abstract bool Verify();

	protected override IUser DoAuthentication()
	{
		IUser user = User ?? GetUser();
		if (user == null)
		{
			return null;
		}
		if (!Verify())
		{
			return null;
		}
		return user;
	}

	internal virtual bool IsValidPasswordForUser(IUser user, string password)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Invalid comparison between Unknown and I4
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		if (user == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(password))
		{
			return false;
		}
		VerifyPasswordResult verifyPasswordResult = _PasswordsClient.VerifyPassword((PasswordOwnerType)1, user.AccountId, password);
		if ((int)verifyPasswordResult != 2)
		{
			return (int)verifyPasswordResult == 3;
		}
		return true;
	}
}
