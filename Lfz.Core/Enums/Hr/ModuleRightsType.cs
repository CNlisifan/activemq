namespace Lfz.Enums.Hr
{
    /// <summary>
    /// ģ��Ȩ������
    /// </summary>
    public enum ModuleRightsType
    {
        /// <summary>
        /// ˭�����Բ鿴
        /// </summary>
        [CustomDescription("����Ȩ��")]
        Common = 0,

        /// <summary>
        /// �ɷ����
        /// </summary>
        [CustomDescription("�ɷ���")]
        Assignabled = 1, 

        /// <summary>
        /// ���ɷ����Ȩ�ޣ�ϵͳ��ɫ����
        /// </summary>
        [CustomDescription("���ɷ���")]
        NonAssignabled=99
    }
}