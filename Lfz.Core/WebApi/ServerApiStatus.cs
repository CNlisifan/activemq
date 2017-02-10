namespace Lfz.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public enum ServerApiStatus
    {
        /// <summary>
        /// �����ɹ�
        /// </summary>
        Success = 0,

        /// <summary>
        /// ����token��Ч
        /// </summary> 
        InvalidAccessToken = 1,
        /// <summary>
        /// Api��Ч
        /// </summary> 
        InvalidApiAndSecret = 2,

        /// <summary>
        /// �ͻ�ID��Ч
        /// </summary> 
        InvalidClientId = 3,
        /// <summary>
        /// ��״̬
        /// </summary>
        None = -1,

        /// <summary>
        /// ��״̬
        /// </summary>
        Error = -99
    }
}