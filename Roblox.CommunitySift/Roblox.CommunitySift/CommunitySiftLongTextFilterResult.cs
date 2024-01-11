using System.Collections.Generic;

namespace Roblox.CommunitySift;

internal class CommunitySiftLongTextFilterResult : ICommunitySiftLongTextFilterResult
{
	public bool TextWasFiltered { get; set; }

	public string FilteredText { get; set; }

	public HashSet<CommunitySiftModerationTopic> FilteredCategories { get; set; }
}
