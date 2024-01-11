using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Roblox.Configuration;

namespace Roblox.BriteVerify.Properties;

/// <summary>
/// </summary>
[SettingsProvider(typeof(Provider))]
[ExcludeFromCodeCoverage]
[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.3.0.0")]
internal sealed class Settings : ApplicationSettingsBase, IBriteVerifySettings
{
	private readonly ConcurrentDictionary<string, object> _Properties = new ConcurrentDictionary<string, object>();

	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public override object this[string propertyName]
	{
		get
		{
			return _Properties.GetOrAdd(propertyName, (string propName) => base[propName]);
		}
		set
		{
			base[propertyName] = value;
		}
	}

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("00:00:30")]
	public TimeSpan BriteVerifyCircuitBreakerRetryInterval => (TimeSpan)this["BriteVerifyCircuitBreakerRetryInterval"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string BriteVerifyApiKey => (string)this["BriteVerifyApiKey"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("https://bpi.briteverify.com")]
	public string BriteVerifyApiEndpoint => (string)this["BriteVerifyApiEndpoint"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("00:00:10")]
	public TimeSpan BriteVerifyClientTimeoutInterval => (TimeSpan)this["BriteVerifyClientTimeoutInterval"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool IsDetailedBriteVerifyErrorLoggingEnabled => (bool)this["IsDetailedBriteVerifyErrorLoggingEnabled"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("/emails.json")]
	public string BriteVerifyUri => (string)this["BriteVerifyUri"];

	internal Settings()
	{
		base.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
		{
			_Properties.TryRemove(args.PropertyName, out var _);
		};
	}

	protected override void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
	{
		base.OnSettingsLoaded(sender, e);
		Provider.RegisterSettings(e, this);
	}
}
