using System;

namespace Roblox.Platform.Authentication;

/// <summary>
/// A common interface for a Facebook account.
/// </summary>
public interface IFacebookAccount : IFacebookAccountIdentifier
{
	/// <summary>
	/// The account id linked to this Facebook account.
	/// </summary>
	long AccountId { get; }

	/// <summary>
	/// The Facebook id for this Facebook account.
	/// </summary>
	ulong FacebookId { get; }

	/// <summary>
	/// The date this Facebook account was created.
	/// </summary>
	/// <remarks>
	/// TODO: Delete once HasAutogeneratedPassword has been removed
	/// </remarks>
	DateTime Created { get; }
}
