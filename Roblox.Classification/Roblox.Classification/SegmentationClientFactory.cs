using System;
using Roblox.Classification.Properties;
using Roblox.Segmentation.Client;

namespace Roblox.Classification;

internal static class SegmentationClientFactory
{
	private static readonly SegmentationClient _Client = new SegmentationClient((Func<string>)(() => Settings.Default.ApiKey));

	public static SegmentationClient GetSegmentationClient()
	{
		return _Client;
	}
}
