using UnityEngine;
using System.Collections.Generic;

public class TDAdTrackingDemoScript : MonoBehaviour
{
	
	const int left = 90;
	const int height = 50;
	const int top = 120;
	int width = Screen.width - left * 2;
	const int step = 60;

	void OnGUI()
	{
		
		int i = 0;
		GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Demo Menu");
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnReceiveDeepLink"))
		{
			TalkingDataAdTracking.OnReceiveDeepLink("https://www.talkingdata.com");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnAdSearch"))
		{
			TDAdSearch adSearch = TDAdSearch.CreateAdSearch();
			adSearch.SetDestination("Beijing");
			adSearch.SetOrigin("Shanghai");
			adSearch.SetItemId("123");
			adSearch.SetItemLocationId("Edu");
			adSearch.SetStartDate("2019-01-01");
			adSearch.SetEndDate("2019-12-12");
			TalkingDataAdTracking.OnAdSearch(adSearch);
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnRegister"))
		{
			TalkingDataAdTracking.OnRegister("user01");
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnLogin"))
		{
			TalkingDataAdTracking.OnLogin("user01");
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCreateRole"))
		{
			TalkingDataAdTracking.OnCreateRole("role01");
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnPay"))
		{
			TalkingDataAdTracking.OnPay("user01", "order02", 1077600, "CNY", "Apple Pay");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnPlaceOrder"))
		{
			TDOrder order = TDOrder.CreateOrder("order01", 2466400, "CNY");
			order.AddItem("A1660", "手机", "iPhone 7", 538800, 2);
			order.AddItem("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
			TalkingDataAdTracking.OnPlaceOrder("user01", order);
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnOrderPaySucc"))
		{
			TalkingDataAdTracking.OnOrderPaySucc("user01", "order01", 2, "CNY", "AliPay");
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnViewItem"))
		{
			TalkingDataAdTracking.OnViewItem("A1660", "手机", "iPhone 7", 538800);
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnAddItemToShoppingCart"))
		{
			TalkingDataAdTracking.OnAddItemToShoppingCart("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
		}

		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnViewShoppingCart"))
		{
			TDShoppingCart shoppingCart = TDShoppingCart.CreateShoppingCart();
			if (shoppingCart != null)
			{
				shoppingCart.AddItem("A1660", "手机", "iPhone 7", 538800, 2);
				shoppingCart.AddItem("MLH12CH", "电脑", "MacBook Pro", 1388800, 1);
				TalkingDataAdTracking.OnViewShoppingCart(shoppingCart);
			}
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent1"))
		{
			TalkingDataAdTracking.OnCustEvent1();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent2"))
		{
			TalkingDataAdTracking.OnCustEvent2();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent3"))
		{
			TalkingDataAdTracking.OnCustEvent3();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent4"))
		{
			TalkingDataAdTracking.OnCustEvent4();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent5"))
		{
			TalkingDataAdTracking.OnCustEvent5();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent6"))
		{
			TalkingDataAdTracking.OnCustEvent6();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent7"))
		{
			TalkingDataAdTracking.OnCustEvent7();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent8"))
		{
			TalkingDataAdTracking.OnCustEvent8();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent9"))
		{
			TalkingDataAdTracking.OnCustEvent9();
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "OnCustEvent10"))
		{
			TalkingDataAdTracking.OnCustEvent10();
		}
	}
	
	void Start()
	{
		Debug.Log("start...!!!!!!!!!!");
		// TalkingDataAdTracking.SetVerboseLogDisable();
		TalkingDataAdTracking.BackgroundSessionEnabled();
		TalkingDataAdTracking.EnableSFSafariViewControllerTracking();
		TalkingDataAdTracking.Init("your_app_id", "your_channel_id");
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
