namespace Lfz.MqListener.Shared.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum NMSMessageType
    {
        /// <summary>
        /// ����Ϣ�����
        /// </summary>
        None = 0,

        /// <summary>
        /// ���ݱ�ͬ��
        /// </summary>
        SyncByTableName = 1,
        /// <summary>
        /// ��ȡͬ����Ϣ
        /// </summary>
        SyncByTableNamePullMessage = 2,

        /// <summary>
        /// 
        /// </summary>
        MenuSolutionVersion = 10,

        /// <summary>
        /// 
        /// </summary>
        MenuSolutionMdType = 11,

        /// <summary>
        /// 
        /// </summary>
        MenuSolutionMFType = 12,

        /// <summary>
        /// 
        /// </summary>
        MenuSolutionMenuConfig = 13,

        #region ��������


        #endregion

        /// <summary>
        /// �Զ�����
        /// </summary>
        AutoUpdateInfo=998,
        /// <summary>
        /// ������
        /// </summary>

        Heart = 999
    }
}