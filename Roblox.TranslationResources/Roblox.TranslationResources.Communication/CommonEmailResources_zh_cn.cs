namespace Roblox.TranslationResources.Communication;

/// <summary>
/// This class overrides CommonEmailResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class CommonEmailResources_zh_cn : CommonEmailResources_en_us, ICommonEmailResources, ITranslationResources
{
	public CommonEmailResources_zh_cn(TranslationResourceState state)
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
		return $"此信息由 {environmentIdentifier} 生成。";
	}

	protected override string _GetTemplateForDescriptionMessageGenerationContent()
	{
		return "此信息由 {environmentIdentifier} 生成。";
	}
}
