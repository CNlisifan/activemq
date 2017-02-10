using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Lfz.Collections;
using Lfz.Config;
using Lfz.Redis;
using Lfz.Utitlies;

namespace Lfz.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCacheManager
    {
        #region ���ԡ��ֶ�

        /// <summary>
        /// ������������ǰ׺
        /// </summary>  
        private readonly int _regionName;
        private readonly int _defaultCacheTime;

        /// <summary>
        /// 
        /// </summary>
        public int DefaultCacheTime
        {
            get { return _defaultCacheTime; }
        }


        #endregion

        #region ���캯��

        /// <summary>
        /// ������������ǰ׺
        /// </summary>  
        /// <param name="regionName">����ID</param>
        /// <param name="defaultCacheTime">Ĭ�ϻ���ʱ��120����(2��Сʱ)����λ����</param>
        public RedisCacheManager(int regionName, int defaultCacheTime = 120)
        {
            _regionName = regionName;
            _defaultCacheTime = defaultCacheTime;
        }

        #endregion

        #region ������

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param> 
        /// <returns></returns>
        public string GetCacheKey<TKey>(TKey key)
        {
            if (typeof (TKey) == typeof (Guid))
            {
                return string.Format("{0}{1}_{2}", AppSettingsHelper.CacheItemPrefix, (int)_regionName, GetGuidString(key));
            }
            return string.Format("{0}{1}_{2}", AppSettingsHelper.CacheItemPrefix, (int)_regionName, key);
        } 

        private string GetGuidString(object key)
        {
            return ((Guid)key).ToString("n");
        }

        #endregion

        /// <summary>
        /// ���ݼ�ֵ��ȡ����»����ֵ
        /// </summary>
        /// <param name="key">�����ֵ</param> 
        /// <param name="func"></param> 
        /// <returns>���ص�ǰ��ֵ�����Ļ������</returns>
        public virtual TResult Get<TKey, TResult>(TKey key, Func<TKey, TResult> func)
        {
            //��������
            var cacheKey = GetCacheKey(key);
            var result = RedisBase.Current.Item_Get<TResult>(cacheKey);
            //��������ֵΪ�գ���ô�Ƴ������ֵ
            if (!TypeParse.IsDefaultValue(result)) return result;
            //����ֵ
            result = func(key);
            //null ,0,Guid.Empty,string.Empty��ֵ��������
            if (TypeParse.IsDefaultValue(result))
            {
                return result;
            }
            //���û���
            Set(key, result, _defaultCacheTime);
            return result;
        }

        /// <summary>
        /// ���ݼ�ֵ��ȡ����»����ֵ
        /// </summary>
        /// <param name="key">�����ֵ</param> 
        /// <param name="func"></param> 
        /// <returns>���ص�ǰ��ֵ�����Ļ������</returns>
        public virtual IEnumerable<TResult> GetList<TKey, TResult>(TKey key, Func<TKey,
            IEnumerable<TResult>> func)
        {
            //��������
            var cacheKey = GetCacheKey(key);
            IEnumerable<TResult> result = RedisBase.Current.List_GetList<TResult>(cacheKey);
            //��������ֵΪ�գ���ô�Ƴ������ֵ  
            if (result != null && result.Any()) return result;
            //����ֵ
            result = func(key);
            if (result == null)
            {
                return null;
            }
            var cacheList = result.ToReadOnlyCollection();
            //���û���
            SetList(key, cacheList, _defaultCacheTime);
            return cacheList;
        }

        /// <summary>
        /// ���ݼ�ֵ��ȡ����»����ֵ
        /// </summary>
        /// <param name="key">�����ֵ</param> 
        /// <param name="func"></param> 
        /// <returns>���ص�ǰ��ֵ�����Ļ������</returns>
        public virtual IDictionary<TKey1, TResult> GetDictionary<TKey, TKey1, TResult>(TKey key, Func<TKey,
            IDictionary<TKey1, TResult>> func)
        {

            //��������
            var cacheKey = GetCacheKey(key);
            IDictionary<TKey1, TResult> result = RedisBase.Current.Item_Get<IDictionary<TKey1, TResult>>(cacheKey);
            //��������ֵΪ�գ���ô�Ƴ������ֵ  
            if (result != null && result.Any()) return result;
            //����ֵ
            result = func(key);
            if (result == null)
            {
                return null;
            }
            var cacheList = result;
            //���û���
            SetDictionary(key, cacheList, _defaultCacheTime);
            return cacheList;
        }

        /// <summary>
        /// ��ӻ���»������ݡ�������»��棬��ô�������ʱ�䲻������
        /// </summary>
        /// <param name="key">key</param> 
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        /// <param name="changeMonitorList">�޸ļ�����</param>
        public void SetDictionary<TKey, TKey1, TResult>(TKey key, IDictionary<TKey1, TResult> data, int cacheTime,
            IEnumerable<ChangeMonitor> changeMonitorList = null)
        {
            if (data == null || data.Count == 0) return;
            //��������
            var cacheKey = GetCacheKey(key);
            var expiration = cacheTime <= 0 ? _defaultCacheTime : cacheTime;
            RedisBase.Current.Item_Set<IDictionary<TKey1, TResult>>(cacheKey, data, expiration);
        }

        /// <summary>
        /// ���ݼ�ֵ��ȡ����»����ֵ
        /// </summary>
        /// <param name="key">�����ֵ</param> 
        /// <returns>���ص�ǰ��ֵ�����Ļ������</returns>
        public virtual TResult Get<TKey, TResult>(TKey key)
        {
            //��������
            var cacheKey = GetCacheKey(key);
            var result = RedisBase.Current.Item_Get<TResult>(cacheKey);
            //��������ֵΪ�գ���ô�Ƴ������ֵ
            if (!TypeParse.IsDefaultValue(result)) return result;
            Remove(key);
            return default(TResult);
        }


        /// <summary>
        /// ��ӻ���»������ݡ�������»��棬��ô�������ʱ�䲻������
        /// </summary>
        /// <param name="key">key</param> 
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        /// <param name="changeMonitorList">�޸ļ�����</param>
        public void SetList<TKey, TResult>(TKey key, IList<TResult> data, int cacheTime,
            IEnumerable<ChangeMonitor> changeMonitorList = null)
        {
            if (data == null || data.Count == 0) return;
            //��������
            var cacheKey = GetCacheKey(key);
            var expiration =
                DateTime.Now +
                (cacheTime <= 0 ? TimeSpan.FromMinutes(_defaultCacheTime) : TimeSpan.FromMinutes(cacheTime));
            RedisBase.Current.List_Add<IList<TResult>>(cacheKey, data);
            RedisBase.Current.List_SetExpire(cacheKey, expiration);
        }

        /// <summary>
        /// ��ӻ���»������ݡ�������»��棬��ô�������ʱ�䲻������
        /// </summary>
        /// <param name="key">key</param> 
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        /// <param name="changeMonitorList">�޸ļ�����</param>
        public void Set<TKey, TResult>(TKey key, TResult data, int cacheTime,
            IEnumerable<ChangeMonitor> changeMonitorList = null)
        {
            //��������
            var cacheKey = GetCacheKey(key);
            var expiration = cacheTime <= 0 ? _defaultCacheTime : cacheTime;
            RedisBase.Current.Item_Set<TResult>(cacheKey, data, expiration);
        }

        #region �Ƴ���������ջ���

        /// <summary>
        /// �Ƴ�ָ������
        /// </summary>
        /// <param name="key">key</param> 
        public void Remove<TKey>(TKey key)
        {
            Remove(GetCacheKey(key));
        }

        private void Remove(string key)
        {
            RedisBase.Current.Item_Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public void RemovePattern(string str)
        {
            RedisBase.Current.RemovePattern(string.Format("*{0}*", str));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public void RemoveStartWith(string str)
        {
            str = string.Format("{0}*", str);
            RedisBase.Current.RemovePattern(str);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveRegion()
        {
            var str = string.Format("{0}{1}_*", AppSettingsHelper.CacheItemPrefix, (int)_regionName);
            RedisBase.Current.RemovePattern(str);
        }


        #endregion

    }
}