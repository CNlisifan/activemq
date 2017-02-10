//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : ServiceBase
//        DESCRIPTION : �������,�ṩIsRunning,Start,Stop��
//
//        Created By �ַ��� at  2013-01-04 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   
/***************************************************************************
 *  ���������� �������,�ṩIsRunning,Start,Stop��
 *  �����ˣ��ַ���
 *  ���ʱ�䣺2013-01-04
 *  �޸��ˣ�      �޸�ʱ�䣺
 *  �޸�������
 ****************************************************************************/

using System;
using Lfz.Logging;

namespace Lfz.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    public abstract class ServiceBase : IService
    {

        /// <summary>
        /// ��������
        /// </summary>
        public string ServiceName { get; protected set; }

        protected readonly object IsRunningLockObj = new object();

        /// <summary>
        /// ��ȡ����״̬ 
        /// </summary>
        public ServiceStatus Status
        {

            get
            {
                ServiceStatus flag;
                lock (IsRunningLockObj)
                {
                    flag = _status;
                }
                return flag;
            }
            protected set
            {
                lock (IsRunningLockObj)
                {
                    _status = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected ServiceBase()
        {
            Logger = LoggerFactory.GetLog();
            ServiceName = this.GetType().Name;
            Status = ServiceStatus.UnStarted;
            this._enabled = true;
        }



        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public virtual bool IsRunning
        {
            get { return Status == ServiceStatus.Running; }
        }

        /// <summary>
        /// �����Ѿ�ֹͣ���¼���
        /// </summary>
        public event ServiceEventHandler Stoped;

        /// <summary>
        /// ��������ֹͣ���¼���,��ʼ���ر�����
        /// </summary>
        public event ServiceEventHandler Stoping;

        /// <summary>
        /// �����Ѿ��������¼���
        /// </summary>
        public event ServiceEventHandler Started;

        /// <summary>
        /// ���������������¼�������ʼ����������
        /// </summary>
        public event ServiceEventHandler Starting;

        #region ICommandService ��Ա

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            //δ�����ù��ܵķ���������
            if (!this.Enabled)
            {
                Status = ServiceStatus.UnStarted;
                Logger.Log(LogLevel.Debug, string.Format("{0}��ֹʹ�ã�����ʧ��", ServiceName));
                return;
            }
            if (!IsRunning)
            {
                Status = ServiceStatus.Starting;
                if (Starting != null) Starting();
                //���������û�������⴦�������״̬Ӧ��ΪStarting
                if (Status != ServiceStatus.Starting)
                {
                    //��������
                    Status = ServiceStatus.UnStarted;
                    Logger.Log(LogLevel.Debug, string.Format("{0}����ʧ��", ServiceName));
                    return;
                } 
                if (Started != null) Started(); 
                //���������û�������⴦�������״̬Ӧ��ΪStarting
                if (Status != ServiceStatus.Starting)
                {
                    //�������� 
                    Status = ServiceStatus.UnStarted;
                    Logger.Log(LogLevel.Debug, string.Format("{0}����ʧ��", ServiceName));
                    return;
                } 
                Status = ServiceStatus.Running;
                Logger.Log(LogLevel.Debug, string.Format("{0}����", ServiceName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            try
            {

                Status = ServiceStatus.Stoping;
                if (Stoping != null) Stoping();
                if (Status != ServiceStatus.Stoping)
                {
                    Logger.Log(LogLevel.Debug, string.Format("{0} ��ֹֹͣ�", ServiceName));
                    return;
                }
                if (Stoped != null) Stoped();
                Status = ServiceStatus.Stoped;
                Logger.Log(LogLevel.Debug, string.Format("{0}����", ServiceName));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, string.Format("{0}����", ServiceName));
            }
        }

        #endregion

        #region IDisposable ��Ա

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public virtual void Dispose()
        {
            Stop();
        }

        #endregion

        #region IService ��Ա

        /// <summary>
        /// ������־
        /// </summary>
        public ILogger Logger { get; set; }

        #endregion

        private bool _enabled;
        private ServiceStatus _status;

        /// <summary>
        /// �����Ƿ�����
        /// </summary>
        public virtual bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Restart()
        {
            if (this.IsRunning) this.Stop();
            this.Start();
            return true;
        }

    }

    /// <summary>
    /// ����״̬
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// δ����(��ֵ��ЧΪ�Ѿ�ֹͣStoped)
        /// </summary>
        [CustomDescription("δ����")]
        UnStarted = 0,
        /// <summary>
        /// ��������
        /// </summary>
        [CustomDescription("��������")]
        Starting = 1,
        /// <summary>
        /// ������
        /// </summary>
        [CustomDescription("������")]
        Running = 2,
        /// <summary>
        /// ����ֹͣ��=3
        /// </summary>
        [CustomDescription("����ֹͣ��")]
        Stoping = 3,
        /// <summary>
        /// �Ѿ�ֹͣ(��ֵ��ЧΪδ����UnStarted)=0
        /// </summary>
        [CustomDescription("�Ѿ�ֹͣ")]
        Stoped = UnStarted
    }
}