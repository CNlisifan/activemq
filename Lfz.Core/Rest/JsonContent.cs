using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lfz.Rest
{

    /// <summary>
    /// ֧��֧��Json���л����б�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonArray]
    public class JsonList<T> : List<T>, IJsonContent
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
    public class MultifunctionalContent : IJsonContent
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
    public class MultifunctionalContent<T> : MultifunctionalContent
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
    public class SingleContent<T> : IJsonContent
    {
        /// <summary>
        /// 
        /// </summary> 
        public T Value { get; set; }
    }

    /// <summary>
    /// ֧��Rest�������Ϣ���ݣ���JSON���л���
    /// </summary> 
    public class MessageContent : IJsonContent
    {

        /// <summary>
        /// ��Ϣ����
        /// </summary> 
        public string Message { get; set; }
    }

    /// <summary>
    /// ��������Rest��������
    /// </summary> 
    public class BoolContent : IJsonContent
    {
        /// <summary>
        /// ���⵱ǰ������True��False
        /// </summary> 
        public bool Flag { get; set; }
    }


    /// <summary>
    /// ��������Rest��������
    /// </summary> 
    public class EmptyContent : IJsonContent
    {
    }

    /// <summary>
    /// ֧��֧��Json���л����б�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IJsonContent
    {
    }

}