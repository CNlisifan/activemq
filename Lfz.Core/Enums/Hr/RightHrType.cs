using System;

namespace Lfz.Enums.Hr
{
    /// <summary>
    /// Ȩ�޵��������ͣ�0 ������֯����� 1:Ա�� 2:���� 4:�������� 8:���Ÿ�λ  32768:����
    /// </summary> 
    [Flags]
    public enum RightHrType
    {
        /// <summary>
        /// 0 ������֯�����
        /// </summary>
        None = 0,

        /// <summary>
        /// 1  ����ָ��Ա�������������𣬸�λ����ɫ����άȨ�ޱ�
        /// </summary>
        Employee = 1,

        /// <summary>
        /// 2  ����
        /// </summary>
        Department = 2,

        /// <summary>
        /// 4 ��������
        /// </summary>
        Level = 4,

        /// <summary>
        /// 8 ���Ÿ�λ
        /// </summary>
        Position = 8,
         
        /// <summary>
        /// 32768  ����
        /// </summary>
        Other = 32768
    }
}