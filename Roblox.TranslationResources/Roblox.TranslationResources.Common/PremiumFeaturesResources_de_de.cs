namespace Roblox.TranslationResources.Common;

/// <summary>
/// This class overrides PremiumFeaturesResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class PremiumFeaturesResources_de_de : PremiumFeaturesResources_en_us, IPremiumFeaturesResources, ITranslationResources
{
	/// <summary>
	/// Key: "Label.RobloxPremiumName"
	/// English String: "Roblox Premium"
	/// </summary>
	public override string LabelRobloxPremiumName => "Roblox-Premium";

	public PremiumFeaturesResources_de_de(TranslationResourceState state)
		: base(state)
	{
	}

	/// <summary>
	/// Key: "Label.RobloxPremium"
	/// English String: "Roblox Premium {amount}"
	/// </summary>
	public override string LabelRobloxPremium(string amount)
	{
		return $"Roblox-Premium {amount}";
	}

	protected override string _GetTemplateForLabelRobloxPremium()
	{
		return "Roblox-Premium {amount}";
	}

	protected override string _GetTemplateForLabelRobloxPremiumName()
	{
		return "Roblox-Premium";
	}

	/// <summary>
	/// Key: "Label.RobuxPackage"
	/// English String: "{amount} Robux"
	/// </summary>
	public override string LabelRobuxPackage(string amount)
	{
		return $"{amount}\u00a0Robux";
	}

	protected override string _GetTemplateForLabelRobuxPackage()
	{
		return "{amount}\u00a0Robux";
	}
}
