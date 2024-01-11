using System;
using Roblox.FloodCheckers.Core;
using Roblox.Passwords.Client;
using Roblox.Platform.Authentication.Properties;
using Roblox.Platform.Membership;

namespace Roblox.Platform.Authentication;

internal class RobloxCredentials : CredentialsBase, IRobloxCredentials, ICredentials
{
	internal readonly IUser _User;

	internal readonly string _Password;

	private readonly IPasswordsClient _PasswordsClient;

	private readonly IAuthenticationSettings _Settings;

	internal RobloxCredentials(IUser user, string password, IUserFactory userFactory, IPasswordsClient passwordsClient, IAuthenticationSettings settings)
		: base(userFactory)
	{
		_User = user ?? throw new ArgumentNullException("user");
		_Password = password;
		_PasswordsClient = passwordsClient ?? throw new ArgumentNullException("passwordsClient");
		_Settings = settings ?? throw new ArgumentNullException("settings");
	}

	protected override IUser DoAuthentication()
	{
		IFloodChecker floodChecker = GetFailedLoginFloodChecker(_User);
		if (floodChecker.IsFlooded())
		{
			throw new LoginFailureLimitExceededException(_User.Name);
		}
		if (string.IsNullOrWhiteSpace(_Password))
		{
			floodChecker.UpdateCount();
			return null;
		}
		if (!Verify())
		{
			floodChecker.UpdateCount();
			return null;
		}
		return _User;
	}

	public bool Verify()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Invalid comparison between Unknown and I4
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Invalid comparison between Unknown and I4
		VerifyPasswordResult verifyPasswordResult = _PasswordsClient.VerifyPassword((PasswordOwnerType)1, _User.AccountId, _Password);
		if ((int)verifyPasswordResult != 2)
		{
			return (int)verifyPasswordResult == 3;
		}
		return true;
	}

	internal virtual IFloodChecker GetFailedLoginFloodChecker(IUser user)
	{
		return new GlobalFailedLoginFloodChecker(user.Name);
	}
}
