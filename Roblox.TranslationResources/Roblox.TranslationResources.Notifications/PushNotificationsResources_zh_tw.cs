namespace Roblox.TranslationResources.Notifications;

/// <summary>
/// This class overrides PushNotificationsResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class PushNotificationsResources_zh_tw : PushNotificationsResources_en_us, IPushNotificationsResources, ITranslationResources
{
	/// <summary>
	/// Key: "Message.AddedToPrivateServerWhiteList.Default"
	/// You have a been invited to a VIP server!
	/// English String: "You have a been invited to a VIP server!"
	/// </summary>
	public override string MessageAddedToPrivateServerWhiteListDefault => "您已受邀到 VIP 伺服器！";

	/// <summary>
	/// Key: "Message.DefaultSystemMessage.Body"
	/// New activity on Roblox!
	/// English String: "New activity on Roblox!"
	/// </summary>
	public override string MessageDefaultSystemMessageBody => "Roblox 有新活動！";

	/// <summary>
	/// Key: "Message.DefaultSystemMessage.Title"
	/// Roblox
	/// English String: "Roblox"
	/// </summary>
	public override string MessageDefaultSystemMessageTitle => "Roblox";

	/// <summary>
	/// Key: "Message.FriendRequestAccepted.Default"
	/// Your friend request has been accepted!
	/// English String: "Your friend request has been accepted!"
	/// </summary>
	public override string MessageFriendRequestAcceptedDefault => "您的好友邀請已被接受！";

	/// <summary>
	/// Key: "Message.FriendRequestReceived.Default"
	/// You have a new friend request!
	/// English String: "You have a new friend request!"
	/// </summary>
	public override string MessageFriendRequestReceivedDefault => "您有新的好友邀請！";

	/// <summary>
	/// Key: "Message.NewChatMessage.Default"
	/// You have a new chat message.
	/// English String: "You have a new chat message."
	/// </summary>
	public override string MessageNewChatMessageDefault => "您有新的聊天訊息。";

	/// <summary>
	/// Key: "Message.PartyInvitation.Default"
	/// You are invited to a party!
	/// English String: "You are invited to a party!"
	/// </summary>
	public override string MessagePartyInvitationDefault => "您被邀請加入隊伍！";

	/// <summary>
	/// Key: "Message.PartyMembersJoined.Default"
	/// A new member joined your party!
	/// English String: "A new member joined your party!"
	/// </summary>
	public override string MessagePartyMembersJoinedDefault => "有新成員加入您的隊伍！";

	/// <summary>
	/// Key: "Message.PrivateMessageReceived.Default"
	/// You have a new private message.
	/// English String: "You have a new private message."
	/// </summary>
	public override string MessagePrivateMessageReceivedDefault => "您有新的私人訊息。";

	/// <summary>
	/// Key: "Message.TeamCreateInvitation.Default"
	/// A user invites another user to contribute to a team create game.
	/// English String: "You are invited to edit a game!"
	/// </summary>
	public override string MessageTeamCreateInvitationDefault => "您被邀請一起編輯遊戲！";

	public PushNotificationsResources_zh_tw(TranslationResourceState state)
		: base(state)
	{
	}

	/// <summary>
	/// Key: "Message.AddedToPrivateServerWhiteList.AddedToWhiteListMessage"
	/// {vipInviter} added you to their VIP server, {server}, at {place}!
	/// English String: "{vipInviter} added you to their VIP server, {server}, at {place}!"
	/// </summary>
	public override string MessageAddedToPrivateServerWhiteListAddedToWhiteListMessage(string vipInviter, string server, string place)
	{
		return $"{vipInviter} 已將您加入 {place} 的 {server} VIP 伺服器！";
	}

	protected override string _GetTemplateForMessageAddedToPrivateServerWhiteListAddedToWhiteListMessage()
	{
		return "{vipInviter} 已將您加入 {place} 的 {server} VIP 伺服器！";
	}

	protected override string _GetTemplateForMessageAddedToPrivateServerWhiteListDefault()
	{
		return "您已受邀到 VIP 伺服器！";
	}

	protected override string _GetTemplateForMessageDefaultSystemMessageBody()
	{
		return "Roblox 有新活動！";
	}

	protected override string _GetTemplateForMessageDefaultSystemMessageTitle()
	{
		return "Roblox";
	}

	/// <summary>
	/// Key: "Message.FriendRequestAccepted.AcceptedMessage"
	/// {friend} accepted your friend request!
	/// English String: "{friend} accepted your friend request!"
	/// </summary>
	public override string MessageFriendRequestAcceptedAcceptedMessage(string friend)
	{
		return $"{friend} 已接受您的好友邀請！";
	}

	protected override string _GetTemplateForMessageFriendRequestAcceptedAcceptedMessage()
	{
		return "{friend} 已接受您的好友邀請！";
	}

	protected override string _GetTemplateForMessageFriendRequestAcceptedDefault()
	{
		return "您的好友邀請已被接受！";
	}

	/// <summary>
	/// Key: "Message.FriendRequestReceived.CompleteMessage"
	/// You have just accepted {friend} as your friend!
	/// English String: "You are now friends with {friend}!"
	/// </summary>
	public override string MessageFriendRequestReceivedCompleteMessage(string friend)
	{
		return $"您已與 {friend} 成為好友！";
	}

	protected override string _GetTemplateForMessageFriendRequestReceivedCompleteMessage()
	{
		return "您已與 {friend} 成為好友！";
	}

	protected override string _GetTemplateForMessageFriendRequestReceivedDefault()
	{
		return "您有新的好友邀請！";
	}

	/// <summary>
	/// Key: "Message.FriendRequestReceived.NewRequestMessage"
	/// {friend} sent you a friend request!
	/// English String: "{friend} sent you a friend request!"
	/// </summary>
	public override string MessageFriendRequestReceivedNewRequestMessage(string friend)
	{
		return $"{friend} 向您傳送好友邀請！";
	}

	protected override string _GetTemplateForMessageFriendRequestReceivedNewRequestMessage()
	{
		return "{friend} 向您傳送好友邀請！";
	}

	protected override string _GetTemplateForMessageNewChatMessageDefault()
	{
		return "您有新的聊天訊息。";
	}

	/// <summary>
	/// Key: "Message.NewChatMessage.ReceivedGroupMessage"
	/// notification for a chat message sent in a Group Chat. Conversation title is Group Title.
	/// English String: "{senderUserName} to {conversationTitle}: {messageContent}"
	/// </summary>
	public override string MessageNewChatMessageReceivedGroupMessage(string senderUserName, string conversationTitle, string messageContent)
	{
		return $"{senderUserName} 對 {conversationTitle}：{messageContent}";
	}

	protected override string _GetTemplateForMessageNewChatMessageReceivedGroupMessage()
	{
		return "{senderUserName} 對 {conversationTitle}：{messageContent}";
	}

	/// <summary>
	/// Key: "Message.NewChatMessage.ReceivedMessage"
	/// {friend}: {message}
	/// English String: "{friend}: {message}"
	/// </summary>
	public override string MessageNewChatMessageReceivedMessage(string friend, string message)
	{
		return $"{friend}：{message}";
	}

	protected override string _GetTemplateForMessageNewChatMessageReceivedMessage()
	{
		return "{friend}：{message}";
	}

	protected override string _GetTemplateForMessagePartyInvitationDefault()
	{
		return "您被邀請加入隊伍！";
	}

	/// <summary>
	/// Key: "Message.PartyInvitation.PartyMessage"
	/// {partyInviter} invited you to a party!
	/// English String: "{partyInviter} invited you to a party!"
	/// </summary>
	public override string MessagePartyInvitationPartyMessage(string partyInviter)
	{
		return $"{partyInviter} 邀請您加入隊伍！";
	}

	protected override string _GetTemplateForMessagePartyInvitationPartyMessage()
	{
		return "{partyInviter} 邀請您加入隊伍！";
	}

	/// <summary>
	/// Key: "Message.PartyInvitation.XBoxPartyMessage"
	/// {partyInviter} invited you to an XBOX party!
	/// English String: "{partyInviter} invited you to an XBOX party!"
	/// </summary>
	public override string MessagePartyInvitationXBoxPartyMessage(string partyInviter)
	{
		return $"{partyInviter} 邀請您加入 Xbox 隊伍！";
	}

	protected override string _GetTemplateForMessagePartyInvitationXBoxPartyMessage()
	{
		return "{partyInviter} 邀請您加入 Xbox 隊伍！";
	}

	protected override string _GetTemplateForMessagePartyMembersJoinedDefault()
	{
		return "有新成員加入您的隊伍！";
	}

	/// <summary>
	/// Key: "Message.PartyMembersJoined.JoinMessage"
	/// {partyInvitee} joined your party!
	/// English String: "{partyInvitee} joined your party!"
	/// </summary>
	public override string MessagePartyMembersJoinedJoinMessage(string partyInvitee)
	{
		return $"{partyInvitee} 已加入您的隊伍！";
	}

	protected override string _GetTemplateForMessagePartyMembersJoinedJoinMessage()
	{
		return "{partyInvitee} 已加入您的隊伍！";
	}

	protected override string _GetTemplateForMessagePrivateMessageReceivedDefault()
	{
		return "您有新的私人訊息。";
	}

	/// <summary>
	/// Key: "Message.PrivateMessageReceived.MessageReceived"
	/// {messageSender} sent you a new private message.
	/// English String: "{messageSender} sent you a new private message."
	/// </summary>
	public override string MessagePrivateMessageReceivedMessageReceived(string messageSender)
	{
		return $"{messageSender} 向您傳送新的私人訊息。";
	}

	protected override string _GetTemplateForMessagePrivateMessageReceivedMessageReceived()
	{
		return "{messageSender} 向您傳送新的私人訊息。";
	}

	/// <summary>
	/// Key: "Message.TeamCreateInvitation"
	/// English String: "{inviter} invited you to edit the game: {gameName}!"
	/// </summary>
	public override string MessageTeamCreateInvitation(string inviter, string gameName)
	{
		return $"{inviter}邀請您編輯此遊戲：{gameName}！";
	}

	protected override string _GetTemplateForMessageTeamCreateInvitation()
	{
		return "{inviter}邀請您編輯此遊戲：{gameName}！";
	}

	protected override string _GetTemplateForMessageTeamCreateInvitationDefault()
	{
		return "您被邀請一起編輯遊戲！";
	}

	/// <summary>
	/// Key: "Messages.PlayTogether"
	/// English String: "{actorUsername} chose a game to play together: {universeName}"
	/// </summary>
	public override string MessagesPlayTogether(string actorUsername, string universeName)
	{
		return $"{actorUsername} 選擇了一起玩的遊戲：「{universeName}」";
	}

	protected override string _GetTemplateForMessagesPlayTogether()
	{
		return "{actorUsername} 選擇了一起玩的遊戲：「{universeName}」";
	}
}
