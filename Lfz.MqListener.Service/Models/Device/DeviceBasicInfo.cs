using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lfz.MqListener.Models.Device
{
    [Table("Device_BasicInfo")]
    public partial class DeviceBasicInfo
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? StoreId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// �豸����
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        ///  �豸IP��ַ
        /// </summary>
        [StringLength(255)]
        public string IpAddress { get; set; }

        /// <summary>
        /// �豸MAC��ַ
        /// </summary>
        [StringLength(255)]
        public string MacAddress { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        /// <summary>
        ///  �豸״̬
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// ����ϵͳ�汾
        /// </summary>
        [StringLength(255)]
        public string OSVersion { get; set; }

        /// <summary>
        /// �����汾
        /// </summary>
        [StringLength(255)]
        public string DriverVersion { get; set; }

        /// <summary>
        /// Զ�̷�����Ϣ
        /// </summary>
        [StringLength(255)]
        public string RemoteAccessInfo { get; set; }

        /// <summary>
        /// ����ʱ���
        /// </summary>
        [StringLength(2000)]
        public string WorkTimePeriod { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public Guid? CreateUserId { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        public Guid? LastUpdateUserId { get; set; }
    }
}
