using Roblox.TextFilter;
using Roblox.TextFilter.Client;

namespace Roblox.Platform.Membership.Extensions;

/// <summary>
/// Extension methods for <see cref="T:Roblox.Platform.Membership.IUser" />.
/// </summary>
public static class UserExtensions
{
	/// <summary>
	/// Creates a <see cref="T:Roblox.TextFilter.Client.IClientTextAuthor" /> from an <see cref="T:Roblox.Platform.Membership.IUser" /> for text filter request using the TextFilter client.
	/// </summary>
	/// <param name="user">The <see cref="T:Roblox.Platform.Membership.IUser" /> that creates the text.</param>
	/// <returns><see cref="T:Roblox.TextFilter.Client.IClientTextAuthor" />.</returns>
	public static IClientTextAuthor ToClientTextAuthor(this IUser user)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (user != null)
		{
			return (IClientTextAuthor)new TextAuthor
			{
				Id = user.Id,
				Name = user.Name,
				IsUnder13 = (user.AgeBracket != AgeBracket.Age13OrOver)
			};
		}
		return TextAuthor.UnknownTextAuthor();
	}

	/// <summary>
	/// Creates a <see cref="T:Roblox.TextFilter.ITextAuthor" /> from an <see cref="T:Roblox.Platform.Membership.IUser" /> for text filtering request.
	/// </summary>
	/// <param name="user">The <see cref="T:Roblox.Platform.Membership.IUser" /> that creates the text.</param>
	/// <returns><see cref="T:Roblox.TextFilter.ITextAuthor" />.</returns>
	public static ITextAuthor ToTextAuthor(this IUser user)
	{
		if (user != null)
		{
			return new TextAuthor(user.Id, user.Name, user.AgeBracket != AgeBracket.Age13OrOver);
		}
		return new UnknownTextAuthor();
	}

	/// <summary>
	/// Creates a <see cref="T:Roblox.TextFilter.ITextRecipient" /> from an <see cref="T:Roblox.Platform.Membership.IUser" /> for text filtering request.
	/// </summary>
	/// <param name="user">The <see cref="T:Roblox.Platform.Membership.IUser" /> who will be receiving the text.</param>
	/// <returns><see cref="T:Roblox.TextFilter.ITextRecipient" />.</returns>
	public static ITextRecipient ToTextRecipient(this IUser user)
	{
		if (user != null)
		{
			return new TextRecipient(user.AgeBracket != AgeBracket.Age13OrOver);
		}
		return new UnknownTextRecipient();
	}
}
