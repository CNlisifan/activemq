using System;
using Newtonsoft.Json;

namespace Lfz.WebApi
{
    /// <summary>
    /// 
    /// </summary> 
    public class ApiResult
    {
        /// <summary>
        /// �Ƿ�ʹ�óɹ� 0 �ɹ�
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// api������Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ״̬ԭʼֵ��������
        /// </summary>
        [JsonIgnore]
        public ServerApiStatus ApiStatus
        {
            get
            {
                if (Enum.IsDefined(typeof(ServerApiStatus), Status))
                    return (ServerApiStatus)Enum.Parse(typeof(ServerApiStatus), Status.ToString());
                return ServerApiStatus.None;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// ��ǰ��ȡ�����ݼ���
        /// </summary>
        public T Data { get; set; }
    }
}