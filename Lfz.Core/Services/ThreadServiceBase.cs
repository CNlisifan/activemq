//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : ThreadServiceBase
//        DESCRIPTION : �̼߳����������
//
//        Created By �ַ��� at  2012-11-08 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System;
using System.Threading;
using Lfz.Logging;

namespace Lfz.Services
{
    /// <summary>
    /// �̼߳����������
    /// </summary>
    /// <typeparam name="TEventArgs">��չ����</typeparam>
    public abstract class ThreadServiceBase  : ServiceBase 
    {
        private Thread _mUdpThread;

        /// <summary>
        /// 
        /// </summary>
        protected ThreadServiceBase()
        {
            var str = "";
            if (ThreadStartParams != null) str = "_" + ThreadStartParams.ToString();
            ServiceName = this.GetType().Name + str;

            Stoping += OnClosing;
            Started += OnStarted;
        }

        /// <summary>
        /// �̷߳���
        /// </summary>
        protected abstract void Excute(object obj);

        /// <summary>
        /// �߳���������
        /// </summary>
        public object ThreadStartParams { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        private void OnStarted()
        {
            _mUdpThread = new Thread(RunThread) { Name = ServiceName };
            _mUdpThread.Start(ThreadStartParams);
        }

        private void RunThread(object obj)
        {
            try
            {
                Excute(obj);
            }
            catch (Exception ex)
            { 
                Status = ServiceStatus.Stoped;
                Logger.Error(this.ServiceName + " ������Ϣ:" + ex.Message);
            }
            finally
            { 
                Status = ServiceStatus.Stoped;
                Logger.Log(LogLevel.Debug, string.Format("{0}�̷߳�������ϲ��Զ�����", ServiceName));
            }
        }

        /// <summary>
        /// ��������ֹͣ
        /// </summary>
        private void OnClosing()
        {
            if (_mUdpThread != null)
            {
                int count = 0;//������ѭ��
                while (count < 100 && (_mUdpThread.ThreadState == ThreadState.Running || _mUdpThread.ThreadState == ThreadState.WaitSleepJoin))
                {
                    if (_mUdpThread.ThreadState == ThreadState.Running) _mUdpThread.Abort();
                    Thread.Sleep(10);
                    count++;
                }
            }
        } 
    }
     
}