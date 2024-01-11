namespace Roblox.TranslationResources.Feature;

internal static class ServerListResourceFactory
{
	public const string FullNamespace = "Feature.ServerList";

	public static IServerListResources GetResources(TranslationResourceLocale locale, TranslationResourceState state)
	{
		return locale switch
		{
			TranslationResourceLocale.de_de => new ServerListResources_de_de(state), 
			TranslationResourceLocale.en_us => new ServerListResources_en_us(state), 
			TranslationResourceLocale.es_es => new ServerListResources_es_es(state), 
			TranslationResourceLocale.fr_fr => new ServerListResources_fr_fr(state), 
			TranslationResourceLocale.ja_jp => new ServerListResources_ja_jp(state), 
			TranslationResourceLocale.ko_kr => new ServerListResources_ko_kr(state), 
			TranslationResourceLocale.pt_br => new ServerListResources_pt_br(state), 
			TranslationResourceLocale.zh_cjv => new ServerListResources_zh_cjv(state), 
			TranslationResourceLocale.zh_cn => new ServerListResources_zh_cn(state), 
			TranslationResourceLocale.zh_tw => new ServerListResources_zh_tw(state), 
			_ => new ServerListResources_en_us(state), 
		};
	}
}
