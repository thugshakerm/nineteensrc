namespace Roblox.TranslationResources.Feature;

/// <summary>
/// This class overrides PlaceThumbnailsResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class PlaceThumbnailsResources_zh_cn : PlaceThumbnailsResources_en_us, IPlaceThumbnailsResources, ITranslationResources
{
	/// <summary>
	/// Key: "Action.ContinueToVideo"
	/// The button text that user confirm for leaving to Youtube
	/// English String: "Continue to Video"
	/// </summary>
	public override string ActionContinueToVideo => "继续前往视频";

	/// <summary>
	/// Key: "Description.LeaveRobloxForYouTube"
	/// The content of the dialog that will show up when user is leaving Roblox to YouTube
	/// English String: "You are about to leave Roblox to view a video on YouTube."
	/// </summary>
	public override string DescriptionLeaveRobloxForYouTube => "你即将离开 Roblox，前往 Youtube 观看视频。";

	/// <summary>
	/// Key: "Description.YouTubeIsNotRoblox"
	/// The content of the dialog that will show up when user is leaving Roblox to YouTube
	/// English String: "YouTube is not part of Roblox.com and is governed by a separate privacy policy."
	/// </summary>
	public override string DescriptionYouTubeIsNotRoblox => "Youtube 不属于 Roblox.com，受单独隐私政策的监管。";

	/// <summary>
	/// Key: "Heading.LeavingRoblox"
	/// The title of the dialog that will show up when user is leaving Roblox to Youtube
	/// English String: "You are leaving Roblox"
	/// </summary>
	public override string HeadingLeavingRoblox => "你即将离开 Roblox";

	/// <summary>
	/// Key: "Label.Next"
	/// English String: "Next"
	/// </summary>
	public override string LabelNext => "下一步";

	/// <summary>
	/// Key: "Label.Previous"
	/// English String: "Previous"
	/// </summary>
	public override string LabelPrevious => "上一步";

	public PlaceThumbnailsResources_zh_cn(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForActionContinueToVideo()
	{
		return "继续前往视频";
	}

	protected override string _GetTemplateForDescriptionLeaveRobloxForYouTube()
	{
		return "你即将离开 Roblox，前往 Youtube 观看视频。";
	}

	protected override string _GetTemplateForDescriptionYouTubeIsNotRoblox()
	{
		return "Youtube 不属于 Roblox.com，受单独隐私政策的监管。";
	}

	protected override string _GetTemplateForHeadingLeavingRoblox()
	{
		return "你即将离开 Roblox";
	}

	protected override string _GetTemplateForLabelNext()
	{
		return "下一步";
	}

	protected override string _GetTemplateForLabelPrevious()
	{
		return "上一步";
	}
}
