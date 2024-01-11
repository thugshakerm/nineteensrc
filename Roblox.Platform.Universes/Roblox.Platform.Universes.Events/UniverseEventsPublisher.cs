using System;
using Amazon;
using Roblox.Amazon.Sns;
using Roblox.Configuration;
using Roblox.EventLog;
using Roblox.Instrumentation;
using Roblox.Platform.Universes.Properties;

namespace Roblox.Platform.Universes.Events;

/// <summary>
/// UniverseEventsPublisher implements <see cref="T:Roblox.Platform.Universes.Events.IUniverseEventsObserver" /> to get notifications of 
/// events and publishes them to a Universe entity changed SNS topic.
/// </summary>
internal class UniverseEventsPublisher : IUniverseEventsObserver, IObserver<UniverseEntityEvent>
{
	private readonly ILogger _Logger;

	private readonly ICounterRegistry _CounterRegistry;

	private SnsPublisher _Publisher;

	private string _AccessKeyAndSecretCsv;

	private string _SnsTopicArn;

	private IDisposable _Observable;

	private string Name => "UniverseEventsPublisher";

	/// <summary>
	/// Constructs a new <see cref="T:Roblox.Platform.Universes.Events.UniverseEventsPublisher" />.
	/// </summary>
	/// <param name="logger">An <see cref="T:Roblox.EventLog.ILogger" /></param>
	/// <param name="counterRegistry">the counter registry (used by the <see cref="T:Roblox.Instrumentation.ICounterReporter" /> for telemetry)</param>
	/// <exception cref="T:System.ArgumentNullException"><paramref name="logger" /></exception>
	public UniverseEventsPublisher(ILogger logger, ICounterRegistry counterRegistry)
	{
		_Logger = logger ?? throw new ArgumentNullException("logger");
		_CounterRegistry = counterRegistry;
		InitializePublisher();
		Settings.Default.MonitorChanges((Settings s) => s.AwsUniverseChangeTopicAccessKeyIdAndSecretCsv, InitializePublisher);
		Settings.Default.MonitorChanges((Settings s) => s.AwsUniverseChangeSnsTopicArn, InitializePublisher);
	}

	/// <inheritdoc />
	public void Subscribe(IObservable<UniverseEntityEvent> observable)
	{
		_Observable = observable.Subscribe(this);
	}

	/// <inheritdoc />
	public void Unsubscribe()
	{
		_Observable.Dispose();
	}

	/// <inheritdoc />
	public void OnNext(UniverseEntityEvent value)
	{
		if (Settings.Default.IsPublishToUniverseChangeTopicEnabled && value != null)
		{
			if (_Publisher == null)
			{
				_Logger.Error($"{Name} has a null SNS publisher! universeId:{value.UniverseId}");
			}
			else
			{
				_Publisher.Publish(value);
			}
		}
	}

	/// <summary>
	/// This is not implemented.
	/// </summary>
	/// <param name="error"></param>
	/// <exception cref="T:System.NotImplementedException"></exception>
	public void OnError(Exception error)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public void OnCompleted()
	{
		Unsubscribe();
	}

	private void InitializePublisher()
	{
		VerifyAndCreatePublisher(_AccessKeyAndSecretCsv, _SnsTopicArn, Settings.Default.AwsUniverseChangeTopicAccessKeyIdAndSecretCsv, Settings.Default.AwsUniverseChangeSnsTopicArn);
	}

	private void VerifyAndCreatePublisher(string previousKeyAndSecret, string previousTopicArn, string newAccessKeyIdAndSecretCsv, string newSnsTopicArn)
	{
		if (_Publisher != null && previousKeyAndSecret == newAccessKeyIdAndSecretCsv && previousTopicArn == newSnsTopicArn)
		{
			return;
		}
		string initOrReinit = ((_Publisher == null) ? "Initializing" : "Re-initializing");
		_Logger.Info($"{initOrReinit} {Name}");
		string[] keyAndSecret = newAccessKeyIdAndSecretCsv.Split(',');
		if (keyAndSecret.Length != 2)
		{
			_Logger.Error($"{initOrReinit} {Name}, invalid AWS Key ID & Secret CSV.");
			return;
		}
		string keyId = keyAndSecret[0];
		string secretKey = keyAndSecret[1];
		try
		{
			_Publisher = new SnsPublisher(keyId, secretKey, RegionEndpoint.USEast1, newSnsTopicArn, $"Roblox.{Name}", _CounterRegistry);
			_Publisher.SnsError += OnSnsError;
		}
		catch (ArgumentException ex)
		{
			_Logger.Error($"{initOrReinit} {Name} failed: {ex}");
		}
		_AccessKeyAndSecretCsv = newAccessKeyIdAndSecretCsv;
		_SnsTopicArn = newSnsTopicArn;
	}

	private void OnSnsError(Exception e, string message)
	{
		_Logger.Error($"Error sending {Name} SNS: {e}.\nMessage: {message}");
	}
}
