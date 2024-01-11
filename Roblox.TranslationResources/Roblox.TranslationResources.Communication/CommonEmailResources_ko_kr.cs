namespace Roblox.TranslationResources.Communication;

/// <summary>
/// This class overrides CommonEmailResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class CommonEmailResources_ko_kr : CommonEmailResources_en_us, ICommonEmailResources, ITranslationResources
{
	public CommonEmailResources_ko_kr(TranslationResourceState state)
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
		return $"본 메시지는 {environmentIdentifier}에서 작성되었습니다.";
	}

	protected override string _GetTemplateForDescriptionMessageGenerationContent()
	{
		return "본 메시지는 {environmentIdentifier}에서 작성되었습니다.";
	}
}