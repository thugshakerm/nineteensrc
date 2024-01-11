namespace Roblox.TranslationResources.CommonUI;

internal static class MessagesResourceFactory
{
	public const string FullNamespace = "CommonUI.Messages";

	public static IMessagesResources GetResources(TranslationResourceLocale locale, TranslationResourceState state)
	{
		return locale switch
		{
			TranslationResourceLocale.de_de => new MessagesResources_de_de(state), 
			TranslationResourceLocale.en_us => new MessagesResources_en_us(state), 
			TranslationResourceLocale.es_es => new MessagesResources_es_es(state), 
			TranslationResourceLocale.fr_fr => new MessagesResources_fr_fr(state), 
			TranslationResourceLocale.id_id => new MessagesResources_id_id(state), 
			TranslationResourceLocale.it_it => new MessagesResources_it_it(state), 
			TranslationResourceLocale.ja_jp => new MessagesResources_ja_jp(state), 
			TranslationResourceLocale.ko_kr => new MessagesResources_ko_kr(state), 
			TranslationResourceLocale.pt_br => new MessagesResources_pt_br(state), 
			TranslationResourceLocale.ru_ru => new MessagesResources_ru_ru(state), 
			TranslationResourceLocale.th_th => new MessagesResources_th_th(state), 
			TranslationResourceLocale.tr_tr => new MessagesResources_tr_tr(state), 
			TranslationResourceLocale.vi_vn => new MessagesResources_vi_vn(state), 
			TranslationResourceLocale.zh_cjv => new MessagesResources_zh_cjv(state), 
			TranslationResourceLocale.zh_cn => new MessagesResources_zh_cn(state), 
			TranslationResourceLocale.zh_tw => new MessagesResources_zh_tw(state), 
			_ => new MessagesResources_en_us(state), 
		};
	}
}
