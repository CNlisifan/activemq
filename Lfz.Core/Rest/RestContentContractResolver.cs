using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lfz.Rest
{
    /// <summary>
    /// Rest�������������
    /// </summary>
    public class RestContentContractResolver : DefaultContractResolver
    {
        private readonly IDictionary<JsonPropertyFilterEnum, Type> _dicPropertyNameType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dicPropertyNameType">JSON���Թ������б����й������е����Ծ�Ϊ��Ҫָ�������ع������л������</param>
        public RestContentContractResolver(IDictionary<JsonPropertyFilterEnum, Type> dicPropertyNameType)
            : base(false)
        {
            this._dicPropertyNameType = dicPropertyNameType ?? new Dictionary<JsonPropertyFilterEnum, Type>();
        }

        /// <summary>
        /// ���������л�����Ҫʹ�õ���JsonProperty�����б�
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            var filterList = GetFilterAttrbute(type);
            return FilterProperties(properties, filterList);
        }

        private IList<JsonProperty> FilterProperties(IList<JsonProperty> properties, IList<FilterInfo> filterList)
        {
            foreach (var jsonProperty in properties)
            {
                if (jsonProperty.PropertyType == null) continue;
                //��ȡ�����������Ƿ�ʵ��RestResolverFilterAttribute����
                var filterInfo = filterList.FirstOrDefault(x => x.PropertyType == jsonProperty.PropertyType && x.Name == jsonProperty.PropertyName);
                if (filterInfo == null || filterInfo.FilterAttribute == null) continue;
                var attr = filterInfo.FilterAttribute;
                if (attr.UniqueKey == JsonPropertyFilterEnum.None) continue;
                //���������Ч��RestResolverFilterAttribute���ԣ���ô��Ҫ���������滻������滻���Ͳ����ڣ���ô�Ƴ�Json����
                if (_dicPropertyNameType.ContainsKey(attr.UniqueKey))
                {
                    var value = _dicPropertyNameType[attr.UniqueKey];
                    jsonProperty.PropertyType = value;
                    jsonProperty.Required = Required.Default;
                }
                else
                {
                    //��ǰ��Ҫ���˴�������δ������Ч����,�����Ƴ�����
                    jsonProperty.PropertyType = null;
                }
            }
            //ֻ��PropertyType��Ϊ�յĲ�����Ч����
            properties = properties.Where(x => x.PropertyType != null).ToList();
            return properties;
        }

        private IList<FilterInfo> GetFilterAttrbute(Type type)
        {
            var dictionary = new List<FilterInfo>();
            //�������Ե����ͻ�ȡ 
            var propertyList = type.GetProperties();
            foreach (var propertyInfo in propertyList)
            {
                var filterAttribute = GetAttribute<RestResolverFilterAttribute>(propertyInfo);
                if (filterAttribute == null) continue;
                var typeName = new FilterInfo();
                var attribute = GetAttribute<JsonPropertyAttribute>(propertyInfo);
                typeName.Name = attribute != null ? attribute.PropertyName : propertyInfo.Name;
                typeName.PropertyType = propertyInfo.PropertyType;
                typeName.FilterAttribute = filterAttribute;
                dictionary.Add(typeName);
            }
            return dictionary;
        }

        private T GetAttribute<T>(PropertyInfo memberInfo) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
        }

        private class FilterInfo
        {
            public string Name { get; set; }
            public Type PropertyType { get; set; }
            public RestResolverFilterAttribute FilterAttribute { get; set; }
        }
    }
}