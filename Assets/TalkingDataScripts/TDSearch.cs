using UnityEngine;


public class TDSearch
{
	
#if UNITY_ANDROID
	public AndroidJavaObject javaObj;
#endif
	
#if UNITY_IPHONE
	private string category;
	private string content;
#if TDAT_RETAIL
	private string itemId;
	private string itemLocationId;
#endif
#if TDAT_TOUR
	private string destination;
	private string origin;
	private long startDate;
	private long endDate;
#endif
#endif
	
	/* Public interface for use inside C# code */
	
	public static TDSearch CreateSearch()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			TDSearch search = new TDSearch();
#if UNITY_ANDROID
			AndroidJavaClass javaClass = new AndroidJavaClass("com.tendcloud.appcpa.TDSearch");
			search.javaObj = javaClass.CallStatic<AndroidJavaObject>("createAdSearch");
#endif
			return search;
		}
		
		return null;
	}
	
	// 搜索分类
	public TDSearch SetCategory(string category)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setCategory", category);
			}
#endif
#if UNITY_IPHONE
			this.category = category;
#endif
		}
		
		return this;
	}
	
	// 搜索内容
	public TDSearch SetContent(string content)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("setContent", content);
			}
#endif
#if UNITY_IPHONE
			this.content = content;
#endif
		}
		
		return this;
	}
	
#if TDAT_RETAIL
	// 商品 ID（eg.酒店/汽车）；至多64字符，支持数字+字母
	public TDSearch SetItemId(string itemId)
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
	public TDSearch SetItemLocationId(string itemLocationId)
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
#endif
	
#if TDAT_TOUR
	// 目的地城市 ID；至多64字符，支持数字+字母
	public TDSearch SetDestination(string destination)
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
	public TDSearch SetOrigin(string origin)
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
	
	// 业务事件起始日期（eg.航班出发日期）
	public TDSearch SetStartDate(long startDate)
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
	
	// 业务事件截止日期（eg.航班返程日期）
	public TDSearch SetEndDate(long endDate)
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
#endif
	
#if UNITY_IPHONE
	public override string ToString()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			string searchStr = "{\"category\":\"" + this.category + "\""
							 + ",\"content\":\"" + this.content + "\""
#if TDAT_RETAIL
							 + ",\"itemId\":\"" + this.itemId + "\""
							 + ",\"itemLocationId\":\"" + this.itemLocationId + "\""
#endif
#if TDAT_TOUR
							 + ",\"destination\":\"" + this.destination + "\""
							 + ",\"origin\":\"" + this.origin + "\""
							 + ",\"startDate\":" + this.startDate
							 + ",\"endDate\":" + this.endDate
#endif
							 + "}";
			return searchStr;
		}
		
		return null;
	}
#endif
}
