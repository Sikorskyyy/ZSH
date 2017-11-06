/*using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Assets.Scripts.Common;

public class APIController : MonoBehaviour,  IStoreListener
{
	public static APIController instance;

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	public static string Poduct_PROTEIN_3000 = "protein.3k";   
	public static string Poduct_PROTEIN_7000 = "protein.7k";
	public static string Poduct_PROTEIN_16000 = "protein.16k";
	public static string Poduct_PROTEIN_30000 = "protein.30k";
	public static string Poduct_NO_ADS = "no.ad";  

	// Apple App Store-specific product identifier for the subscription product.
	//private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";

	public void Start()
	{
		if(instance == null)
			instance = this;
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(Poduct_PROTEIN_3000, ProductType.Consumable);
		builder.AddProduct(Poduct_PROTEIN_7000, ProductType.Consumable);
		builder.AddProduct(Poduct_PROTEIN_16000, ProductType.Consumable);
		builder.AddProduct(Poduct_PROTEIN_30000, ProductType.Consumable);
		builder.AddProduct(Poduct_NO_ADS, ProductType.NonConsumable);

		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyProtein3000()
	{
		BuyProductID(Poduct_PROTEIN_3000);
	}


	public void BuyProtein7000()
	{
		BuyProductID(Poduct_PROTEIN_7000);
	}

	public void BuyProtein16000()
	{
		BuyProductID(Poduct_PROTEIN_16000);
	}

	public void BuyProtein30000()
	{
		BuyProductID(Poduct_PROTEIN_30000);
	}

	public void BuyNoAds()
	{
		BuyProductID(Poduct_NO_ADS);
	}


	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}
		
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		if (String.Equals(args.purchasedProduct.definition.id, Poduct_PROTEIN_3000 , StringComparison.Ordinal))
		{
			PlayerData.SetCoins (Encryptor.Encode((float.Parse (Encryptor.Decode (PlayerData.GetCoins ()))+3000).ToString()));
		}

		else if (String.Equals(args.purchasedProduct.definition.id, Poduct_PROTEIN_7000, StringComparison.Ordinal))
		{
			PlayerData.SetCoins (Encryptor.Encode((float.Parse (Encryptor.Decode (PlayerData.GetCoins ()))+7000).ToString()));
		}
		// Or ... a subscription product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, Poduct_PROTEIN_16000, StringComparison.Ordinal))
		{
			PlayerData.SetCoins (Encryptor.Encode((float.Parse (Encryptor.Decode (PlayerData.GetCoins ()))+16000).ToString()));
		}
		else if (String.Equals(args.purchasedProduct.definition.id, Poduct_PROTEIN_30000, StringComparison.Ordinal))
		{
			//Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerData.SetCoins (Encryptor.Encode((float.Parse (Encryptor.Decode (PlayerData.GetCoins ()))+30000).ToString()));
		}
		else if (String.Equals(args.purchasedProduct.definition.id, Poduct_NO_ADS, StringComparison.Ordinal))
		{
			PlayerPrefs.SetString("NoAdsIsBuyed", "yes");
			MainMenuButtons.noAdBtn.SetActive (false);
		}
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		updateProtCount.Instance.UpdateText ();
		return PurchaseProcessingResult.Complete;
	}
		
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}*/
