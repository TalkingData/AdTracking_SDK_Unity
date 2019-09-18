using UnityEngine;
using System;
using System.Collections.Generic;


public class TDAdSearch
{
	
#if UNITY_ANDROID
	public AndroidJavaObject javaObj;
#endif
	
#if UNITY_IPHONE
	private string destination;
	private string origin;
	private string itemId;
	private string itemLocationId;
	private string startDate;
	private string endDate;
	private string searchTerm;
	private string googleBusinessVertical;
	private string custom;
#endif
	
	/* Public interface for use inside C# code */
	
	public static TDAdSearch CreateAdSearch()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			TDAdSearch adSearch = new TDAdSearch();
#if UNITY_ANDROID
			AndroidJavaClass javaClass = new AndroidJavaClass("com.tendcloud.appcpa.TDSearch");
			adSearch.javaObj = javaClass.CallStatic<AndroidJavaObject>("createAdSearch");
#endif
			return adSearch;
		}
		
		return null;
	}
	
	public TDAdSearch() {}
	
	// 目的地城市 ID；至多64字符，支持数字+字母
	public TDAdSearch SetDestination(string destination)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setDestination", destination);
			}
#endif
#if UNITY_IPHONE
			this.destination = destination;
#endif
		}
		
		return this;
	}
	
	// 出发地城市 ID；至多64字符，支持数字+字母
	public TDAdSearch SetOrigin(string origin)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setOrigin", origin);
			}
#endif
#if UNITY_IPHONE
			this.origin = origin;
#endif
		}
		
		return this;
	}
	
	// 商品 ID（eg.酒店/汽车）；至多64字符，支持数字+字母
	public TDAdSearch SetItemId(string itemId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setItemId", itemId);
			}
#endif
#if UNITY_IPHONE
			this.itemId = itemId;
#endif
		}
		
		return this;
	}
	
	// 商品位置 ID（eg.求职招聘/教育行业）；至多64字符，支持数字+字母
	public TDAdSearch SetItemLocationId(string itemLocationId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setItemLocationId", itemLocationId);
			}
#endif
#if UNITY_IPHONE
			this.itemLocationId = itemLocationId;
#endif
		}
		
		return this;
	}
	
	// 业务事件起始日期（eg.航班出发日期）；yyyy-mm-dd，"2016-09-23"；
	public TDAdSearch SetStartDate(string startDate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setStartDate", startDate);
			}
#endif
#if UNITY_IPHONE
			this.startDate = startDate;
#endif
		}
		
		return this;
	}
	
	// 业务事件截止日期（eg.航班返程日期）；yyyy-mm-dd，"2016-09-23"；
	public TDAdSearch SetEndDate(string endDate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setEndDate", endDate);
			}
#endif
#if UNITY_IPHONE
			this.endDate = endDate;
#endif
		}
		
		return this;
	}
	
#if UNITY_IPHONE
	public override string ToString()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			string adSearchStr = "{\"destination\":\"" + this.destination
								+ "\",\"origin\":\"" + this.origin
								+ "\",\"itemId\":\"" + this.itemId
								+ "\",\"itemLocationId\":\"" + this.itemLocationId
								+ "\",\"startDate\":\"" + this.startDate
								+ "\",\"endDate\":\"" + this.endDate + "\"}";
			return adSearchStr;
		}
		
		return null;
	}
#endif
}
