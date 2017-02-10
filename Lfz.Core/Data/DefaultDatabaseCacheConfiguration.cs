using NHibernate.Cfg.Loquacious;

namespace PMSoft.Data {
    /// <summary>
    /// ���ݻ�������
    /// </summary>
    public class DefaultDatabaseCacheConfiguration : IDatabaseCacheConfiguration {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public void Configure(ICacheConfigurationProperties cache) {
            cache.UseQueryCache = false;
        }
    }
}