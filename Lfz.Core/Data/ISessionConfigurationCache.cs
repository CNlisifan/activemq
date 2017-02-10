using System;
using NHibernate.Cfg;

namespace PMSoft.Data
{
    /// <summary>
    /// Session���û���
    /// </summary>
    public interface ISessionConfigurationCache 
    {
        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        Configuration GetConfiguration(Func<Configuration> builder);
    }
}