namespace Roblox.TranslationResources.Feature;

/// <summary>
/// This class overrides SocialShareResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class SocialShareResources_pt_br : SocialShareResources_en_us, ISocialShareResources, ITranslationResources
{
	/// <summary>
	/// Key: "Action.Share"
	/// Label for share button.
	/// English String: "Share"
	/// </summary>
	public override string ActionShare => "Comp.";

	public SocialShareResources_pt_br(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForActionShare()
	{
		return "Comp.";
	}
}
