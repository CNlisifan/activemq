using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lfz.MqListener.Models.ConfigCenter
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ConfigCenter_MqTable")]
    public partial class ConfigCenterMqTable
    {
        /// <summary>
        /// �ŵ�ID
        /// </summary>
        [Key]
        public Guid StoreId { get; set; } 
         
        /// <summary>
        /// �ͻ�Ĭ����Ϣ��������ID
        /// </summary>
        public int? MqInstanceId { get; set; }

        /// <summary>
        /// MQ����ʵ��ID
        /// </summary>
        public int? MqListenerId { get; set; }
         

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// ����ʱ�䣬��λ����
        /// </summary>
        public int Expired { get; set; }

    }
}
