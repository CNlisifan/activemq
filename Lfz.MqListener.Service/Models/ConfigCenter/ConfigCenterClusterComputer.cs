using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lfz.MqListener.Shared.Enums;

namespace Lfz.MqListener.Models.ConfigCenter
{
    /// <summary>
    /// 
    /// </summary> 
    [Table("ConfigCenter_ClusterComputer")]
    public partial class ConfigCenterClusterComputer
    {
        /// <summary>
        /// 
        /// </summary>
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //��������
        public int Id { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "ip��ַ����Ϊ��")] 
        [StringLength(50)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MqttPort { get; set; }

        /// <summary>
        /// ������IP
        /// </summary>
        public string LanIpAddress { get; set; }

        ///// <summary>
        ///// TODO ��ʱҪ�������������Ķ˿�һֱ
        ///// </summary>
        //public int LanPort { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int? LanMqttPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "�������Ʋ���Ϊ��")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string AdminUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string AccessUsername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string AccessPassword { get; set; }

        /// <summary>
        /// Ȩ��
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// ��Ⱥ��������
        /// </summary>
        public ClusterComputerType ComputerType { get; set; }

        /// <summary>
        /// ɾ�����
        /// </summary>
        public bool DelFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// ���ɾ��ʱ�䣬������ݱ��δɾ������ôΪɾ��ʱ��
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }
    }
}
