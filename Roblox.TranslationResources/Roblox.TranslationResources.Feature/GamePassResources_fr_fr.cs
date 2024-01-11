namespace Roblox.TranslationResources.Feature;

/// <summary>
/// This class overrides GamePassResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class GamePassResources_fr_fr : GamePassResources_en_us, IGamePassResources, ITranslationResources
{
	/// <summary>
	/// Key: "Action.AddPass"
	/// English String: "Add Pass"
	/// </summary>
	public override string ActionAddPass => "Ajouter passe";

	/// <summary>
	/// Key: "Heading.PassesForThisGame"
	/// English String: "Passes"
	/// </summary>
	public override string HeadingPassesForThisGame => "Passes";

	/// <summary>
	/// Key: "Label.Buy"
	/// English String: "Buy"
	/// </summary>
	public override string LabelBuy => "Acheter";

	/// <summary>
	/// Key: "Label.Owned"
	/// English String: "Owned"
	/// </summary>
	public override string LabelOwned => "Possédé";

	public GamePassResources_fr_fr(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForActionAddPass()
	{
		return "Ajouter passe";
	}

	protected override string _GetTemplateForHeadingPassesForThisGame()
	{
		return "Passes";
	}

	protected override string _GetTemplateForLabelBuy()
	{
		return "Acheter";
	}

	protected override string _GetTemplateForLabelOwned()
	{
		return "Possédé";
	}
}
