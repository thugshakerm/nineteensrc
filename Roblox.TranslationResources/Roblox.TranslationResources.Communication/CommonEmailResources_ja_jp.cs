namespace Roblox.TranslationResources.Communication;

/// <summary>
/// This class overrides CommonEmailResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class CommonEmailResources_ja_jp : CommonEmailResources_en_us, ICommonEmailResources, ITranslationResources
{
	public CommonEmailResources_ja_jp(TranslationResourceState state)
		: base(state)
	{
	}

	/// <summary>
	/// Key: "Description.MessageGenerationContent"
	/// The environment specific generation message appended to each email.
	/// English String: "This message was generated by {environmentIdentifier}."
	/// </summary>
	public override string DescriptionMessageGenerationContent(string environmentIdentifier)
	{
		return $"このメッセージは、{environmentIdentifier}によって生成されています。";
	}

	protected override string _GetTemplateForDescriptionMessageGenerationContent()
	{
		return "このメッセージは、{environmentIdentifier}によって生成されています。";
	}
}
