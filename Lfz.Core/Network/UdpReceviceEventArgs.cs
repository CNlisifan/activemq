using System;
using System.Net;

namespace Lfz.Network
{
    /// <summary>
    /// Udp���������¼�����
    /// </summary>
    public class UdpReceviceEventArgs : EventArgs
    {
        /// <summary>
        /// ���յ���������
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// UDPԶ��IP��ַ��Ϣ
        /// </summary>
        public EndPoint RemoteEndPoint { get; set; }
    }
}