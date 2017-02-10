namespace Lfz.Models
{
    /// <summary>
    /// ���Ը���״̬
    /// </summary>
    public enum PropertyTrackStatus
    { 
        /// <summary>
        /// δ�޸�
        /// </summary>
        [CustomDescription("δ�޸�")]
        Unchanged = 2,
      
        /// <summary>
        /// �������
        /// </summary>
        [CustomDescription("�����")]
        Added = 4,

        /// <summary>
        /// ��ɾ��
        /// </summary>
        [CustomDescription("��ɾ��")]
        Deleted = 8,

        /// <summary>
        /// ���޸�
        /// </summary>
        [CustomDescription("���޸�")]
        Modified = 16,
    }
}