namespace Roblox.TranslationResources.Feature;

/// <summary>
/// This class overrides FeedsResources_en_us to provide locale specific translations where possible,
/// using the base translations where they are not provided
/// </summary>
internal class FeedsResources_es_es : FeedsResources_en_us, IFeedsResources, ITranslationResources
{
	/// <summary>
	/// Key: "Heading.BuildSomething"
	/// English String: "Build Something"
	/// </summary>
	public override string HeadingBuildSomething => "Construye algo";

	/// <summary>
	/// Key: "Heading.CustomizeAvatar"
	/// English String: "Customize Your Avatar"
	/// </summary>
	public override string HeadingCustomizeAvatar => "Personaliza tu avatar";

	/// <summary>
	/// Key: "Heading.ForumHelp"
	/// English String: "Roblox forums for help"
	/// </summary>
	public override string HeadingForumHelp => "Ayuda en los foros de Roblox";

	/// <summary>
	/// Key: "Heading.MakeFriends"
	/// English String: "Make Friends"
	/// </summary>
	public override string HeadingMakeFriends => "Haz amigos";

	/// <summary>
	/// Key: "Heading.PlayGames"
	/// English String: "Play Games"
	/// </summary>
	public override string HeadingPlayGames => "Juega a juegos";

	/// <summary>
	/// Key: "Label.NoFeedLink"
	/// English String: "make some best friends now."
	/// </summary>
	public override string LabelNoFeedLink => "Haz amigos ahora.";

	/// <summary>
	/// Key: "Label.NoFeedText"
	/// English String: "No news about your best friends... want to know what your best friends are up to?"
	/// </summary>
	public override string LabelNoFeedText => "No hay noticias de tus mejores amigos... ¿Quieres saber qué hacen tus amigos?";

	/// <summary>
	/// Key: "Label.PlayGames"
	/// English String: "Nearly all Roblox games are built by players like you. Here are some of our favorites:"
	/// </summary>
	public override string LabelPlayGames => "Casi todos los juegos de Roblox los construyen jugadores como tú. Estos son algunos de nuestros favoritos:";

	public FeedsResources_es_es(TranslationResourceState state)
		: base(state)
	{
	}

	protected override string _GetTemplateForHeadingBuildSomething()
	{
		return "Construye algo";
	}

	protected override string _GetTemplateForHeadingCustomizeAvatar()
	{
		return "Personaliza tu avatar";
	}

	protected override string _GetTemplateForHeadingForumHelp()
	{
		return "Ayuda en los foros de Roblox";
	}

	protected override string _GetTemplateForHeadingMakeFriends()
	{
		return "Haz amigos";
	}

	protected override string _GetTemplateForHeadingPlayGames()
	{
		return "Juega a juegos";
	}

	/// <summary>
	/// Key: "Label.BuildSomething"
	/// English String: "Builders will enjoy playing our multiplayer building game. Professional builders will want to check out Roblox Studio, our game development environment on your {linkStart}Develop page{linkEnd}."
	/// </summary>
	public override string LabelBuildSomething(string linkStart, string linkEnd)
	{
		return $"A los constructores les encantará nuestro juego de construcción multijugador. Los constructores profesionales deberían echar un vistazo a Roblox Studio, nuestro entorno de desarrollo de juegos en la {linkStart}página de creación{linkEnd}.";
	}

	protected override string _GetTemplateForLabelBuildSomething()
	{
		return "A los constructores les encantará nuestro juego de construcción multijugador. Los constructores profesionales deberían echar un vistazo a Roblox Studio, nuestro entorno de desarrollo de juegos en la {linkStart}página de creación{linkEnd}.";
	}

	/// <summary>
	/// Key: "Label.CustomizeAvatarDesktop"
	/// English String: "Visit the {avatarLinkStart}Avatar Editor{avatarLinkEnd} to customize your character. Shop for clothing items in the {catalogLinkStart}Catalog{catalogLinkEnd}."
	/// </summary>
	public override string LabelCustomizeAvatarDesktop(string avatarLinkStart, string avatarLinkEnd, string catalogLinkStart, string catalogLinkEnd)
	{
		return $"Visita el {avatarLinkStart}Editor de avatares{avatarLinkEnd} para personalizar tu avatar. Consigue ropa nueva en el {catalogLinkStart}Catálogo{catalogLinkEnd}.";
	}

	protected override string _GetTemplateForLabelCustomizeAvatarDesktop()
	{
		return "Visita el {avatarLinkStart}Editor de avatares{avatarLinkEnd} para personalizar tu avatar. Consigue ropa nueva en el {catalogLinkStart}Catálogo{catalogLinkEnd}.";
	}

	/// <summary>
	/// Key: "Label.CustomizeAvatarPhone"
	/// English String: "Get new clothing in the {linkStart} catalog {linkEnd} and customize your avatar."
	/// </summary>
	public override string LabelCustomizeAvatarPhone(string linkStart, string linkEnd)
	{
		return $"Consigue ropa nueva en el {linkStart} Catálogo {linkEnd} y personaliza tu avatar.";
	}

	protected override string _GetTemplateForLabelCustomizeAvatarPhone()
	{
		return "Consigue ropa nueva en el {linkStart} Catálogo {linkEnd} y personaliza tu avatar.";
	}

	/// <summary>
	/// Key: "Label.ForumHelp"
	/// English String: "No matter what you're looking for, if it's Roblox related, there are people talking about it {linkStart}here{linkEnd}."
	/// </summary>
	public override string LabelForumHelp(string linkStart, string linkEnd)
	{
		return $"Busques lo que busques, si tiene que ver con Roblox, alguien está hablando de ello {linkStart}aquí{linkEnd}.";
	}

	protected override string _GetTemplateForLabelForumHelp()
	{
		return "Busques lo que busques, si tiene que ver con Roblox, alguien está hablando de ello {linkStart}aquí{linkEnd}.";
	}

	/// <summary>
	/// Key: "Label.MakeFriends"
	/// English String: "Meet other players in-game and send them a friend request. If you miss your opportunity you can always send a request later by {linkStart}searching{linkEnd} for their user profile."
	/// </summary>
	public override string LabelMakeFriends(string linkStart, string linkEnd)
	{
		return $"Conoce a otros jugadores dentro del juego y envíales una solicitud de amistad. Si pierdes la ocasión, siempre puedes enviar la solicitud más tarde {linkStart}buscando{linkEnd} su perfil de usuario.";
	}

	protected override string _GetTemplateForLabelMakeFriends()
	{
		return "Conoce a otros jugadores dentro del juego y envíales una solicitud de amistad. Si pierdes la ocasión, siempre puedes enviar la solicitud más tarde {linkStart}buscando{linkEnd} su perfil de usuario.";
	}

	protected override string _GetTemplateForLabelNoFeedLink()
	{
		return "Haz amigos ahora.";
	}

	protected override string _GetTemplateForLabelNoFeedText()
	{
		return "No hay noticias de tus mejores amigos... ¿Quieres saber qué hacen tus amigos?";
	}

	protected override string _GetTemplateForLabelPlayGames()
	{
		return "Casi todos los juegos de Roblox los construyen jugadores como tú. Estos son algunos de nuestros favoritos:";
	}
}
