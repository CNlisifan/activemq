using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lfz.Data;
using Lfz.MqListener.Shared.Enums;

namespace Lfz.MqListener.Models.ConfigCenter
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ConfigCenter_MqListener")]
    public partial class ConfigCenterMqListener : EntityBase<int>
    {
        /// <summary>
        ///  
        /// </summary>
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //��������
        public int  Id { get; set; }

        /// <summary>
        /// ����֮�󣬴�����Ϣ����
        /// </summary>
        public long? MsgCount { get; set; }

        /// <summary>
        /// ����֮�󣬴�����Ϣ���ݴ�С
        /// </summary>
        public long? MsgSize { get; set; }

        /// <summary>
        /// �ۼƴ�����Ϣ����
        /// </summary>
        public long? TotalMsgCount { get; set; }

        /// <summary>
        /// �ۼƴ�����Ϣ���ݴ�С
        /// </summary>
        public long? TotalMsgSize { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [StringLength(255)]
        public string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string MacAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string ProcessDirectory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(1000)]
        public string IpAddress { get; set; }

     
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MqListenerStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DownTime { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? LastHeartTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string ComputerName { get; set; }


        public override int GetKeyValue()
        {
            return Id;
        }
    }
}