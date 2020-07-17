using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public static class TalkingDataAdTracking
{
#if UNITY_ANDROID
    private static readonly string AD_TRACKING_CLASS = "com.tendcloud.appcpa.TalkingDataAppCpa";
    private static AndroidJavaClass adTrackingClass;
    private static AndroidJavaClass unityPlayerClass;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern string TDATGetDeviceId();

    [DllImport("__Internal")] 
    private static extern void TDATSetVerboseLogDisable();

    [DllImport("__Internal")]
    private static extern void TDATBackgroundSessionEnabled();

    [DllImport("__Internal")]
    private static extern void TDATEnableSFSafariViewControllerTracking();

    [DllImport("__Internal")]
    private static extern void TDATInit(string appId, string channelId, string custom);

    [DllImport("__Internal")]
    private static extern void TDATOnRegister(string accountId, string invitationCode);

    [DllImport("__Internal")]
    private static extern void TDATOnLogin(string accountId);

    [DllImport("__Internal")]
    private static extern void TDATOnCreateCard(string account, string method, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnReceiveDeepLink(string url);

    [DllImport("__Internal")]
    private static extern void TDATOnFavorite(string category, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnShare(string account, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnPunch(string account, string punchId);

    [DllImport("__Internal")]
    private static extern void TDATOnSearch(string searchJson);

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnContact(string account, string content);
#endif

#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnPay(string account, string orderId, int amount, string currencyType, string payType);
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnChargeBack(string account, string orderId, string reason, string type);
#endif

#if TDAT_FINANCE || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnReservation(string account, string reservationId, string category, int amount, string term);
#endif

#if TDAT_RETAIL || TDAT_TOUR
    [DllImport("__Internal")]
    private static extern void TDATOnBooking(string account, string bookingId, string category, int amount, string content);
#endif

#if TDAT_RETAIL
    [DllImport("__Internal")]
    private static extern void TDATOnViewItem(string itemId, string category, string name, int unitPrice);

    [DllImport("__Internal")]
    private static extern void TDATOnAddItemToShoppingCart(string item, string category, string name, int unitPrice, int amount);

    [DllImport("__Internal")]
    private static extern void TDATOnViewShoppingCart(string shoppingCartJson);

    [DllImport("__Internal")]
    private static extern void TDATOnPlaceOrder(string account, string orderJson);

    [DllImport("__Internal")]
    private static extern void TDATOnOrderPaySucc(string account, string orderId, int amount, string currencyType, string payType);
#endif

#if TDAT_FINANCE
    [DllImport("__Internal")]
    private static extern void TDATOnCredit(string account, int amount, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnTransaction(string account, string transactionJson);
#endif

#if TDAT_GAME
    [DllImport("__Internal")]
    private static extern void TDATOnCreateRole(string name);

    [DllImport("__Internal")]
    private static extern void TDATOnLevelPass(string account, string levelId);

    [DllImport("__Internal")]
    private static extern void TDATOnGuideFinished(string account, string content);
#endif

#if TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnLearn(string account, string course, long begin, int duration);

    [DllImport("__Internal")]
    private static extern void TDATOnPreviewFinished(string account, string content);
#endif

#if TDAT_READING
    [DllImport("__Internal")]
    private static extern void TDATOnRead(string account, string book, long begin, int duration);

    [DllImport("__Internal")]
    private static extern void TDATOnFreeFinished(string account, string content);
#endif

#if TDAT_GAME || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnAchievementUnlock(string account, string achievementId);
#endif

#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnBrowse(string account, string content, long begin, int duration);
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnTrialFinished(string account, string content);
#endif

#if TDAT_CUSTOM
    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent1();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent2();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent3();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent4();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent5();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent6();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent7();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent8();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent9();

    [DllImport("__Internal")]
    private static extern void TDATOnCustEvent10();
#endif
#endif

#if UNITY_ANDROID
    private static AndroidJavaObject GetCurrentActivity()
    {
        if (unityPlayerClass == null)
        {
            unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        }
        AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        return activity;
    }
#endif

    private static string deviceId = null;
    public static string GetDeviceId()
    {
        if (deviceId == null && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass == null)
            {
                adTrackingClass = new AndroidJavaClass(AD_TRACKING_CLASS);
            }
            deviceId = adTrackingClass.CallStatic<string>("getDeviceId", GetCurrentActivity());
#endif
#if UNITY_IPHONE
            deviceId = TDATGetDeviceId();
#endif
        }
        return deviceId;
    }

    private static string oaid = null;
    public static string GetOAID()
    {
        if (oaid == null && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass == null)
            {
                adTrackingClass = new AndroidJavaClass(AD_TRACKING_CLASS);
            }
            oaid = adTrackingClass.CallStatic<string>("getOAID", GetCurrentActivity());
#endif
        }
        return oaid;
    }

    public static void SetVerboseLogDisable()
    {
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
            TDATSetVerboseLogDisable();
#endif
        }
    }

    public static void BackgroundSessionEnabled()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            TDATBackgroundSessionEnabled();
#endif
        }
    }

    public static void EnableSFSafariViewControllerTracking()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            TDATEnableSFSafariViewControllerTracking();
#endif
        }
    }

    public static void Init(string appId, string channelId, string custom = null)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Debug.Log("TalkingData AD Tracking Unity SDK.");
#if UNITY_ANDROID
            using (AndroidJavaClass dz = new AndroidJavaClass("com.talkingdata.sdk.dz"))
            {
                dz.SetStatic("a", 2);
            }
            if (adTrackingClass == null)
            {
                adTrackingClass = new AndroidJavaClass(AD_TRACKING_CLASS);
            }
            AndroidJavaObject activity = GetCurrentActivity();
            adTrackingClass.CallStatic("init", activity, appId, channelId, custom);
            adTrackingClass.CallStatic("onResume", activity);
#endif
#if UNITY_IPHONE
            TDATInit(appId, channelId, custom);
#endif
        }
    }

    public static void OnRegister(string account, string invitationCode = null)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onRegister", account, invitationCode);
            }
#endif
#if UNITY_IPHONE
            TDATOnRegister(account, invitationCode);
#endif
        }
    }

    public static void OnLogin(string account)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLogin", account);
            }
#endif
#if UNITY_IPHONE
            TDATOnLogin(account);
#endif
        }
    }

    public static void OnCreateCard(string account, string method, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCreateCard", account, method, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnCreateCard(account, method, content);
#endif
        }
    }

    public static void OnReceiveDeepLink(string url)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onReceiveDeepLink", url);
            }
#endif
#if UNITY_IPHONE
            TDATOnReceiveDeepLink(url);
#endif
        }
    }

    public static void OnFavorite(string category, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onFavorite", category, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnFavorite(category, content);
#endif
        }
    }

    public static void OnShare(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onShare", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnShare(account, content);
#endif
        }
    }

    public static void OnPunch(string account, string punchId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPunch", account, punchId);
            }
#endif
#if UNITY_IPHONE
            TDATOnPunch(account, punchId);
#endif
        }
    }

    public static void OnSearch(TDSearch search)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onSearch", search.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnSearch(search.ToString());
#endif
        }
    }

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    public static void OnContact(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onContact", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnContact(account, content);
#endif
        }
    }
#endif

#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
    public static void OnPay(string account, string orderId, int amount, string currencyType, string payType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPay", account, orderId, amount, currencyType, payType);
            }
#endif
#if UNITY_IPHONE
            TDATOnPay(account, orderId, amount, currencyType, payType);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    public static void OnChargeBack(string account, string orderId, string reason, string type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onChargeBack", account, orderId, reason, type);
            }
#endif
#if UNITY_IPHONE
            TDATOnChargeBack(account, orderId, reason, type);
#endif
        }
    }
#endif

#if TDAT_FINANCE || TDAT_ONLINEEDU
    public static void OnReservation(string account, string reservationId, string category, int amount, string term)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onReservation", account, reservationId, category, amount, term);
            }
#endif
#if UNITY_IPHONE
            TDATOnReservation(account, reservationId, category, amount, term);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_TOUR
    public static void OnBooking(string account, string bookingId, string category, int amount, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onBooking", account, bookingId, category, amount, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnBooking(account, bookingId, category, amount, content);
#endif
        }
    }
#endif

#if TDAT_RETAIL
    public static void OnViewItem(string itemId, string category, string name, int unitPrice)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onViewItem", itemId, category, name, unitPrice);
            }
#endif
#if UNITY_IPHONE
            TDATOnViewItem(itemId, category, name, unitPrice);
#endif
        }
    }

    public static void OnAddItemToShoppingCart(string itemId, string category, string name, int unitPrice, int amount)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onAddItemToShoppingCart", itemId, category, name, unitPrice, amount);
            }
#endif
#if UNITY_IPHONE
            TDATOnAddItemToShoppingCart(itemId, category, name, unitPrice, amount);
#endif
        }
    }

    public static void OnViewShoppingCart(TDShoppingCart shoppingCart)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onViewShoppingCart", shoppingCart.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnViewShoppingCart(shoppingCart.ToString());
#endif
        }
    }

    public static void OnPlaceOrder(string account, TDOrder order)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPlaceOrder", account, order.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnPlaceOrder(account, order.ToString());
#endif
        }
    }

    public static void OnOrderPaySucc(string account, string orderId, int amount, string currencyType, string payType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onOrderPaySucc", account, orderId, amount, currencyType, payType);
            }
#endif
#if UNITY_IPHONE
            TDATOnOrderPaySucc(account, orderId, amount, currencyType, payType);
#endif
        }
    }
#endif

#if TDAT_FINANCE
    public static void OnCredit(string account, int amount, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCredit", account, amount, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnCredit(account, amount, content);
#endif
        }
    }

    public static void OnTransaction(string account, TDTransaction transaction)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onTransaction", account, transaction.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnTransaction(account, transaction.ToString());
#endif
        }
    }
#endif

#if TDAT_GAME
    public static void OnCreateRole(string name)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCreateRole", name);
            }
#endif
#if UNITY_IPHONE
            TDATOnCreateRole(name);
#endif
        }
    }

    public static void OnLevelPass(string account, string levelId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLevelPass", account, levelId);
            }
#endif
#if UNITY_IPHONE
            TDATOnLevelPass(account, levelId);
#endif
        }
    }

    public static void OnGuideFinished(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onGuideFinished", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnGuideFinished(account, content);
#endif
        }
    }
#endif

#if TDAT_ONLINEEDU
    public static void OnLearn(string account, string course, long begin, int duration)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLearn", account, course, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnLearn(account, course, begin, duration);
#endif
        }
    }

    public static void OnPreviewFinished(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPreviewFinished", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnPreviewFinished(account, content);
#endif
        }
    }
#endif

#if TDAT_READING
    public static void OnRead(string account, string book, long begin, int duration)
     {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onRead", account, book, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnRead(account, book, begin, duration);
#endif
        }
    }

    public static void OnFreeFinished(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onFreeFinished", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnFreeFinished(account, content);
#endif
        }
    }
#endif

#if TDAT_GAME || TDAT_ONLINEEDU
    public static void OnAchievementUnlock(string account, string achievementId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onAchievementUnlock", account, achievementId);
            }
#endif
#if UNITY_IPHONE
            TDATOnAchievementUnlock(account, achievementId);
#endif
        }
    }
#endif

#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    public static void OnBrowse(string account, string content, long begin, int duration)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onBrowse", account, content, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnBrowse(account, content, begin, duration);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    public static void OnTrialFinished(string account, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onTrialFinished", account, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnTrialFinished(account, content);
#endif
        }
    }
#endif

#if TDAT_CUSTOM
    public static void OnCustEvent1()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent1");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent1();
#endif
        }
    }

    public static void OnCustEvent2()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent2");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent2();
#endif
        }
    }

    public static void OnCustEvent3()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent3");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent3();
#endif
        }
    }

    public static void OnCustEvent4()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent4");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent4();
#endif
        }
    }

    public static void OnCustEvent5()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent5");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent5();
#endif
        }
    }

    public static void OnCustEvent6()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent6");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent6();
#endif
        }
    }

    public static void OnCustEvent7()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent7");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent7();
#endif
        }
    }

    public static void OnCustEvent8()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent8");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent8();
#endif
        }
    }

    public static void OnCustEvent9()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent9");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent9();
#endif
        }
    }

    public static void OnCustEvent10()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCustEvent10");
            }
#endif
#if UNITY_IPHONE
            TDATOnCustEvent10();
#endif
        }
    }
#endif
}
