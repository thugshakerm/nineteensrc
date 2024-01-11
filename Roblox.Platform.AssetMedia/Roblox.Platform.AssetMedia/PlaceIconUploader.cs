using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using System.IO;
using Roblox.ApiClientBase;
using Roblox.Drawing;
using Roblox.EphemeralCounters;
using Roblox.FloodCheckers;
using Roblox.FloodCheckers.Core;
using Roblox.Platform.AssetMedia.Properties;
using Roblox.Platform.AssetOwnership;
using Roblox.Platform.Assets;
using Roblox.Platform.Assets.Interface;
using Roblox.Platform.Core;
using Roblox.Platform.EventStream;
using Roblox.Platform.EventStream.WebEvents;
using Roblox.Platform.EventStream.WebEvents.Events;
using Roblox.Platform.Membership;
using Roblox.Thumbs;

namespace Roblox.Platform.AssetMedia;

/// <summary>
/// Manages uploading of place icons.
/// </summary>
public class PlaceIconUploader : IPlaceIconUploader
{
	private readonly IAssetThumbnail _AssetThumbnail;

	private readonly IAssetOwnershipAuthority _AssetOwnershipAuthority;

	private readonly IPlaceIconFactory _PlaceIconFactory;

	private readonly IEphemeralCounterFactory _EphemeralCounterFactory;

	private readonly IImageFactory _ImageFactory;

	private readonly IEventStreamer _EventStreamer;

	private readonly IUploadFloodcheckerFactory _UploadFloodcheckerFactory;

	private const string _AddGeneratedIconAttemptCounterName = "AddGeneratedIcon_Attempt";

	private const string _AddGeneratedIconProcessingTimeCounterName = "AddGeneratedIcon_ProccesingTime";

	private static readonly ImageParameters _IconParameters = new ImageParameters(512, 512, ImageFormat.Png);

	private static TimeSpan DownloadTimeout => Settings.Default.DownloadImageTimeout;

	/// <summary>
	/// Constructs the place icon uploader.
	/// </summary>
	/// <param name="assetThumbnail">An <see cref="T:Roblox.Thumbs.IAssetThumbnail" /></param>
	/// <param name="assetOwnershipAuthority">An <see cref="T:Roblox.Platform.AssetOwnership.IAssetOwnershipAuthority" /></param>
	/// <param name="placeIconFactory">An <see cref="T:Roblox.Platform.AssetMedia.IPlaceIconFactory" /></param>
	/// <param name="ephemeralCounterFactory">An <see cref="T:Roblox.EphemeralCounters.IEphemeralCounterFactory" /></param>
	/// <param name="imageFactory">An <see cref="T:Roblox.Platform.Assets.IImageFactory" /></param>
	/// <param name="eventStreamer">An <see cref="T:Roblox.Platform.EventStream.IEventStreamer" /></param>
	/// <param name="uploadFloodcheckerFactory">An <see cref="T:Roblox.Platform.Assets.Interface.IUploadFloodcheckerFactory" /></param>
	/// <exception cref="T:System.ArgumentNullException">
	/// - <paramref name="assetThumbnail" />
	/// - <paramref name="assetOwnershipAuthority" />
	/// - <paramref name="placeIconFactory" />
	/// - <paramref name="ephemeralCounterFactory" />
	/// - <paramref name="imageFactory" />
	/// - <paramref name="eventStreamer" />
	/// - <paramref name="uploadFloodcheckerFactory" />
	/// </exception>
	public PlaceIconUploader(IAssetThumbnail assetThumbnail, IAssetOwnershipAuthority assetOwnershipAuthority, IPlaceIconFactory placeIconFactory, IEphemeralCounterFactory ephemeralCounterFactory, IImageFactory imageFactory, IEventStreamer eventStreamer, IUploadFloodcheckerFactory uploadFloodcheckerFactory)
	{
		_AssetThumbnail = assetThumbnail ?? throw new ArgumentNullException("assetThumbnail");
		_AssetOwnershipAuthority = assetOwnershipAuthority ?? throw new ArgumentNullException("assetOwnershipAuthority");
		_PlaceIconFactory = placeIconFactory ?? throw new ArgumentNullException("placeIconFactory");
		_EphemeralCounterFactory = ephemeralCounterFactory ?? throw new ArgumentNullException("ephemeralCounterFactory");
		_ImageFactory = imageFactory ?? throw new ArgumentNullException("imageFactory");
		_EventStreamer = eventStreamer ?? throw new ArgumentNullException("eventStreamer");
		_UploadFloodcheckerFactory = uploadFloodcheckerFactory ?? throw new ArgumentNullException("uploadFloodcheckerFactory");
	}

	/// <inheritdoc cref="M:Roblox.Platform.AssetMedia.IPlaceIconUploader.SetGeneratedPlaceIcon(Roblox.Platform.Assets.IPlace,Roblox.Platform.Membership.IUser)" />
	public IPlaceIcon SetGeneratedPlaceIcon(IPlace place, IUser actingUser)
	{
		if (place == null)
		{
			throw new ArgumentNullException("place");
		}
		if (actingUser == null)
		{
			throw new ArgumentNullException("actingUser");
		}
		ThumbResult autoThumbnailResult = _AssetThumbnail.GetThumbnailUrl(place, _IconParameters, overrideModeration: false, ignoreAssetMediaItems: true, returnAutoGenerated: true);
		if (autoThumbnailResult == null || !autoThumbnailResult.final)
		{
			throw new PlatformOperationUnavailableException("Generated place icon is not available");
		}
		IFloodChecker generatedIconUploadFloodChecker = CreatePlaceIconUploadFloodChecker(place);
		if (generatedIconUploadFloodChecker.IsFlooded())
		{
			throw new PlatformFloodedException("Too many place icon upload requests");
		}
		IncrementEphemeralCounter("AddGeneratedIcon_Attempt");
		Stopwatch stopWatch = Stopwatch.StartNew();
		string downloadUrl = autoThumbnailResult.GetUrl(arg: true);
		IPlaceIcon placeIcon;
		using (MemoryStream stream = new MemoryStream(DownloadImageData(downloadUrl)))
		{
			placeIcon = SetPlaceIconFromStream(place, actingUser, stream);
		}
		generatedIconUploadFloodChecker.UpdateCount();
		stopWatch.Stop();
		AddProcessingTimeToEphemeralCounter("AddGeneratedIcon_ProccesingTime", stopWatch.Elapsed.TotalMilliseconds);
		return placeIcon;
	}

	/// <inheritdoc cref="M:Roblox.Platform.AssetMedia.IPlaceIconUploader.SetPlaceIcon(Roblox.Platform.Assets.IPlace,Roblox.Platform.Membership.IUser,System.IO.Stream)" />
	public IPlaceIcon SetPlaceIcon(IPlace place, IUser actingUser, Stream imageData)
	{
		if (place == null)
		{
			throw new ArgumentNullException("place");
		}
		if (imageData == null)
		{
			throw new ArgumentNullException("imageData");
		}
		if (actingUser == null)
		{
			throw new ArgumentNullException("actingUser");
		}
		IRetryAfterFloodChecker imageUploadFloodChecker = _UploadFloodcheckerFactory.GetImageUploadFloodchecker(actingUser);
		if (imageUploadFloodChecker.IsFlooded())
		{
			throw new PlatformFloodedException("Too many place icon upload requests");
		}
		IPlaceIcon result = SetPlaceIconFromStream(place, actingUser, imageData);
		imageUploadFloodChecker.UpdateCount();
		return result;
	}

	[ExcludeFromCodeCoverage]
	internal virtual IFloodChecker CreatePlaceIconUploadFloodChecker(IPlace place)
	{
		return new GeneratedIconUploadFloodChecker(place.Id);
	}

	[ExcludeFromCodeCoverage]
	internal virtual byte[] DownloadImageData(string downloadUrl)
	{
		using TimeoutWebClient webClient = new TimeoutWebClient(DownloadTimeout);
		return webClient.DownloadData(downloadUrl);
	}

	private IPlaceIcon SetPlaceIconFromStream(IPlace place, IUser user, Stream stream)
	{
		IPlaceIcon placeIcon = _PlaceIconFactory.GetPlaceIconByPlaceId(place.Id);
		string oldIconUrl = placeIcon.GetGameIconThumbResult(_IconParameters)?.GetUrl(arg: true);
		AssetCreatorInfo assetCreatorInfo = new AssetCreatorInfo(Roblox.Platform.Assets.CreatorType.User, user.Id);
		TrustedAssetTextInfo assetNameAndDescription = new TrustedAssetTextInfo($"{place.Name}_Image", string.Empty);
		Stream resampledTexture;
		try
		{
			resampledTexture = ImageUtil.ResampleTextureEnforceDesiredSizeWithPadding(stream, _IconParameters.Width, _IconParameters.Height);
		}
		catch (ArgumentException)
		{
			throw new InvalidDataException("The provided image data stream is in an invalid format");
		}
		IImage image;
		using (resampledTexture)
		{
			StreamCreatorInfo streamCreatorInfo = new StreamCreatorInfo(resampledTexture);
			image = _ImageFactory.CreateWithTrustedAssetText(assetNameAndDescription, assetCreatorInfo, streamCreatorInfo, user);
		}
		_AssetOwnershipAuthority.AwardAsset(image.Id, user.Id);
		placeIcon.ImageId = image.Id;
		_PlaceIconFactory.SavePlaceIcon(ref placeIcon);
		string newIconUrl = placeIcon.GetGameIconThumbResult(_IconParameters)?.GetUrl(arg: true);
		if (oldIconUrl != newIconUrl)
		{
			SendConfigurePlaceEvent(place, oldIconUrl, newIconUrl);
		}
		return placeIcon;
	}

	private void IncrementEphemeralCounter(string counterName)
	{
		_EphemeralCounterFactory.GetCounter(counterName).IncrementInBackground(1, (Action<Exception>)null);
	}

	private void AddProcessingTimeToEphemeralCounter(string counterName, double timeInMilliseconds)
	{
		ISequence counter = _EphemeralCounterFactory.GetSequence(counterName);
		RobloxThreadPool.QueueUserWorkItem(delegate
		{
			counter.Add(timeInMilliseconds);
		});
	}

	private void SendConfigurePlaceEvent(IPlace place, string oldIconUrl, string newIconUrl)
	{
		ConfigurePlaceEventArgs eventArgs = new ConfigurePlaceEventArgs();
		eventArgs.PlaceId = place.Id;
		eventArgs.OldImageIconUrl = oldIconUrl;
		eventArgs.NewImageIconUrl = newIconUrl;
		new ConfigurePlaceEvent(_EventStreamer, eventArgs).Stream();
	}
}
