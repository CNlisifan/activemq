namespace Lfz.AutoUpdater.Rest
{
    /// <summary>
    /// JSON���Թ������б��ṩ��<see cref="RestResolverFilterAttribute"/>ʹ�ã���
    /// ��ö��ֵ��Ϊ��������Json���Խ���������Ψһ��ֵ���ڡ�
    /// </summary>
    internal enum JsonPropertyFilterEnum
    {
        /// <summary>
        /// ����Ҫ���˴���
        /// </summary>
        None = 0,

        /// <summary>
        /// RestResponse���͵�Result Json������Ҫ���滻
        /// </summary>
        RestResponseResult = 100,

        /// <summary>
        ///  SettingModel���͵�Data����Json������Ҫʹ�ù�����
        /// </summary>
        SettingModelData = 200
    }
}