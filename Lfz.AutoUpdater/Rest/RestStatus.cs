namespace Lfz.AutoUpdater.Rest
{
    /// <summary>
    /// 
    /// </summary>
    internal enum RestStatus
    {
        /// <summary>
        /// �޶���
        /// </summary>
        [CustomDescription("�޶���")]
        None = 0,

        /// <summary>
        /// �����ɹ�
        /// </summary>
        [CustomDescription("�����ɹ�")]
        Success = 1,


        /// <summary>
        /// ��Ȩ��
        /// </summary>
        [CustomDescription("��Ȩ��")]
        NoPower = 2,

        /// <summary>
        /// ����ʵ��
        /// </summary>
        [CustomDescription("����ʵ��")]
        NoImp = 4,

        /// <summary>
        /// ��������
        /// </summary>
        [CustomDescription("��������")]
        Error = 8,

        /// <summary>
        /// ��Ч��֤��Ϣ
        /// </summary>
        [CustomDescription("��Ч��֤��Ϣ")]
        InvalidCredential = 101,

        /// <summary>
        /// ������,û��POST��������
        /// </summary>
        [CustomDescription("������")]
        InvalidChars = 102,
    }
}