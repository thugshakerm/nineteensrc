namespace Roblox.TranslationResources.Moderation;

/// <summary>
/// This class overrides ModeratorActionsResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class ModeratorActionsResources_fr_fr : ModeratorActionsResources_en_us, IModeratorActionsResources, ITranslationResources
{
	/// <summary>
	/// Key: "Label.ScrubText"
	/// English String: "[ Content Deleted ]"
	/// </summary>
	public override string LabelScrubText => "[ Contenu supprimé ]";

	public ModeratorActionsResources_fr_fr(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForLabelScrubText()
	{
		return "[ Contenu supprimé ]";
	}
}
