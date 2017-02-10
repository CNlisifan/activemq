using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lfz.MqListener.Models.ConfigCenter
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ConfigCenter_RedisCacheKey")]
    public partial class ConfigCenterRedisCacheKey
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [StringLength(255)]
        public string CacheKey { get; set; }

        /// <summary>
        /// �������õ���
        /// </summary>
        public int RedisInstanceId { get; set; }

        /// <summary>
        /// ����ʱ�䣬��λ����
        /// </summary>
        public int Expired { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}