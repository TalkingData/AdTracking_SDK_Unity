using UnityEngine;

public class TDATDemoScript : MonoBehaviour
{
    private const int top = 100;
    private const int left = 80;
    private const int height = 50;
    private const int spacing = 20;
    private readonly int width = (Screen.width - (left * 2) - spacing) / 2;
    private const int step = 60;
    private string deviceId;
    private string oaid;

    private void OnGUI()
    {
        int i = 0;
        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Demo Menu");

        GUI.Label(new Rect(left, top + (step * i++), width, height), deviceId);
        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "getDeviceId"))
        {
            deviceId = TalkingDataAdTracking.GetDeviceId();
        }

        GUI.Label(new Rect(left, top + (step * i++), width, height), oaid);
        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "getOAID"))
        {
            oaid = TalkingDataAdTracking.GetOAID();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnRegister"))
        {
            TalkingDataAdTracking.OnRegister("user01", "123456");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnLogin"))
        {
            TalkingDataAdTracking.OnLogin("user01");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCreateCard"))
        {
            TalkingDataAdTracking.OnCreateCard("user01", "支付宝", "支付宝账号123456789");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnReceiveDeepLink"))
        {
            TalkingDataAdTracking.OnReceiveDeepLink("https://www.talkingdata.com");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnFavorite"))
        {
            TalkingDataAdTracking.OnFavorite("服装", "2019新款");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnShare"))
        {
            TalkingDataAdTracking.OnShare("user01", "课程");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnPunch"))
        {
            TalkingDataAdTracking.OnPunch("user01", "签到0023");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnSearch"))
        {
            TDSearch search = TDSearch.CreateSearch();
            search.SetCategory("类型");
            search.SetContent("内容");
#if TDAT_RETAIL
            search.SetItemId("商品ID");
            search.SetItemLocationId("location12314");
#endif
#if TDAT_TOUR
            search.SetDestination("目的地");
            search.SetOrigin("出发地");
            search.SetStartDate(1565176907309);
            search.SetEndDate(1565176908309);
#endif
            TalkingDataAdTracking.OnSearch(search);
        }

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnContact"))
        {
            TalkingDataAdTracking.OnContact("user01", "联系平台内容");
        }
#endif

#if TDAT_GAME || TDAT_TOUR || TDAT_ONLINEEDU || TDAT_READING || TDAT_OTHER
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnPay"))
        {
            TalkingDataAdTracking.OnPay("user01", "order02", 1077600, "CNY", "Apple Pay");
        }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_ONLINEEDU
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnChargeBack"))
        {
            TalkingDataAdTracking.OnChargeBack("user01", "order01", "7天无理由退货", "仅退款");
        }
#endif

#if TDAT_FINANCE || TDAT_ONLINEEDU
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnReservation"))
        {
            TalkingDataAdTracking.OnReservation("user01", "AdTracking_123456", "借贷类", 12, "商品信息");
        }
#endif

#if TDAT_RETAIL || TDAT_TOUR
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnBooking"))
        {
            TalkingDataAdTracking.OnBooking("user01", "002391", "电子", 123, "商品信息");
        }
#endif

#if TDAT_RETAIL
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnViewItem"))
        {
            TalkingDataAdTracking.OnViewItem("A1660", "手机", "iPhone 7", 538800);
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnAddItemToShoppingCart"))
        {
            TalkingDataAdTracking.OnAddItemToShoppingCart("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnViewShoppingCart"))
        {
            TDShoppingCart shoppingCart = TDShoppingCart.CreateShoppingCart();
            if (shoppingCart != null)
            {
                shoppingCart.AddItem("A1660", "手机", "iPhone 7", 538800, 2);
                shoppingCart.AddItem("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
                TalkingDataAdTracking.OnViewShoppingCart(shoppingCart);
            }
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnPlaceOrder"))
        {
            TDOrder order = TDOrder.CreateOrder("order01", 2466400, "CNY");
            order.AddItem("A1660", "手机", "iPhone 7", 538800, 2);
            order.AddItem("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
            TalkingDataAdTracking.OnPlaceOrder("user01", order);
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnOrderPaySucc"))
        {
            TalkingDataAdTracking.OnOrderPaySucc("user01", "order01", 2, "CNY", "AliPay");
        }
#endif

#if TDAT_FINANCE
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCredit"))
        {
            TalkingDataAdTracking.OnCredit("user01", 123456, "授信详情为......");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnTransaction"))
        {
            TDTransaction transaction = TDTransaction.CreateTransaction();
            transaction.SetTransactionId("AdTracking_123456");
            transaction.SetCategory("定期");
            transaction.SetAmount(3222);
            transaction.SetPersonA("张三");
            transaction.SetPersonB("金融平台");
            transaction.SetStartDate(1565176907309);
            transaction.SetEndDate(1565176908309);
            transaction.SetCurrencyType("CNY");
            transaction.SetContent("交易详情为......");
            TalkingDataAdTracking.OnTransaction("user01", transaction);
        }
#endif

#if TDAT_GAME
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCreateRole"))
        {
            TalkingDataAdTracking.OnCreateRole("role01");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnLevelPass"))
        {
            TalkingDataAdTracking.OnLevelPass("user01", "AdTracking_123456");
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnGuideFinished"))
        {
            TalkingDataAdTracking.OnGuideFinished("user01", "新手教程顺利通过");
        }
#endif

#if TDAT_ONLINEEDU
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnLearn"))
        {
            TalkingDataAdTracking.OnLearn("user01", "成人教育第一节", 1501234567890, 20);
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnPreviewFinished"))
        {
            TalkingDataAdTracking.OnPreviewFinished("user01", "基础课程试听结束");
        }
#endif

#if TDAT_READING
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnRead"))
        {
            TalkingDataAdTracking.OnRead("user01", "西游记第一章", 1501234567890, 20);
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnFreeFinished"))
        {
            TalkingDataAdTracking.OnFreeFinished("user01", "免费章节阅读结束");
        }
#endif

#if TDAT_GAME || TDAT_ONLINEEDU
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnAchievementUnlock"))
        {
            TalkingDataAdTracking.OnAchievementUnlock("user01", "AdTracking_123456");
        }
#endif

#if TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnBrowse"))
        {
            TalkingDataAdTracking.OnBrowse("user01", "详情页page1", 1501234567890, 20);
        }
#endif

#if TDAT_RETAIL || TDAT_FINANCE || TDAT_TOUR || TDAT_OTHER
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnTrialFinished"))
        {
            TalkingDataAdTracking.OnTrialFinished("user01", "试用体验结束");
        }
#endif

#if TDAT_CUSTOM
        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent1"))
        {
            TalkingDataAdTracking.OnCustEvent1();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent2"))
        {
            TalkingDataAdTracking.OnCustEvent2();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent3"))
        {
            TalkingDataAdTracking.OnCustEvent3();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent4"))
        {
            TalkingDataAdTracking.OnCustEvent4();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent5"))
        {
            TalkingDataAdTracking.OnCustEvent5();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent6"))
        {
            TalkingDataAdTracking.OnCustEvent6();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent7"))
        {
            TalkingDataAdTracking.OnCustEvent7();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent8"))
        {
            TalkingDataAdTracking.OnCustEvent8();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent9"))
        {
            TalkingDataAdTracking.OnCustEvent9();
        }

        if (GUI.Button(new Rect(left + i % 2 * (width + spacing), top + step * (i++ / 2), width, height), "OnCustEvent10"))
        {
            TalkingDataAdTracking.OnCustEvent10();
        }
#endif
    }

    void Start()
    {
        Debug.Log("Start");
        //TalkingDataAdTracking.SetVerboseLogDisable();
        TalkingDataAdTracking.BackgroundSessionEnabled();
        TalkingDataAdTracking.Init("your_app_id", "your_channel_id", "your_custom_parameter");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnDestroy()
    {
        Debug.Log("onDestroy");
    }

    void Awake()
    {
        Debug.Log("Awake");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
}
