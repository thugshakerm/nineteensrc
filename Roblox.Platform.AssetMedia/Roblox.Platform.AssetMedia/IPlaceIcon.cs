using System;
using System.Drawing.Imaging;
using Roblox.Thumbs;

namespace Roblox.Platform.AssetMedia;

/// <summary>
/// Represents a PlaceIcon object: a link between a place and its respective icon image.
/// </summary>
public interface IPlaceIcon
{
	long Id { get; }

	long PlaceId { get; }

	/// <summary>
	/// A null ImageId means that there is no implicitly defined PlaceIcon for this place and the default place screenshot will be used
	/// </summary>
	long? ImageId { get; set; }

	DateTime Created { get; }

	DateTime Updated { get; }

	/// <summary>
	/// Gets the <see cref="T:Roblox.Thumbs.ThumbResult" /> for this <see cref="T:Roblox.Platform.AssetMedia.IPlaceIcon" />'s image in the specified format, and size.
	/// </summary>
	/// <param name="width">The desired width.</param>
	/// <param name="height">The desired height.</param>
	/// <param name="format">The image format to use.</param>
	/// <param name="returnAutoGenerated">The image will be auto generated using the thumbnail service of the user's place, if true.</param>
	/// <returns>A ThumbResult for this <see cref="T:Roblox.Platform.AssetMedia.IPlaceIcon" />'s image.</returns>
	[Obsolete("Use PlaceIconThumbnailGetter.GetPlaceIconThumbResult instead.")]
	ThumbResult GetGameIconThumbResult(int width, int height, ImageFormat format, bool returnAutoGenerated = false);

	/// <summary>
	/// Gets the game icon <see cref="T:Roblox.Thumbs.ThumbResult" /> for this <see cref="T:Roblox.Platform.AssetMedia.IPlaceIcon" />.
	/// </summary>
	/// <param name="imageParameters">The <see cref="T:Roblox.Thumbs.ImageParameters" /> of the image to get.</param>
	/// <param name="returnAutoGenerated">The image will be auto generated using the thumbnail service of the user's place, if true.</param>
	/// <returns>The <see cref="T:Roblox.Thumbs.ThumbResult" /> for the game icon.</returns>
	/// <exception cref="T:Roblox.Platform.Core.PlatformArgumentNullException">Thrown if <paramref name="imageParameters" /> is null.</exception>
	[Obsolete("Use PlaceIconThumbnailGetter.GetPlaceIconThumbResult instead.")]
	ThumbResult GetGameIconThumbResult(ImageParameters imageParameters, bool returnAutoGenerated = false);
}
