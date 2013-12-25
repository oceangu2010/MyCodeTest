using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for JsonHelper
/// </summary>
public class JsonHelper
{
    public JsonHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// 把对象序列化 JSON 字符串 
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象实体</param>
    /// <returns>JSON字符串</returns>
    public static string GetJson<T>(T obj)
    {
        //记住 添加引用 System.ServiceModel.Web 
        /**
         * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json是出不来的哦
         * */
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
        using (MemoryStream ms = new MemoryStream())
        {
            json.WriteObject(ms, obj);
            string szJson = Encoding.UTF8.GetString(ms.ToArray());
            return szJson;
        }
    }
    /// <summary>
    /// 把JSON字符串还原为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="szJson">JSON字符串</param>
    /// <returns>对象实体</returns>
    public static T ParseFormJson<T>(string szJson)
    {
        T obj = Activator.CreateInstance<T>();
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
        {
            DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
            return (T)dcj.ReadObject(ms);
        }
    }
}

