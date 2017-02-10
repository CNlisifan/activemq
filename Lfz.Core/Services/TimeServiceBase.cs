//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : TimeServiceBase
//        DESCRIPTION : ��ʱ�������
//
//        Created By �ַ��� at  2012-12-12 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System;
using System.Timers;

namespace Lfz.Services
{
    /// <summary>
    /// ��ʱ�������
    /// </summary> 
    public abstract class TimeServiceBase : ServiceBase 
    {
        private readonly Timer _timer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        protected TimeServiceBase(TimeSpan interval)
        {
            _timer = new Timer(interval.TotalMilliseconds);
            _timer.Elapsed += Excute;
            _timer.Enabled = false;

            Stoping += OnClosing;
            Starting += OnStarting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void Excute(object sender, ElapsedEventArgs e);

        /// <summary>
        /// �������� System.Timers.Timer.Elapsed �¼��ļ��ʱ�䣨�Ժ���Ϊ��λ����Ĭ��Ϊ 100 ����
        /// </summary>
        /// <param name="interval"></param>
        public void ResetInterval(TimeSpan interval)
        {
            _timer.Interval = interval.TotalMilliseconds;
        }

        /// <summary>
        /// ������������
        /// </summary>
        private void OnStarting()
        {
            _timer.Start();
        }

        /// <summary>
        /// ��������ֹͣ
        /// </summary>
        private void OnClosing()
        {
            _timer.Stop();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public override void Dispose()
        {
            _timer.Close();
            _timer.Dispose();
            base.Dispose();
        }
    }

     
}