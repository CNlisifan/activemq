using System;

namespace Lfz.Models
{
    /// <summary>
    /// ������ֵ�Ĳ�ѯ����
    /// </summary>
    public class KeySearchHit : SearchHit
    {
        /// <summary>
        /// ��ֵ
        /// </summary>
        public Guid Key { get; set; }
    }
}