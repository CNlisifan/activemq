using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Lfz.Config;
using Lfz.Logging;

namespace Lfz.Utitlies
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AppUnitity
    {
        /// <summary>
        /// ����Ӧ��
        /// </summary> 
        /// <param name="exeFileNameOrWindowServiceName">exe�����ļ�����window��������</param>
        /// <param name="isWindowService">�Ƿ�windows����</param>
        public static void Restart(string exeFileNameOrWindowServiceName, bool isWindowService)
        {
            if (isWindowService)
                RestartWindowService(exeFileNameOrWindowServiceName);
            else RestartApplication(exeFileNameOrWindowServiceName);
        }

        /// <summary>
        /// ����Ӧ�ñ�֤�Զ������ɹ�
        /// </summary> 
        /// <param name="exeFileNameOrWindowServiceName">exe�����ļ�����window��������</param>
        /// <param name="isWindowService">�Ƿ�windows����</param>
        /// <param name="isCurrentProcess">�Ƿ������ǰ����</param>
        public static void KillOrStop(string exeFileNameOrWindowServiceName, bool isWindowService, bool isCurrentProcess = false)
        {
            if (isWindowService)
                StopWindowService(exeFileNameOrWindowServiceName);
            else
            {
                if (isCurrentProcess)
                {
                    var p = Process.GetCurrentProcess();
                    p.Kill();
                }
                else
                    KillProcess(exeFileNameOrWindowServiceName);
            }
        }

        /// <summary>
        /// �����Զ���������
        /// </summary>
        /// <param name="processExeName">��ִ���ļ�����(��Ե�ǰ·��)</param>
        /// <param name="isBackground"></param>
        public static void RunAutoUpdaterProcess(string processExeName, bool isBackground = true)
        {
            if (string.IsNullOrEmpty(processExeName)) return;
            var logger = LoggerFactory.GetLog(RunConfig.Current.LoggerType);
            string filename = "";
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = processExeName;
                processStartInfo.Arguments = @"" + isBackground + "";
                processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                filename = Path.Combine(processStartInfo.WorkingDirectory, processStartInfo.FileName);
                logger.Information(filename);
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "RunAutoUpdaterProcess");
            }
        }


        #region Methods

        /// <summary>
        /// ����exe����
        /// </summary> 
        public static void RestartApplication(string exeFileNameOrWindowServiceName)
        {
            try
            {
                var path = Path.Combine(Utils.MapPath("~/").TrimEnd('\\'), exeFileNameOrWindowServiceName);
                if (File.Exists(path))
                    Process.Start(path);
                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                LoggerFactory.GetLog(RunConfig.Current.LoggerType).Error(exception, "RestartApplication");
            }
        }

        /// <summary>
        /// ����windows����
        /// </summary> 
        public static void RestartWindowService(string exeFileNameOrWindowServiceName)
        {
            try
            {
                var controller = GetServiceController(exeFileNameOrWindowServiceName);
                if (controller != null &&
                    controller.Status == ServiceControllerStatus.Stopped)
                    controller.Start();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                LoggerFactory.GetLog(RunConfig.Current.LoggerType).Error(ex, "KillProcess.GetServiceController");
            }
        }



        private static ServiceController GetServiceController(string serviceName)
        {
            var service = ServiceController.GetServices();
            foreach (var serviceController in service)
            {
                if (String.Equals(serviceController.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase))
                    return serviceController;
            }
            return null;
        }
        /// <summary>
        /// ״̬window����
        /// </summary>
        /// <param name="exeFileNameOrWindowServiceName"></param>
        private static void StopWindowService(string exeFileNameOrWindowServiceName)
        {
            try
            {
                var controller = GetServiceController(exeFileNameOrWindowServiceName);
                if (controller != null)
                {
                    int count = 0;
                    while (count < 10)
                    {
                        if (controller.Status == ServiceControllerStatus.Running)
                            controller.Stop();
                        else if (controller.Status == ServiceControllerStatus.Stopped)
                        {
                            return;
                        }
                        Thread.Sleep(1000);
                        controller.Refresh();
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.GetLog(RunConfig.Current.LoggerType).Error(ex, "KillProcess.GetServiceController");
            }
        }
        private static void KillProcess(string exeFileNameOrWindowServiceName)
        {
            var fullname = Path.Combine(Utils.MapPath("~/").TrimEnd('\\'), exeFileNameOrWindowServiceName);
            var list = Process.GetProcessesByName(exeFileNameOrWindowServiceName);
            bool hasKill = false;
            foreach (var p in list)
            {
                try
                {
                    if (p.ProcessName.ToLower().Contains("idle") ||
                        p.ProcessName.ToLower().Contains("qq") ||
                        p.ProcessName.ToLower().Contains("360") ||
                        p.ProcessName.ToLower().Contains("ali") ||
                        p.ProcessName.ToLower().Contains("baidu") ||
                        p.ProcessName.StartsWith("system", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("svchost", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("spoo", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("sql", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("win", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("task", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("office", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("explor", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("csrss", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("conhost", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("chrome", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("intel", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("iis", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("lsm", StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.StartsWith("zhudong", StringComparison.OrdinalIgnoreCase)
                        )
                        continue;
                    if (String.Equals(p.MainModule.FileName, fullname, StringComparison.OrdinalIgnoreCase))
                    {
                        hasKill = true;
                        p.Kill();
                    }
                }
                catch (Exception exception)
                {
                    //LoggerFactory.GetLog(RunConfig.Current.GetLoggerType()).Error(exception, "KillProcess.GetProcessesByName");
                }
            }
            if (hasKill == true) return;
            list = Process.GetProcesses();
            foreach (var process in list)
            {
                string name = process.ProcessName;
                try
                {
                    if (name.Equals(exeFileNameOrWindowServiceName, StringComparison.OrdinalIgnoreCase)
                        || String.Equals(process.MainModule.FileName, fullname, StringComparison.OrdinalIgnoreCase))
                    {
                        process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    LoggerFactory.GetLog(RunConfig.Current.LoggerType).Error(ex, "KillProcess.GetProcesses");
                }
            }
        }
        #endregion
    }
}