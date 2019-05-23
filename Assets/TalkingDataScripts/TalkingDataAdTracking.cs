using UnityEngine;
using System.Runtime.InteropServices;


public class TalkingDataAdTracking
{
	const string version = "4.0.0";
	
#if UNITY_ANDROID
	private static string AD_TRACKING_CLASS = "com.tendcloud.appcpa.TalkingDataAppCpa";
	private static string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
	
	private static AndroidJavaClass adTrackingClass;
	private static AndroidJavaClass unityPlayerClass;
#endif
	
#if UNITY_IPHONE
	/* Interface to native implementation */
	
	[DllImport ("__Internal")] 
	private static extern void tdatSetVerboseLogDisable();
	
	[DllImport ("__Internal")]
	private static extern void tdatBackgroundSessionEnabled();
	
	[DllImport ("__Internal")]
	private static extern void tdatEnableSFSafariViewControllerTracking();
	
	[DllImport ("__Internal")]
	private static extern void tdatInit(string appId, string channelId);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnReceiveDeepLink(string url);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnAdSearch(string adSearchJson);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnRegister(string accountId);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnLogin(string accountId);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCreateRole(string name);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnPay(string account, string orderId, int amount, string currencyType, string payType);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnPlaceOrder(string account, string orderJson);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnOrderPaySucc(string account, string orderId, int amount, string currencyType, string payType);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnViewItem(string itemId, string category, string name, int unitPrice);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnAddItemToShoppingCart(string item, string category, string name, int unitPrice, int amount);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnViewShoppingCart(string shoppingCartJson);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent1();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent2();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent3();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent4();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent5();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent6();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent7();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent8();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent9();
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCustEvent10();
#endif
	
	/* Public interface for use inside C# / JS code */
	
	public static void SetVerboseLogDisable()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (adTrackingClass == null)
			{
				adTrackingClass = new AndroidJavaClass(AD_TRACKING_CLASS);
			}
			adTrackingClass.CallStatic("setVerboseLogDisable");
#endif
#if UNITY_IPHONE
			tdatSetVerboseLogDisable();
#endif
		}
	}
	
	public static void BackgroundSessionEnabled()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_IPHONE
			tdatBackgroundSessionEnabled();
#endif
		}
	}
	
	public static void EnableSFSafariViewControllerTracking()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_IPHONE
			tdatEnableSFSafariViewControllerTracking();
#endif
		}
	}
	
	public static void Init(string appId, string channelId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			Debug.Log("TalkingData Ad Tracking Unity3d SDK version is " + version);
#if UNITY_ANDROID
			if (adTrackingClass == null)
			{
				adTrackingClass = new AndroidJavaClass(AD_TRACKING_CLASS);
			}
			if (unityPlayerClass == null)
			{
				unityPlayerClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
			}
			AndroidJavaObject currActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
			Debug.Log("Android start");
			adTrackingClass.CallStatic("init", currActivity, appId, channelId);
			adTrackingClass.CallStatic("onResume", currActivity);
#endif
#if UNITY_IPHONE
			Debug.Log("iOS start");
			tdatInit(appId, channelId);
#endif
		}
	}
	
	public static void OnReceiveDeepLink(string url)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onReceiveDeepLink", url);
#endif
#if UNITY_IPHONE
			tdatOnReceiveDeepLink(url);
#endif
		}
	}
	
	public static void OnAdSearch(TDAdSearch adSearch)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onAdSearch", adSearch.javaObj);
#endif
#if UNITY_IPHONE
			tdatOnAdSearch(adSearch.ToString());
#endif
		}
	}
	
	public static void OnRegister(string account)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onRegister", account);
#endif
#if UNITY_IPHONE
			tdatOnRegister(account);
#endif
		}
	}
	
	public static void OnLogin(string account)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onLogin", account);
#endif
#if UNITY_IPHONE
			tdatOnLogin(account);
#endif
		}
	}
	
	public static void OnCreateRole(string name)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCreateRole", name);
#endif
#if UNITY_IPHONE
			tdatOnCreateRole(name);
#endif
		}
	}
	
	public static void OnPay(string account, string orderId, int amount, string currencyType, string payType)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onPay", account, orderId, amount, currencyType, payType);
#endif
#if UNITY_IPHONE
			tdatOnPay(account, orderId, amount, currencyType, payType);
#endif
		}
	}
	
	public static void OnPlaceOrder(string account, TDOrder order)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onPlaceOrder", account, order.javaObj);
#endif
#if UNITY_IPHONE
			tdatOnPlaceOrder(account, order.ToString());
#endif
		}
	}
	
	public static void OnOrderPaySucc(string account, string orderId, int amount, string currencyType, string payType)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onOrderPaySucc", account, orderId, amount, currencyType, payType);
#endif
#if UNITY_IPHONE
			tdatOnOrderPaySucc(account, orderId, amount, currencyType, payType);
#endif
		}
	}
	
	public static void OnViewItem(string itemId, string category, string name, int unitPrice)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onViewItem", itemId, category, name, unitPrice);
#endif
#if UNITY_IPHONE
			tdatOnViewItem(itemId, category, name, unitPrice);
#endif
		}
	}
	
	public static void OnAddItemToShoppingCart(string itemId, string category, string name, int unitPrice, int amount)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onAddItemToShoppingCart", itemId, category, name, unitPrice, amount);
#endif
#if UNITY_IPHONE
			tdatOnAddItemToShoppingCart(itemId, category, name, unitPrice, amount);
#endif
		}
	}
	
	public static void OnViewShoppingCart(TDShoppingCart shoppingCart)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onViewShoppingCart", shoppingCart.javaObj);
#endif
#if UNITY_IPHONE
			tdatOnViewShoppingCart(shoppingCart.ToString());
#endif
		}
	}
	
	public static void OnCustEvent1()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent1");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent1();
#endif
		}
	}
	
	public static void OnCustEvent2()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent2");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent2();
#endif
		}
	}
	
	public static void OnCustEvent3()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent3");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent3();
#endif
		}
	}
	
	public static void OnCustEvent4()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent4");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent4();
#endif
		}
	}
	
	public static void OnCustEvent5()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent5");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent5();
#endif
		}
	}
	
	public static void OnCustEvent6()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent6");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent6();
#endif
		}
	}
	
	public static void OnCustEvent7()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent7");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent7();
#endif
		}
	}
	
	public static void OnCustEvent8()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent8");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent8();
#endif
		}
	}
	
	public static void OnCustEvent9()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent9");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent9();
#endif
		}
	}
	
	public static void OnCustEvent10()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCustEvent10");
#endif
#if UNITY_IPHONE
			tdatOnCustEvent10();
#endif
		}
	}
}
