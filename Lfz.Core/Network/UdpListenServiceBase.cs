//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : UdpListenServiceBase
//        DESCRIPTION : ������Udp��������
//
//        Created By �ַ��� at  2013-01-08 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System;
using System.Net;
using System.Net.Sockets;
using Lfz.Logging;
using Lfz.Services;

namespace Lfz.Network
{

    /// <summary>
    /// ������Udp��������
    /// </summary>
    public abstract class UdpListenServiceBase : ThreadServiceBase
    {
        #region ���ԡ��ֶΣ��˿ڿۡ��߳����ȣ�

        /// <summary>
        /// ��ǰ������󲢷�����
        /// </summary>
        protected readonly int NumConnections;

        private readonly int _receiveBufferSize;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _listenPort;
        private Socket _listenSocket;


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        protected UdpListenServiceBase(int port)
            : this(1000, 2048, port)
        {
        }

        /// <summary>
        /// Create an uninitialized server instance.  
        /// To start the server listening for connection requests
        /// call the Init method followed by Start method
        /// </summary>   
        /// <param name="numConnections">the maximum number of connections the Service is designed to handle simultaneously</param>
        /// <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        /// <param name="port"> </param>
        protected UdpListenServiceBase(int numConnections, int receiveBufferSize, int port)
        {
            Stoping += OnClosing;
            Starting += OnStarting;

             
            NumConnections = numConnections;
            _receiveBufferSize = receiveBufferSize;
            _listenPort = port;
        }

        private void OnStarting()
        {
            var ipep = new IPEndPoint(IPAddress.Any, _listenPort);
            _listenSocket = new Socket(ipep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            _listenSocket.Bind(ipep);
        }

        private void OnClosing()
        {
            if (_listenSocket != null) { _listenSocket.Close(); }
        }

        /// <summary>
        ///  
        /// </summary>
        public event EventHandler<UdpReceviceEventArgs> ReceviceCompleted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param> 
        /// <param name="remoteEndPoint"> </param>
        public void Send (byte[] data, EndPoint remoteEndPoint)
        {
            _listenSocket.SendTo(data, remoteEndPoint);
        }


        /// <summary>
        /// �̷߳���
        /// </summary>
        protected override void Excute(object obj)
        {
            while (IsRunning)
            {
                try
                {
                    //Make space to store the data from the socket
                    var received = new Byte[_receiveBufferSize];

                    //Create an end point, just give it temporary values
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, _listenPort);

                    //Read bytes from the socket
                    var bytesReceived = _listenSocket.ReceiveFrom(received, ref remoteEP);
                    UdpReceviceEventArgs eventArgs = new UdpReceviceEventArgs
                                                         {
                                                             RemoteEndPoint = remoteEP,
                                                             Data = new byte[bytesReceived]
                                                         };
                    Buffer.BlockCopy(received, 0, eventArgs.Data, 0, bytesReceived);
                    if (ReceviceCompleted != null)
                        ReceviceCompleted(_listenSocket, eventArgs);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, "UdpListenServiceBase.Excute", ex);
                }
            }
        }
    }
}