//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : ILogger
//        DESCRIPTION : ��־�ӿ�
//
//        Created By �ַ��� at  2013-01-04 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================  

using System;

namespace Lfz.Logging 
{
    /// <summary>
    /// ��־�����ӿ�
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsEnabled(LogLevel level);
        /// <summary>
        /// summary
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Log(LogLevel level, Exception exception, string format, params object[] args);
        /// <summary>
        /// summary
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Log(LogLevel level, string message, Exception exception);
        /// <summary>
        /// ������ʾ��Ϣ��¼
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Log(string message, Exception exception);
        /// <summary>
        /// ��־��¼
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        void Log(LogLevel level, string message );

        /// <summary>
        /// ��ʾ��Ϣ��¼
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);
    }
}