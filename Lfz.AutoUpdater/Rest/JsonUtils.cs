using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lfz.AutoUpdater.Rest
{
    /// <summary>
    /// ʹ��Newtonsoft.Json���л�/�����л�JSON���ݹ�����
    /// </summary>
    internal static class JsonUtils
    {
        /// <summary>
        /// �����л�json�ַ���Ϊһ������
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeObject<T>(string jsonContent)
        {
            return DeserializeObject<T>(jsonContent, new Dictionary<JsonPropertyFilterEnum, Type>());
        }

        /// <summary>
        /// �����л�json�ַ���Ϊһ������
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeObject(string jsonContent, Type type)
        {
            return DeserializeObject(type, jsonContent, new Dictionary<JsonPropertyFilterEnum, Type>());
        }

        /// <summary>
        /// REST��Ӧ JSON�����л�
        /// <example>
        /// �����������ķ����л�:
        ///  public sealed class JsonContent 
        ///  {  
        ///      public RestStatus StatusCode { get; set; } 
        ///      [RestResolverFilter(filterEnum)]
        ///      public IJsonContent Content { get; set; }
        ///  }
        /// ����Content�ǽӿڣ��ڷ����л�ʱ��Ҫ���廯���͡����䷴���л����͵Ĳ��ң�ͨ��filterEnum��ȡ
        /// </example>
        /// </summary> 
        /// <typeparam name="TSubContentImp">JSONģ�������ݣ����Խӿ�����ʵ��,��Ҫ�������;��廯</typeparam>
        /// <typeparam name="T">���������ʽ</typeparam>
        /// <param name="jsonContent"></param>
        /// <param name="filterEnum"></param>
        /// <returns></returns>
        public static T DeserializeObject<T, TSubContentImp>(string jsonContent, JsonPropertyFilterEnum filterEnum)
        {
            var dic = new Dictionary<JsonPropertyFilterEnum, Type> { { filterEnum, typeof(TSubContentImp) } };
            Dictionary<JsonPropertyFilterEnum, Type> dicPropertyNameType = dic;
            return DeserializeObject<T, TSubContentImp>(jsonContent, dicPropertyNameType);
        }

        /// <summary>
        /// REST��Ӧ JSON�����л�
        /// <example>
        /// �����������������������:
        ///  public sealed class JsonContent 
        ///  {  
        ///      public RestStatus StatusCode { get; set; } 
        ///      [RestResolverFilter(filterEnum)]
        ///      public IJsonContent Content { get; set; }
        ///  }
        /// ����Content�ǽӿڣ��ڷ����л�ʱ��Ҫ���廯���͡����䷴���л����͵Ĳ��ң�ͨ��filterEnum��ȡ��
        /// �ڹ��췴���л�������ʱ������һ�������л�ʵ�������ֵ��Թ�����ʵ�����͡�
        /// </example>
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="jsonContent"></param>
        /// <param name="dicPropertyNameType">����һ�������л�ʵ�������ֵ��Թ�����ʵ������</param> 
        /// <returns>���ط����л���Ķ���ʵ��</returns>
        public static object DeserializeObject(Type type, string jsonContent, IDictionary<JsonPropertyFilterEnum, Type> dicPropertyNameType)
        {
            var settings = new JsonSerializerSettings
                {
                    ContractResolver = new RestContentContractResolver(dicPropertyNameType)
                };
            var dataConvert = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            settings.Converters.Add(dataConvert);
            return (string.IsNullOrEmpty(jsonContent) ? null : JsonConvert.DeserializeObject(jsonContent, type, settings));
        }

        /// <summary>
        /// REST��Ӧ JSON�����л�
        /// <example>
        /// �����������������������:
        ///  public sealed class JsonContent 
        ///  {  
        ///      public RestStatus StatusCode { get; set; } 
        ///      [RestResolverFilter(filterEnum)]
        ///      public IJsonContent Content { get; set; }
        ///  }
        /// ����Content�ǽӿڣ��ڷ����л�ʱ��Ҫ���廯���͡����䷴���л����͵Ĳ��ң�ͨ��filterEnum��ȡ��
        /// �ڹ��췴���л�������ʱ������һ�������л�ʵ�������ֵ��Թ�����ʵ�����͡�
        /// </example>
        /// </summary>  
        /// <typeparam name="T">���������ʽ</typeparam>
        /// <param name="jsonContent"></param>
        /// <param name="dicPropertyNameType">����һ�������л�ʵ�������ֵ��Թ�����ʵ������</param> 
        /// <returns>���ط����л���Ķ���ʵ��</returns>
        public static T DeserializeObject<T>(string jsonContent, IDictionary<JsonPropertyFilterEnum, Type> dicPropertyNameType)
        {
            var settings = new JsonSerializerSettings
                {
                    ContractResolver = new RestContentContractResolver(dicPropertyNameType)
                };
            var dataConvert = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            settings.Converters.Add(dataConvert);
            return (string.IsNullOrEmpty(jsonContent) ? default(T) : JsonConvert.DeserializeObject<T>(jsonContent, settings));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSubContentImp"></typeparam>
        /// <param name="jsonContent"></param>
        /// <param name="dicPropertyNameType"></param>
        /// <returns></returns>
        public static T DeserializeObject<T, TSubContentImp>(string jsonContent, IDictionary<JsonPropertyFilterEnum, Type> dicPropertyNameType)
        {
            if (!dicPropertyNameType.ContainsKey(JsonPropertyFilterEnum.RestResponseResult))
            {
                dicPropertyNameType.Add(new KeyValuePair<JsonPropertyFilterEnum, Type>(JsonPropertyFilterEnum.RestResponseResult, typeof(TSubContentImp)));
            }
            return DeserializeObject<T>(jsonContent, dicPropertyNameType);
        }

        /// <summary>
        /// ���л�����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj, params JsonConverter[] converters)
        {
            return SerializeObject(obj, Formatting.None, converters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatting"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj, Formatting formatting, params JsonConverter[] converters)
        {
            if (obj == null) return string.Empty;
            if ((converters == null) || (converters.Length == 0))
            {
                var itemlist = new JsonConverter[1];
                var dataConvert = new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                itemlist[0] = dataConvert;
                converters = itemlist;
            }
            else if (converters.All(x => !(x.GetType() == typeof(IsoDateTimeConverter))))
            {
                var list = converters.ToList();
                var dataConvert = new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                list.Add(dataConvert);
                converters = list.ToArray();
            }
            return JsonConvert.SerializeObject(obj, formatting, converters);
        }


        #region REST��Ӧ(REST API���ô�����Ϻ���ͻ��˵���Ӧ��Ϣ)

        /// <summary>
        /// REST��Ӧ JSON�����л�
        /// <example>
        /// �����������ķ����л�:
        ///  public sealed class RestResponse 
        ///  {  
        ///      public RestStatus StatusCode { get; set; } 
        ///      [JsonProperty("Result")]   
        ///      public IJsonContent Content { get; set; }
        ///  }
        /// ����Content�ǽӿڣ��ڷ����л�ʱ��Ҫ���廯����.
        /// ��Ҫ����RestClient�д�����Ӧʱ��Ҫ�õ�
        /// </example>
        /// </summary> 
        /// <typeparam name="TSubContentImp">JSONģ�������ݣ����Խӿ�����ʵ��,��Ҫ�������;��廯</typeparam> 
        /// <param name="jsonContent"></param> 
        /// <returns></returns>
        public static ResponseContent DeserializeResponse<TSubContentImp>(string jsonContent)
        {
            return DeserializeObject<ResponseContent, TSubContentImp>(jsonContent, JsonPropertyFilterEnum.RestResponseResult);
        }

        /// <summary>
        /// REST��Ӧ JSON�����л� 
        /// </summary>  
        /// <param name="jsonContent"></param> 
        /// <returns></returns>
        public static ResponseContent DeserializeResponse(string jsonContent)
        {
            return DeserializeObject<ResponseContent>(jsonContent);
        }

        /// <summary>
        /// REST��Ӧ JSON�����л�
        /// <example>
        ///  public sealed class RestResponse 
        ///  {  
        ///      public RestStatus StatusCode { get; set; } 
        ///      [JsonProperty("Result")]  
        ///      public IJsonContent Content { get; set; }
        ///  }
        /// ����Content�ǽӿڣ��ڷ����л�ʱ��Ҫ���廯����.
        /// ��Ҫ����Rest Client�д�����Ӧʱ��Ҫ�õ�
        /// </example>
        /// </summary> 
        /// <typeparam name="TDataContentImp"></typeparam>
        /// <typeparam name="TSubContentImp">JSONģ�������ݣ����Խӿ�����ʵ��,��Ҫ�������;��廯</typeparam> 
        /// <param name="jsonContent"></param> 
        /// <returns></returns>
        public static ResponseContent DeserializeResponse<TDataContentImp, TSubContentImp>(string jsonContent)
        {
            var dic = new Dictionary<JsonPropertyFilterEnum, Type>
                {
                    {JsonPropertyFilterEnum.RestResponseResult, typeof (TDataContentImp)},
                    {JsonPropertyFilterEnum.SettingModelData, typeof (TSubContentImp)}
                };
            return DeserializeObject<ResponseContent, TSubContentImp>(jsonContent, dic);
        }

        #endregion

        /// <summary>
        /// JSON�����л� 
        /// </summary>
        /// <typeparam name="T"></typeparam>  
        /// <param name="jsonContent"></param> 
        /// <returns></returns>
        public static T Deserialize<T>(string jsonContent)
        {
            return DeserializeObject<T>(jsonContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static JsonList<T> ToJsonList<T>(this IEnumerable<T> collection)
        {
            return new JsonList<T>(collection);
        }
  
    }
}