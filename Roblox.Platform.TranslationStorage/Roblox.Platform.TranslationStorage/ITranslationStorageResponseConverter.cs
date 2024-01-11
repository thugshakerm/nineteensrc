using Roblox.TranslationStorage.Client;

namespace Roblox.Platform.TranslationStorage;

internal interface ITranslationStorageResponseConverter
{
	DeleteTranslationResponse ConvertDeleteTranslationResponse(DeleteTranslationResponse clientRespone);

	CreateOrUpdateTranslationStatus ConvertCreateOrUpdateTranslationResponse(CreateOrUpdateTranslationResponse clientResponse);
}