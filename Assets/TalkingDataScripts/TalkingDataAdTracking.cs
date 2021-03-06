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
    private static extern void TDATOnRegister(string profileId, string invitationCode);

    [DllImport("__Internal")]
    private static extern void TDATOnLogin(string profileId);

    [DllImport("__Internal")]
    private static extern void TDATOnCreateCard(string profile, string method, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnReceiveDeepLink(string url);

    [DllImport("__Internal")]
    private static extern void TDATOnFavorite(string category, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnShare(string profile, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnPunch(string profile, string punchId);

    [DllImport("__Internal")]
    private static extern void TDATOnSearch(string searchJson);

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnContact(string profile, string content);
#endif

#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnPay(string profile, string orderId, int amount, string currencyType, string payType);
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnChargeBack(string profile, string orderId, string reason, string type);
#endif

#if TDAT_FINANCE || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnReservation(string profile, string reservationId, string category, int amount, string term);
#endif

#if TDAT_RETAIL || TDAT_TOUR
    [DllImport("__Internal")]
    private static extern void TDATOnBooking(string profile, string bookingId, string category, int amount, string content);
#endif

#if TDAT_RETAIL
    [DllImport("__Internal")]
    private static extern void TDATOnViewItem(string itemId, string category, string name, int unitPrice);

    [DllImport("__Internal")]
    private static extern void TDATOnAddItemToShoppingCart(string item, string category, string name, int unitPrice, int amount);

    [DllImport("__Internal")]
    private static extern void TDATOnViewShoppingCart(string shoppingCartJson);

    [DllImport("__Internal")]
    private static extern void TDATOnPlaceOrder(string profile, string orderJson);

    [DllImport("__Internal")]
    private static extern void TDATOnOrderPaySucc(string profile, string orderId, int amount, string currencyType, string payType);
#endif

#if TDAT_FINANCE
    [DllImport("__Internal")]
    private static extern void TDATOnCredit(string profile, int amount, string content);

    [DllImport("__Internal")]
    private static extern void TDATOnTransaction(string profile, string transactionJson);
#endif

#if TDAT_GAME
    [DllImport("__Internal")]
    private static extern void TDATOnCreateRole(string name);

    [DllImport("__Internal")]
    private static extern void TDATOnLevelPass(string profile, string levelId);

    [DllImport("__Internal")]
    private static extern void TDATOnGuideFinished(string profile, string content);
#endif

#if TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnLearn(string profile, string course, long begin, int duration);

    [DllImport("__Internal")]
    private static extern void TDATOnPreviewFinished(string profile, string content);
#endif

#if TDAT_READING
    [DllImport("__Internal")]
    private static extern void TDATOnRead(string profile, string book, long begin, int duration);

    [DllImport("__Internal")]
    private static extern void TDATOnFreeFinished(string profile, string content);
#endif

#if TDAT_GAME || TDAT_ONLINEEDU
    [DllImport("__Internal")]
    private static extern void TDATOnAchievementUnlock(string profile, string achievementId);
#endif

#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnBrowse(string profile, string content, long begin, int duration);
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    [DllImport("__Internal")]
    private static extern void TDATOnTrialFinished(string profile, string content);
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

    public static void OnRegister(string profile, string invitationCode = null)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onRegister", profile, invitationCode);
            }
#endif
#if UNITY_IPHONE
            TDATOnRegister(profile, invitationCode);
#endif
        }
    }

    public static void OnLogin(string profile)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLogin", profile);
            }
#endif
#if UNITY_IPHONE
            TDATOnLogin(profile);
#endif
        }
    }

    public static void OnCreateCard(string profile, string method, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCreateCard", profile, method, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnCreateCard(profile, method, content);
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

    public static void OnShare(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onShare", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnShare(profile, content);
#endif
        }
    }

    public static void OnPunch(string profile, string punchId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPunch", profile, punchId);
            }
#endif
#if UNITY_IPHONE
            TDATOnPunch(profile, punchId);
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
    public static void OnContact(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onContact", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnContact(profile, content);
#endif
        }
    }
#endif

#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
    public static void OnPay(string profile, string orderId, int amount, string currencyType, string payType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPay", profile, orderId, amount, currencyType, payType);
            }
#endif
#if UNITY_IPHONE
            TDATOnPay(profile, orderId, amount, currencyType, payType);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
    public static void OnChargeBack(string profile, string orderId, string reason, string type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onChargeBack", profile, orderId, reason, type);
            }
#endif
#if UNITY_IPHONE
            TDATOnChargeBack(profile, orderId, reason, type);
#endif
        }
    }
#endif

#if TDAT_FINANCE || TDAT_ONLINEEDU
    public static void OnReservation(string profile, string reservationId, string category, int amount, string term)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onReservation", profile, reservationId, category, amount, term);
            }
#endif
#if UNITY_IPHONE
            TDATOnReservation(profile, reservationId, category, amount, term);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_TOUR
    public static void OnBooking(string profile, string bookingId, string category, int amount, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onBooking", profile, bookingId, category, amount, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnBooking(profile, bookingId, category, amount, content);
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

    public static void OnPlaceOrder(string profile, TDOrder order)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPlaceOrder", profile, order.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnPlaceOrder(profile, order.ToString());
#endif
        }
    }

    public static void OnOrderPaySucc(string profile, string orderId, int amount, string currencyType, string payType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onOrderPaySucc", profile, orderId, amount, currencyType, payType);
            }
#endif
#if UNITY_IPHONE
            TDATOnOrderPaySucc(profile, orderId, amount, currencyType, payType);
#endif
        }
    }
#endif

#if TDAT_FINANCE
    public static void OnCredit(string profile, int amount, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onCredit", profile, amount, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnCredit(profile, amount, content);
#endif
        }
    }

    public static void OnTransaction(string profile, TDTransaction transaction)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onTransaction", profile, transaction.javaObj);
            }
#endif
#if UNITY_IPHONE
            TDATOnTransaction(profile, transaction.ToString());
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

    public static void OnLevelPass(string profile, string levelId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLevelPass", profile, levelId);
            }
#endif
#if UNITY_IPHONE
            TDATOnLevelPass(profile, levelId);
#endif
        }
    }

    public static void OnGuideFinished(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onGuideFinished", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnGuideFinished(profile, content);
#endif
        }
    }
#endif

#if TDAT_ONLINEEDU
    public static void OnLearn(string profile, string course, long begin, int duration)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onLearn", profile, course, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnLearn(profile, course, begin, duration);
#endif
        }
    }

    public static void OnPreviewFinished(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onPreviewFinished", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnPreviewFinished(profile, content);
#endif
        }
    }
#endif

#if TDAT_READING
    public static void OnRead(string profile, string book, long begin, int duration)
     {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onRead", profile, book, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnRead(profile, book, begin, duration);
#endif
        }
    }

    public static void OnFreeFinished(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onFreeFinished", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnFreeFinished(profile, content);
#endif
        }
    }
#endif

#if TDAT_GAME || TDAT_ONLINEEDU
    public static void OnAchievementUnlock(string profile, string achievementId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onAchievementUnlock", profile, achievementId);
            }
#endif
#if UNITY_IPHONE
            TDATOnAchievementUnlock(profile, achievementId);
#endif
        }
    }
#endif

#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    public static void OnBrowse(string profile, string content, long begin, int duration)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onBrowse", profile, content, begin, duration);
            }
#endif
#if UNITY_IPHONE
            TDATOnBrowse(profile, content, begin, duration);
#endif
        }
    }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
    public static void OnTrialFinished(string profile, string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (adTrackingClass != null)
            {
                adTrackingClass.CallStatic("onTrialFinished", profile, content);
            }
#endif
#if UNITY_IPHONE
            TDATOnTrialFinished(profile, content);
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
