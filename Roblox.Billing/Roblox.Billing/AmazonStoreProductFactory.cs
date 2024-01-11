using Roblox.Billing.AmazonStoreExceptions;

namespace Roblox.Billing;

/// <summary>
/// Provides a common methods for translating an Amazon product ID to a Product.
/// </summary>
public class AmazonStoreProductFactory
{
	/// <summary>
	/// Gets the product corresponding to the given Amazon product ID.
	/// </summary>
	/// <param name="amazonProductId">The Amazon product ID of the product to get.</param>
	/// <returns>The <see cref="T:Roblox.Billing.IProduct" /> corresponding to the given Amazon product ID.</returns>
	public static IProduct GetProductFromAmazonProductId(string amazonProductId)
	{
		int productId;
		switch (amazonProductId.ToLower())
		{
		case "com.roblox.client.bc1month":
			productId = Product.BC30DaysID;
			break;
		case "com.roblox.client.tbc1month":
			productId = Product.Get("Turbo Builders Club 1 Month").ID;
			break;
		case "com.roblox.client.obc1month":
			productId = Product.Get("Outrageous Builders Club 1 Month").ID;
			break;
		case "com.roblox.client.bc3month":
			productId = Product.Get("Builders Club 90 Days").ID;
			break;
		case "com.roblox.client.tbc3month":
			productId = Product.Get("Turbo Builders Club 3 Months").ID;
			break;
		case "com.roblox.client.obc3month":
			productId = Product.Get("Outrageous Builders Club 3 Months").ID;
			break;
		case "com.roblox.client.robux80":
		case "com.roblox.client.robux80nonbc":
			productId = Product.Get("80 Robux").ID;
			break;
		case "com.roblox.client.robux90":
		case "com.roblox.client.robux90bc":
			productId = Product.Get("90 Robux").ID;
			break;
		case "com.roblox.client.robux160":
			productId = Product.Get("160 Robux").ID;
			break;
		case "com.roblox.client.robux180bc":
			productId = Product.Get("180 Robux").ID;
			break;
		case "com.roblox.client.robux240":
			productId = Product.Get("240 Robux").ID;
			break;
		case "com.roblox.client.robux270bc":
			productId = Product.Get("270 Robux").ID;
			break;
		case "com.roblox.client.robux320":
			productId = Product.Get("320 Robux").ID;
			break;
		case "com.roblox.client.robux360bc":
			productId = Product.Get("360 Robux").ID;
			break;
		case "com.roblox.client.robux400":
			productId = Product.Get("400 Robux").ID;
			break;
		case "com.roblox.client.robux450bc":
			productId = Product.Get("450 Robux").ID;
			break;
		case "com.roblox.client.robux800":
		case "com.roblox.client.robux800nonbc":
			productId = Product.Get("800 Robux").ID;
			break;
		case "com.roblox.client.robux1000":
		case "com.roblox.client.robux1000bc":
			productId = Product.Get("1000 Robux").ID;
			break;
		case "com.roblox.client.robux2000":
			productId = Product.Get("2000 Robux").ID;
			break;
		case "com.roblox.client.robux2750bc":
			productId = Product.Get("2750 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium80robux":
			productId = Product.Get("Premium 80 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium88subscribed":
			productId = Product.Get("Premium 88 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium160robux":
			productId = Product.Get("Premium 160 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium175subscribed":
			productId = Product.Get("Premium 175 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium240robux":
			productId = Product.Get("Premium 240 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium265subscribed":
			productId = Product.Get("Premium 265 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium320robux":
			productId = Product.Get("Premium 320 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium350subscribed":
			productId = Product.Get("Premium 350 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium400robux":
			productId = Product.Get("Premium 400 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium440subscribed2":
			productId = Product.Get("Premium 440 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium800robux":
			productId = Product.Get("Premium 800 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium880subscribed":
			productId = Product.Get("Premium 880 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.premium1700robux":
			productId = Product.Get("Premium 1700 Robux").ID;
			break;
		case "com.roblox.robloxmobile.premium1870subscribed":
			productId = Product.Get("Premium 1870 Subscribed").ID;
			break;
		case "com.roblox.robloxmobile.robloxpremium450onemonth":
			productId = Product.Get("Roblox Premium 450 One Month").ID;
			break;
		case "com.roblox.robloxmobile.robloxpremium1000onemonth":
			productId = Product.Get("Roblox Premium 1000 One Month").ID;
			break;
		case "com.roblox.robloxmobile.robloxpremium2200onemonth":
			productId = Product.Get("Roblox Premium 2200 One Month").ID;
			break;
		default:
			throw new InvalidAmazonProductException(amazonProductId);
		}
		return ProductFactory.Singleton.GetProduct(productId);
	}
}
