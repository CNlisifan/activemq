//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : TcpClientService
//        DESCRIPTION : Tcp�ͻ��˷�װ
//
//        Created By �ַ��� at  2013-01-08 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System;
using System.Net;
using System.Net.Sockets;
using Lfz.Logging;

namespace Lfz.Network
{
    /// <summary>
    /// Tcp�ͻ��˷�װ
    /// </summary>
    public class TcpClientService
    {
        private readonly string _serverIp;
        private readonly int _port;
        private readonly IPEndPoint _localEP;
        private readonly int _sendTimeout;
        private readonly int _revTimeout;
        private readonly int _maxRecBufSize;

        /// <summary>
        /// ��־
        /// </summary>
        public ILogger Logger { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverIp">����IP</param>
        /// <param name="port">�˿�</param>
        /// <param name="localEP">����IP</param>
        /// <param name="sendTimeout">���ͳ�ʱʱ��</param>
        /// <param name="revTimeout">���ճ�ʱʱ��</param> 
        public TcpClientService(string serverIp, int port, IPEndPoint localEP, int sendTimeout, int revTimeout)
            : this(serverIp, port, localEP, sendTimeout, revTimeout,2048)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverIp">����IP</param>
        /// <param name="port">�˿�</param>
        /// <param name="localEP">����IP</param>
        /// <param name="sendTimeout">���ͳ�ʱʱ��</param>
        /// <param name="revTimeout">���ճ�ʱʱ��</param>
        /// <param name="maxRecBufSize">�����ջ����С</param>
        public TcpClientService(string serverIp, int port, IPEndPoint localEP, int sendTimeout, int revTimeout, int maxRecBufSize)
        {
            Logger = LoggerFactory.GetLog(); 
            _serverIp = serverIp;
            _port = port;
            _localEP = localEP;
            _sendTimeout = sendTimeout;
            _revTimeout = revTimeout;
            _maxRecBufSize = maxRecBufSize;
        }

        /// <summary>
        /// TCP�ͻ��˿�ʼ��������
        /// </summary>
        /// <param name="data">�����͵�����</param>
        public void BeginSend(byte[] data)
        {
            TcpClient client;
            client = _localEP != null ? new TcpClient(_localEP) : new TcpClient();
            client.SendTimeout = _sendTimeout;
            client.ReceiveTimeout = _revTimeout;
            client.BeginConnect(_serverIp, _port, BeginConnectCallBack,
                new CallBackState
                {
                    Client = client,
                    Data = data
                });
        }

        private void BeginConnectCallBack(IAsyncResult result)
        {
            var state = result.AsyncState as CallBackState;
            if (state != null)
                try
                {
                    state.Client.EndConnect(result);
                    var stream = state.Client.GetStream();
                    state.NetworkStream = stream;
                    stream.BeginWrite(state.Data, 0, state.Data.Length, BeginWriteCallBack, state);
                }
                catch (Exception e)
                {
                    CloseClient(state);
                    Logger.Log(LogLevel.Error, "BeginConnectCallBack:" + e.Message);
                }
        }

        private void CloseClient(CallBackState state)
        {
            var client = state.Client;
            client.Client.Shutdown(SocketShutdown.Send);
            client.Client.Close();
            state.NetworkStream.Dispose();
            client.Close();
            (client as IDisposable).Dispose();
        }

        private void BeginWriteCallBack(IAsyncResult result)
        {
            string ip = string.Empty;
            int port = 0;
            var state = result.AsyncState as CallBackState;
            if (state != null)
                try
                {
                    state.NetworkStream.EndWrite(result);
                    if (ReadCompleted != null)
                    {
                        state.Data = new byte[2000];
                        state.NetworkStream.BeginRead(state.Data, 0, state.Data.Length, BeginReadCallBack, state);
                    }
                    else CloseClient(state);
                }
                catch (Exception e)
                {
                    if (ReadCompleted != null)
                    {
                        CloseClient(state);
                    }
                    Logger.Log(LogLevel.Error, string.Format("BeginWriteCallBack:{0} IP:{1} port:{2}", e.Message, ip, port));
                }
        }

        private void BeginReadCallBack(IAsyncResult result)
        {
            var state = result.AsyncState as CallBackState;
            if (state != null)
                try
                {
                    var args = new DataEventArgs<byte[]>();
                    var readCount = state.NetworkStream.EndRead(result);
                    args.Data = new byte[readCount];
                    Buffer.BlockCopy(state.Data, 0, args.Data, 0, readCount);
                    ReadCompleted(this, args);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "BeginReadCallBack:" + e.Message);
                }
                finally
                {
                    CloseClient(state);
                }
        }


        /// <summary>
        /// TCP�ͻ��˶�ȡ����¼�
        /// </summary>
        public event EventHandler<DataEventArgs<byte[]>> ReadCompleted;


    }
}