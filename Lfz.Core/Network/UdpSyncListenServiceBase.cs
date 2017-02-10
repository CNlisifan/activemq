//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : UdpSyncListenReceviceService
//        DESCRIPTION : ������Udp �첽�������ݷ���
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
    /// ������Udp �첽�������ݷ���
    /// </summary>
    public abstract class UdpSyncListenServiceBase : ServiceBase
    {
        #region ���ԡ��ֶΣ��˿ڿۡ��߳����ȣ�

        /// <summary>
        /// ��ǰ������󲢷�����
        /// </summary>
        protected readonly int MaxConcurrentConnections;

        /// <summary>
        /// 
        /// </summary>
        public int ListenPort { get; private set; }

        /// <summary>
        /// ����IP
        /// </summary>
        public IPAddress ListenIp { get; private set; }

        private Socket _listenSocket;

        private readonly UdpSyncListenReceviceService _listenReceviceService;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        protected UdpSyncListenServiceBase(int port)
            : this(1000, 2048, string.Empty, port)
        {
        }

        /// <summary>
        /// Create an uninitialized server instance.  
        /// To start the server listening for connection requests
        /// call the Init method followed by Start method
        /// </summary>   
        /// <param name="numConnections">the maximum number of connections the Service is designed to handle simultaneously</param>
        /// <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        /// <param name="listenIp"></param>
        /// <param name="port"> </param>
        protected UdpSyncListenServiceBase(int numConnections, int receiveBufferSize, string listenIp, int port)
        {
            Stoping += OnClosing;
            Starting += OnStarting;

            _listenReceviceService = new UdpSyncListenReceviceService(numConnections, receiveBufferSize);
            _listenReceviceService.ReceviceCompleted += ListenReceviceServiceOnReceviceCompleted;
             
            MaxConcurrentConnections = numConnections;
            ListenPort = port;
            IPAddress temp;
            if (!IPAddress.TryParse(listenIp, out temp)) temp = IPAddress.Any;
            ListenIp = temp;
        }

        private void ListenReceviceServiceOnReceviceCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
        {
            if (ReceviceCompleted != null) ReceviceCompleted(sender, socketAsyncEventArgs);
        }

        private void OnStarting()
        {
            var ipep = new IPEndPoint(ListenIp, ListenPort);
            _listenSocket = new Socket(ipep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            _listenSocket.Bind(ipep);
            _listenReceviceService.ListenSocket = _listenSocket;
            _listenReceviceService.Start();
        }

        private void OnClosing()
        {
            _listenReceviceService.Stop();
            if (_listenSocket != null) { _listenSocket.Close(); }
        }

        /// <summary>
        ///  
        /// </summary>
        public event EventHandler<SocketAsyncEventArgs> ReceviceCompleted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param> 
        /// <param name="remoteEndPoint"> </param>
        public void Send(byte[] data, EndPoint remoteEndPoint)
        {
            if (IsRunning)
                _listenSocket.SendTo(data, remoteEndPoint);
            else Logger.Log(LogLevel.Error, "ת���߳�δ�������Ѿ�ֹͣ");
        }

        /// <summary>
        /// �����ȴ�����
        /// </summary>
        public long ConcurrentWaitRecClients
        {
            get { return _listenReceviceService.ConcurrentWaitRecClients; }
        }
    }
}