using System;

namespace Lfz.Mq
{
    /// <summary>
    /// ��Ϣͨ��������Ϣ
    /// </summary>
    public class MqInstanceInfo
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
        /// MQʵ��Id
        /// </summary>
        public int MqInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DelFalg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}