namespace Roblox.TranslationResources.Feature;

/// <summary>
/// This class overrides PlacesListResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class PlacesListResources_ja_jp : PlacesListResources_en_us, IPlacesListResources, ITranslationResources
{
	/// <summary>
	/// Key: "Action.JoinGame"
	/// Join game
	/// English String: "Join"
	/// </summary>
	public override string ActionJoinGame => "参加";

	/// <summary>
	/// Key: "Action.SeeAll"
	/// English String: "See All"
	/// </summary>
	public override string ActionSeeAll => "すべて見る";

	/// <summary>
	/// Key: "Action.ViewDetails"
	/// check game details page
	/// English String: "View Details"
	/// </summary>
	public override string ActionViewDetails => "詳細を表示";

	/// <summary>
	/// Key: "Label.ContextMenuTitle"
	/// English String: "Game"
	/// </summary>
	public override string LabelContextMenuTitle => "ゲーム";

	/// <summary>
	/// Key: "Label.PlacesListName"
	/// Title of game list
	/// English String: "Games"
	/// </summary>
	public override string LabelPlacesListName => "ゲーム";

	public PlacesListResources_ja_jp(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForActionJoinGame()
	{
		return "参加";
	}

	protected override string _GetTemplateForActionSeeAll()
	{
		return "すべて見る";
	}

	protected override string _GetTemplateForActionViewDetails()
	{
		return "詳細を表示";
	}

	protected override string _GetTemplateForLabelContextMenuTitle()
	{
		return "ゲーム";
	}

	/// <summary>
	/// Key: "Label.CreatorBy"
	/// English String: "By {creatorLink}"
	/// </summary>
	public override string LabelCreatorBy(string creatorLink)
	{
		return $"作：{creatorLink}";
	}

	protected override string _GetTemplateForLabelCreatorBy()
	{
		return "作：{creatorLink}";
	}

	protected override string _GetTemplateForLabelPlacesListName()
	{
		return "ゲーム";
	}

	/// <summary>
	/// Key: "Label.PlayingPhrase"
	/// number of players playing
	/// English String: "{playerCount} Playing"
	/// </summary>
	public override string LabelPlayingPhrase(string playerCount)
	{
		return $"{playerCount}人がプレイ中";
	}

	protected override string _GetTemplateForLabelPlayingPhrase()
	{
		return "{playerCount}人がプレイ中";
	}
}
