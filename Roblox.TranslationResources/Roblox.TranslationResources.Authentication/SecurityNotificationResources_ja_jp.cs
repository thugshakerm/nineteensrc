namespace Roblox.TranslationResources.Authentication;

/// <summary>
/// This class overrides SecurityNotificationResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class SecurityNotificationResources_ja_jp : SecurityNotificationResources_en_us, ISecurityNotificationResources, ITranslationResources
{
	/// <summary>
	/// Key: "Heading.Important"
	/// English String: "Important"
	/// </summary>
	public override string HeadingImportant => "重要";

	public SecurityNotificationResources_ja_jp(TranslationResourceState state)
		: base(state)
	{
	}

	/// <summary>
	/// Key: "Description.SecurityNotificationText"
	/// English String: "For the safety and security of your account, your Roblox password has been reset. To regain access to your account, please type in your email or phone number and click the submit button on this page. If you do not have an email or phone number associated with your account, please contact Roblox customer service at {aTagStartWithHref}{emailMailToLink}{hrefEnd}{emailText}{aTagEnd}."
	/// </summary>
	public override string DescriptionSecurityNotificationText(string aTagStartWithHref, string emailMailToLink, string hrefEnd, string emailText, string aTagEnd)
	{
		return $"お持ちのアカウントの安全性とセキュリティのため、Robloxパスワードがリセットされました。 お持ちのアカウントへのアクセスを取り戻すには、メールアドレスか電話番号を入力して、このページの送信ボタンをクリックしてください。このアカウントに関連づけられたメールアドレスか電話番号がない場合は、Robloxカスタマーサービス\n{aTagStartWithHref}{emailMailToLink}{hrefEnd}{emailText}{aTagEnd} にご連絡ください。";
	}

	protected override string _GetTemplateForDescriptionSecurityNotificationText()
	{
		return "お持ちのアカウントの安全性とセキュリティのため、Robloxパスワードがリセットされました。 お持ちのアカウントへのアクセスを取り戻すには、メールアドレスか電話番号を入力して、このページの送信ボタンをクリックしてください。このアカウントに関連づけられたメールアドレスか電話番号がない場合は、Robloxカスタマーサービス\n{aTagStartWithHref}{emailMailToLink}{hrefEnd}{emailText}{aTagEnd} にご連絡ください。";
	}

	/// <summary>
	/// Key: "Description.SecurityNotificationTextWarning"
	/// English String: "Please choose a password that is brand {startSpan}new{endSpan} and {startSpan}unique{endSpan} to Roblox, do not use this password on any other site. This is the best way to prevent your Roblox account from getting compromised."
	/// </summary>
	public override string DescriptionSecurityNotificationTextWarning(string startSpan, string endSpan)
	{
		return $"まったく{startSpan}新しい{endSpan}Robloxだけの{startSpan}ユニーク{endSpan}なパスワードを選んでください。このパスワードを他のサイトでは使わないでください。これがお持ちのRobloxアカウントの乗っ取りを防ぐベストな方法です。";
	}

	protected override string _GetTemplateForDescriptionSecurityNotificationTextWarning()
	{
		return "まったく{startSpan}新しい{endSpan}Robloxだけの{startSpan}ユニーク{endSpan}なパスワードを選んでください。このパスワードを他のサイトでは使わないでください。これがお持ちのRobloxアカウントの乗っ取りを防ぐベストな方法です。";
	}

	protected override string _GetTemplateForHeadingImportant()
	{
		return "重要";
	}
}
