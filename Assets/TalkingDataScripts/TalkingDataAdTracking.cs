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
	private static extern void tdatInit(string appId, string channelId, string custom);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnRegister(string accountId, string invitationCode);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnLogin(string accountId);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnCreateCard(string account, string method, string content);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnReceiveDeepLink(string url);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnFavorite(string category, string content);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnShare(string account, string content);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnPunch(string account, string punchId);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnSearch(string searchJson);
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
	[DllImport("__Internal")]
	private static extern void tdatOnContact(string account, string content);
#endif
	
#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
	[DllImport("__Internal")]
	private static extern void tdatOnPay(string account, string orderId, int amount, string currencyType, string payType);
#endif
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
	[DllImport("__Internal")]
	private static extern void tdatOnChargeBack(string account, string orderId, string reason, string type);
#endif
	
#if TDAT_FINANCE || TDAT_ONLINEEDU
	[DllImport ("__Internal")]
	private static extern void tdatOnReservation(string account, string reservationId, string category, int amount, string term);
#endif
	
#if TDAT_RETAIL || TDAT_TOUR
	[DllImport ("__Internal")]
	private static extern void tdatOnBooking(string account, string bookingId, string category, int amount, string content);
#endif
	
#if TDAT_RETAIL
	[DllImport ("__Internal")]
	private static extern void tdatOnViewItem(string itemId, string category, string name, int unitPrice);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnAddItemToShoppingCart(string item, string category, string name, int unitPrice, int amount);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnViewShoppingCart(string shoppingCartJson);
	
	[DllImport ("__Internal")]
	private static extern void tdatOnPlaceOrder(string account, string orderJson);
	
	[DllImport("__Internal")]
	private static extern void tdatOnOrderPaySucc(string account, string orderId, int amount, string currencyType, string payType);
#endif
	
#if TDAT_FINANCE
	[DllImport("__Internal")]
	private static extern void tdatOnCredit(string account, int amount, string content);
	
	[DllImport("__Internal")]
	private static extern void tdatOnTransaction(string account, string transactionJson);
#endif
	
#if TDAT_GAME
	[DllImport("__Internal")]
	private static extern void tdatOnCreateRole(string name);
	
	[DllImport("__Internal")]
	private static extern void tdatOnLevelPass(string account, string levelId);
	
	[DllImport("__Internal")]
	private static extern void tdatOnGuideFinished(string account, string content);
#endif
	
#if TDAT_ONLINEEDU
	[DllImport ("__Internal")]
	private static extern void tdatOnLearn(string account, string course, long begin, int duration);
	
	[DllImport("__Internal")]
	private static extern void tdatOnPreviewFinished(string account, string content);
#endif
	
#if TDAT_READING
	[DllImport ("__Internal")]
	private static extern void tdatOnRead(string account, string book, long begin, int duration);
	
	[DllImport("__Internal")]
	private static extern void tdatOnFreeFinished(string account, string content);
#endif
	
#if TDAT_GAME || TDAT_ONLINEEDU
	[DllImport("__Internal")]
	private static extern void tdatOnAchievementUnlock(string account, string achievementId);
#endif
	
#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
	[DllImport ("__Internal")]
	private static extern void tdatOnBrowse(string account, string content, long begin, int duration);
#endif
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
	[DllImport ("__Internal")]
	private static extern void tdatOnTrialFinished(string account, string content);
#endif
	
#if TDAT_CUSTOM
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
	
	public static void Init(string appId, string channelId, string custom = null)
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
			adTrackingClass.CallStatic("init", currActivity, appId, channelId, custom);
			adTrackingClass.CallStatic("onResume", currActivity);
#endif
#if UNITY_IPHONE
			Debug.Log("iOS start");
			tdatInit(appId, channelId, custom);
#endif
		}
	}
	
	public static void OnRegister(string account, string invitationCode = null)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onRegister", account, invitationCode);
#endif
#if UNITY_IPHONE
			tdatOnRegister(account, invitationCode);
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
	
	public static void OnCreateCard(string account, string method, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCreateCard", account, method, content);
#endif
#if UNITY_IPHONE
			tdatOnCreateCard(account, method, content);
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
	
	public static void OnFavorite(string category, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onFavorite", category, content);
#endif
#if UNITY_IPHONE
			tdatOnFavorite(category, content);
#endif
		}
	}
	
	public static void OnShare(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onShare", account, content);
#endif
#if UNITY_IPHONE
			tdatOnShare(account, content);
#endif
		}
	}
	
	public static void OnPunch(string account, string punchId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onPunch", account, punchId);
#endif
#if UNITY_IPHONE
			tdatOnPunch(account, punchId);
#endif
		}
	}
	
	public static void OnSearch(TDSearch search)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onSearch", search.javaObj);
#endif
#if UNITY_IPHONE
			tdatOnSearch(search.ToString());
#endif
		}
	}
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
	public static void OnContact(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onContact", account, content);
#endif
#if UNITY_IPHONE
			tdatOnContact(account, content);
#endif
		}
	}
#endif
	
#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
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
#endif
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
	public static void OnChargeBack(string account, string orderId, string reason, string type)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onChargeBack", account, orderId, reason, type);
#endif
#if UNITY_IPHONE
			tdatOnChargeBack(account, orderId, reason, type);
#endif
		}
	}
#endif
	
#if TDAT_FINANCE || TDAT_ONLINEEDU
	public static void OnReservation(string account, string reservationId, string category, int amount, string term)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onReservation", account, reservationId, category, amount, term);
#endif
#if UNITY_IPHONE
			tdatOnReservation(account, reservationId, category, amount, term);
#endif
		}
	}
#endif
	
#if TDAT_RETAIL || TDAT_TOUR
	public static void OnBooking(string account, string bookingId, string category, int amount, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onBooking", account, bookingId, category, amount, content);
#endif
#if UNITY_IPHONE
			tdatOnBooking(account, bookingId, category, amount, content);
#endif
		}
	}
#endif
	
#if TDAT_RETAIL
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
#endif
	
#if TDAT_FINANCE
	public static void OnCredit(string account, int amount, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onCredit", account, amount, content);
#endif
#if UNITY_IPHONE
			tdatOnCredit(account, amount, content);
#endif
		}
	}
	
	public static void OnTransaction(string account, TDTransaction transaction)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onTransaction", account, transaction.javaObj);
#endif
#if UNITY_IPHONE
			tdatOnTransaction(account, transaction.ToString());
#endif
		}
	}
#endif
	
#if TDAT_GAME
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
	
	public static void OnLevelPass(string account, string levelId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onLevelPass", account, levelId);
#endif
#if UNITY_IPHONE
			tdatOnLevelPass(account, levelId);
#endif
		}
	}
	
	public static void OnGuideFinished(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onGuideFinished", account, content);
#endif
#if UNITY_IPHONE
			tdatOnGuideFinished(account, content);
#endif
		}
	}
#endif
	
#if TDAT_ONLINEEDU
	public static void OnLearn(string account, string course, long begin, int duration)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onLearn", account, course, begin, duration);
#endif
#if UNITY_IPHONE
			tdatOnLearn(account, course, begin, duration);
#endif
		}
	}
	
	public static void OnPreviewFinished(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onPreviewFinished", account, content);
#endif
#if UNITY_IPHONE
			tdatOnPreviewFinished(account, content);
#endif
		}
	}
#endif
	
#if TDAT_READING
	public static void OnRead(string account, string book, long begin, int duration)
	 {
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onRead", account, book, begin, duration);
#endif
#if UNITY_IPHONE
			tdatOnRead(account, book, begin, duration);
#endif
		}
	}
	
	public static void OnFreeFinished(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onFreeFinished", account, content);
#endif
#if UNITY_IPHONE
			tdatOnFreeFinished(account, content);
#endif
		}
	}
#endif
	
#if TDAT_GAME || TDAT_ONLINEEDU
	public static void OnAchievementUnlock(string account, string achievementId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onAchievementUnlock", account, achievementId);
#endif
#if UNITY_IPHONE
			tdatOnAchievementUnlock(account, achievementId);
#endif
		}
	}
#endif
	
#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
	public static void OnBrowse(string account, string content, long begin, int duration)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onBrowse", account, content, begin, duration);
#endif
#if UNITY_IPHONE
			tdatOnBrowse(account, content, begin, duration);
#endif
		}
	}
#endif
	
#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
	public static void OnTrialFinished(string account, string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			adTrackingClass.CallStatic("onTrialFinished", account, content);
#endif
#if UNITY_IPHONE
			tdatOnTrialFinished(account, content);
#endif
		}
	}
#endif
	
#if TDAT_CUSTOM
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
#endif
}
