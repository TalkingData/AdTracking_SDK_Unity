using UnityEngine;


#if TDAT_FINANCE
public class TDTransaction
{
#if UNITY_ANDROID
    public AndroidJavaObject javaObj;
#endif

#if UNITY_IPHONE
    private string transactionId;
    private string category;
    private int amount;
    private string personA;
    private string personB;
    private long startDate;
    private long endDate;
    private string currencyType;
    private string content;
#endif

    public static TDTransaction CreateTransaction()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            TDTransaction transaction = new TDTransaction();
#if UNITY_ANDROID
            AndroidJavaClass javaClass = new AndroidJavaClass("com.tendcloud.appcpa.TDTransaction");
            transaction.javaObj = javaClass.CallStatic<AndroidJavaObject>("createTDTransaction");
#endif
            return transaction;
        }
        return null;
    }

    // 交易ID
    public TDTransaction SetTransactionId(string transactionId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setTransactionId", transactionId);
            }
#endif
#if UNITY_IPHONE
            this.transactionId = transactionId;
#endif
        }
        return this;
    }

    // 交易分类
    public TDTransaction SetCategory(string category)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setCategory", category);
            }
#endif
#if UNITY_IPHONE
            this.category = category;
#endif
        }
        return this;
    }

    // 交易额
    public TDTransaction SetAmount(int amount)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setAmount", amount);
            }
#endif
#if UNITY_IPHONE
            this.amount = amount;
#endif
        }
        return this;
    }

    // 交易甲方
    public TDTransaction SetPersonA(string personA)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setPersonA", personA);
            }
#endif
#if UNITY_IPHONE
            this.personA = personA;
#endif
        }
        return this;
    }

    // 交易乙方
    public TDTransaction SetPersonB(string personB)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setPersonB", personB);
            }
#endif
#if UNITY_IPHONE
            this.personB = personB;
#endif
        }
        return this;
    }

    // 交易起始Unix时间戳。单位：毫秒
    public TDTransaction SetStartDate(long startDate)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setStartDate", startDate);
            }
#endif
#if UNITY_IPHONE
            this.startDate = startDate;
#endif
        }
        return this;
    }

    // 交易终止Unix时间戳。单位：毫秒
    public TDTransaction SetEndDate(long endDate)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setEndDate", endDate);
            }
#endif
#if UNITY_IPHONE
            this.endDate = endDate;
#endif
        }
        return this;
    }

    // 货币类型
    public TDTransaction SetCurrencyType(string currencyType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setCurrencyType", currencyType);
            }
#endif
#if UNITY_IPHONE
            this.currencyType = currencyType;
#endif
        }
        return this;
    }

    // 交易详情
    public TDTransaction SetContent(string content)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (javaObj != null)
            {
                javaObj.Call<AndroidJavaObject>("setContent", content);
            }
#endif
#if UNITY_IPHONE
            this.content = content;
#endif
        }
        return this;
    }

#if UNITY_IPHONE
    public override string ToString()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            string transactionStr = "{\"transactionId\":\"" + transactionId + "\""
                                  + ",\"category\":\"" + category + "\""
                                  + ",\"amount\":" + amount
                                  + ",\"personA\":\"" + personA + "\""
                                  + ",\"personB\":\"" + personB + "\""
                                  + ",\"startDate\":" + startDate
                                  + ",\"endDate\":" + endDate
                                  + ",\"currencyType\":\"" + currencyType + "\""
                                  + ",\"content\":\"" + content + "\""
                                  + "}";
            return transactionStr;
        }
        return null;
    }
#endif
}
#endif
