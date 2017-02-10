using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lfz.AutoUpdater.Rest
{ 

    /// <summary>
    /// ֧��֧��Json���л����б�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonArray]
    internal class JsonList<T> : List<T>, IJsonContent
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public JsonList()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="collection">�����б�</param>
        public JsonList(IEnumerable<T> collection)
            : base(collection)
        {
        } 
    }

    /// <summary>
    /// �๦�����ݣ�һ��REST URL��Դ�ɸ������������ֹ���ʵ�֣�
    /// </summary> 
    internal class MultifunctionalContent : IJsonContent
    {
        /// <summary>
        /// 
        /// </summary> 
        public string Method { get; set; }

        /// <summary>
        /// ��������
        /// </summary> 
        [RestResolverFilter(JsonPropertyFilterEnum.SettingModelData)]
        public IJsonContent Data { get; set; }
    }
     
    /// <summary>
    ///  �๦�����ݣ�һ��REST URL��Դ�ɸ������������ֹ���ʵ�֣�,��Я��һ������ֵ
    /// </summary>
    /// <typeparam name="T"></typeparam> 
    internal class MultifunctionalContent<T> : MultifunctionalContent
    {
        /// <summary>
        /// 
        /// </summary> 
        public T Id { get; set; }
    }
     
    /// <summary>
    /// ��һֵ���Rest�������ݣ������л�JSON�ַ�����
    /// </summary>
    /// <typeparam name="T"></typeparam> 
    internal class SingleContent<T> : IJsonContent
    {
        /// <summary>
        /// 
        /// </summary> 
        public T Value { get; set; }
    }

    /// <summary>
    /// ֧��Rest�������Ϣ���ݣ���JSON���л���
    /// </summary> 
    internal class MessageContent : IJsonContent
    { 

        /// <summary>
        /// ��Ϣ����
        /// </summary> 
        public string Message { get; set; }
    }

    /// <summary>
    /// ��������Rest��������
    /// </summary> 
    internal class BoolContent : IJsonContent
    {
        /// <summary>
        /// ���⵱ǰ������True��False
        /// </summary> 
        public bool Flag { get; set; }
    }
 

    /// <summary>
    /// ��������Rest��������
    /// </summary> 
    internal class EmptyContent : IJsonContent
    { 
    }

    /// <summary>
    /// ֧��֧��Json���л����б�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class JsonDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IJsonContent
    {               
    }

}