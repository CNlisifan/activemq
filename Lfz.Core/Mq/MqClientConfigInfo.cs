using System;

namespace Lfz.Mq
{
    /// <summary>
    /// ��Ϣͨ��������Ϣ(ָ���ͻ�����Ϣ������Ϣ)
    /// </summary>
    public class MqClientConfigInfo
    {
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// tcpͨ�Ŷ˿ڣ�֧��openwriteЭ��
        /// </summary> 
        public int Port { get; set; }

        /// <summary>
        /// mqttЭ��ʹ�ö˿�
        /// </summary>
        public int? MqttPort { get; set; }

        /// <summary>
        /// ��Ϣ���з����û�
        /// </summary>
        public string AccessUsername { get; set; }

        /// <summary>
        /// ��Ϣ���з�������
        /// </summary>
        public string AccessPassword { get; set; }

        /// <summary>
        /// �ͻ�ID(�������ŵ�ID��)
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// MQʵ��Id
        /// </summary>
        public int MqInstanceId { get; set; }

        /// <summary>
        /// MQ����ʵ��ID
        /// </summary>
        public int MqListenerId { get; set; }
        /// <summary>
        /// ���ù��ڣ�����֮����Ҫ�����ж������Ƿ���Ч
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}