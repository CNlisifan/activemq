namespace PMSoft.Enums
{
    /// <summary>
    /// Ȩ������ 0����Ȩ�ޣ�1�ǿ��Ա�����ģ�99 ����
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// ����Ȩ��,����Ȩ��=0
        /// </summary>
        Shared = 0,

        /// <summary>
        /// �ɷ���Ȩ��=1
        /// </summary>
        Assignassignable = 1,
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        Other = 99,
        /// <summary>
        /// Ĭ��Ȩ��=0
        /// </summary>
        Default = Shared
    }
}