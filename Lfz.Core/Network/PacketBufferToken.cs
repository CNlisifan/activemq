//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : PacketBufferToken
//        DESCRIPTION : ���ͻ���յ����ݰ�
//
//        Created By �ַ��� at  2013-01-08 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System;
using System.Net;
using System.Net.Sockets;

namespace Lfz.Network
{
    /// <summary>
    /// ���ͻ���յ����ݰ�
    /// </summary>
    public sealed class PacketBufferToken : IDisposable
    {
        /// <summary>
        /// ���������С
        /// </summary> 
        public const int BufferSize = 2048;

        /// <summary>
        /// ������ֵ
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// �������ݳ���,�׽ӽ��յĳ��ȣ���Data.Length��ʾ��������󳤶�
        /// </summary>
        public int DataLength
        {
            get;
            set;
        }

        /// <summary>
        /// UDP ����Socket��TCP����,ֻ����ʹ��TCPʱ�õ���?
        /// </summary>
        public Socket Hanlder { get; set; }

        /// <summary>
        /// �׽�Э������
        /// </summary>
        public ProtocolType ProtocolType { get; private set; }

        /// <summary>
        /// Զ��IP
        /// </summary>
        public EndPoint RemoteEndPoint;




        /// <summary>
        /// 
        /// </summary>
        /// <param name="hanlder"></param>
        /// <param name="isUdp"></param>
        public PacketBufferToken(Socket hanlder, bool isUdp)
        {
            if (isUdp)
            {
                ProtocolType = ProtocolType.Udp;
            }
            else
            {
                ProtocolType = ProtocolType.Tcp;
                RemoteEndPoint = hanlder.RemoteEndPoint;
            }
            DataLength = 0;

        }

        #region IDisposable ��Ա

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (Hanlder != null && ProtocolType == ProtocolType.Tcp)
            {
                if (Hanlder.Connected)
                {
                    Hanlder.Shutdown(SocketShutdown.Both);
                }
                Hanlder.Close();
            }
        }

        #endregion
    }

    
}