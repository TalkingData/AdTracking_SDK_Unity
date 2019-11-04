using UnityEngine;


#if TDAT_RETAIL
public class TDShoppingCart
{
	
#if UNITY_ANDROID
	public AndroidJavaObject javaObj;
#endif
	
#if UNITY_IPHONE
	private string items = "";
#endif
	
	private static TDShoppingCart shoppingCart;
	
	/* Public interface for use inside C# code */
	
	public static TDShoppingCart CreateShoppingCart()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			shoppingCart = new TDShoppingCart();
#if UNITY_ANDROID
			AndroidJavaClass javaClass = new AndroidJavaClass("com.tendcloud.appcpa.ShoppingCart");
			shoppingCart.javaObj = javaClass.CallStatic<AndroidJavaObject>("createShoppingCart");
#endif
			return shoppingCart;
		}
		
		return null;
	}
	
	public TDShoppingCart AddItem(string itemId, string category, string name, int unitPrice, int amount)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
#if UNITY_ANDROID
			if (this.javaObj != null)
			{
				this.javaObj.Call<AndroidJavaObject>("addItem", itemId, category, name, unitPrice, amount);
			}
#endif
#if UNITY_IPHONE
			string item = "{\"itemId\":\"" + itemId + "\",\"category\":\"" + category + "\",\"name\":\"" + name + "\",\"unitPrice\":" + unitPrice + ",\"amount\":" + amount + "}";
			if (this.items.Length > 0)
			{
				this.items += ",";
			}
			this.items += item;
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
			string orderStr = "{\"items\":[" + items + "]}";
			return orderStr;
		}
		
		return null;
	}
#endif
}
#endif
