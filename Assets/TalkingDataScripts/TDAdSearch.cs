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
			AndroidJavaClass javaClass = new AndroidJavaClass("com.tendcloud.appcpa.AdSearch");
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
	
	// 搜索字符串，至多128字符
	public TDAdSearch SetSearchTerm(string searchTerm)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setSearchTerm", searchTerm);
			}
#endif
#if UNITY_IPHONE
			this.searchTerm = searchTerm;
#endif
		}
		
		return this;
	}
	
	// 用于区分各种业务类型的字符串，至多128字符
	public TDAdSearch SetGoogleBusinessVertical(string googleBusinessVertical)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setGoogleBusinessVertical", googleBusinessVertical);
			}
#endif
#if UNITY_IPHONE
			this.googleBusinessVertical = googleBusinessVertical;
#endif
		}
		
		return this;
	}
	
	// 可自定义扩展参数
	public TDAdSearch SetCustomParam(Dictionary<string, object> parameters)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null && parameters != null && parameters.Count > 0)
			{
				int count = parameters.Count;
				AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap", count);
				IntPtr method_Put = AndroidJNIHelper.GetMethodID(map.GetRawClass(), 
						"put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
				object[] args = new object[2];
				foreach (KeyValuePair<string, object> kvp in parameters)
				{
					args[0] = new AndroidJavaObject("java.lang.String", kvp.Key);
					if (typeof(System.String).IsInstanceOfType(kvp.Value))
					{
						args[1] = new AndroidJavaObject("java.lang.String", kvp.Value);
					}
					else if (typeof(System.Boolean).IsInstanceOfType(kvp.Value))
					{
						args[1] = new AndroidJavaObject("java.lang.Boolean", kvp.Value);
					}
					else
					{
						args[1] = new AndroidJavaObject("java.lang.Double", ""+kvp.Value);
					}
					AndroidJNI.CallObjectMethod(map.GetRawObject(), method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
				}
				this.javaObj.Call<AndroidJavaObject>("setCustomParam", map);
			}
#endif
#if UNITY_IPHONE
			if (parameters != null && parameters.Count > 0)
			{
				string paramJson = "{";
				foreach (KeyValuePair<string, object> kvp in parameters)
				{
					if (paramJson.Length > 1)
					{
						paramJson += ",";
					}
					if (typeof(System.String).IsInstanceOfType(kvp.Value))
					{
						paramJson += "\"" + kvp.Key + "\":\"" + kvp.Value + "\"";
					}
					else if (typeof(System.Boolean).IsInstanceOfType(kvp.Value))
					{
						paramJson += "\"" + kvp.Key + "\":" + kvp.Value.ToString().ToLower();
					}
					else
					{
						paramJson += "\"" + kvp.Key + "\":" + kvp.Value;
					}
				}
				paramJson += "}";
				this.custom = paramJson;
			}
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
								+ "\",\"endDate\":\"" + this.endDate
								+ "\",\"searchTerm\":\"" + this.searchTerm
								+ "\",\"googleBusinessVertical\":\"" + this.googleBusinessVertical
								+ "\",\"custom\":" + this.custom + "}";
			return adSearchStr;
		}
		
		return null;
	}
#endif
}
