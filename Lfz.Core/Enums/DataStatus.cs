namespace Lfz.Enums
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public enum DataStatus
    {
        /// <summary>
        /// ���ڡ���Ч�ġ��Ѿ��ر�
        /// </summary>
        [CustomDescription("�ر�")]
        InValid = 0,

        /// <summary>
        /// ��Ч������ʹ�á��Ѿ�����
        /// </summary>
        [CustomDescription("����")]
        Valid = 1,

        /// <summary>
        /// ����
        /// </summary>
        [CustomDescription("����")]
        Other = 99
    }
}