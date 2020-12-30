using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public static class JsonHelper
{
    private static JsonSerializerSettings _jsonSettings;

    static JsonHelper()
    {
        IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverterContent
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
        };

        _jsonSettings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        _jsonSettings.Converters.Add(datetimeConverter);
        _jsonSettings.ContractResolver = new LowercaseContractResolver();
    }

    /// <summary>
    /// 将指定的对象序列化成 JSON 数据。
    /// </summary>
    /// <param name="obj">要序列化的对象。</param>
    /// <returns></returns>
    public static string Serialize(object @object)
    {
        if (null == @object)
            return null;
        return JsonConvert.SerializeObject(@object, Formatting.None, _jsonSettings);
    }

    /// <summary>
    /// 将指定的 JSON 数据反序列化成指定对象。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="json">JSON 数据。</param>
    /// <returns></returns>
    public static T Deserialize<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
            return default;
        return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
    }

    /// <summary>
    /// 将指定的 JSON 数据反序列化成指定对象。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="json">JSON 数据。</param>
    /// <returns></returns>
    public static T Deserialize<T>(Dictionary<string, object> @object)
    {
        var json = JsonConvert.SerializeObject(@object, Formatting.None, _jsonSettings);
        return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
    }

    /// <summary>
    /// 将转换后的Key全部设置为小写
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static SortedDictionary<string, object> Deserialize(object @object)
    {
        var json = JsonConvert.SerializeObject(@object, Formatting.None, _jsonSettings);
        return Deserialize<SortedDictionary<string, object>>(json);
    }

    /// <summary>
    /// 将转换后的Key全部设置为小写
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static SortedDictionary<string, object> Deserialize(string json)
    {
        return Deserialize<SortedDictionary<string, object>>(json);
    }

    /// <summary>
    /// 将转换后的Key全部设置为小写
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static SortedDictionary<string, object> DeserializeLower(JObject json)
    {
        return DeserializeLower(json.ToString());
    }

    /// <summary>
    /// 将转换后的Key全部设置为小写
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static SortedDictionary<string, object> DeserializeLower(string json)
    {
        var obj = Deserialize<SortedDictionary<string, object>>(json);
        SortedDictionary<string, object> nobj = new SortedDictionary<string, object>();

        foreach (var item in obj)
        {
            nobj[item.Key.ToLower()] = item.Value;
        }
        obj.Clear();
        return nobj;
    }
}
