namespace Lfz.Data.Nh.Entities
{
    /// <summary>
    /// �ṩ�����Ż���ص�META��Ϣ
    /// </summary>
    public abstract class MetaEntityBase : EntityBase<int>
    {
        /// <summary>
        /// Meta����
        /// </summary>
        public virtual string MetaTitle { get; set; }

        /// <summary>
        /// Meta����
        /// </summary>
        public virtual string MetaDescription { get; set; }

        /// <summary>
        /// Meta��ֵ
        /// </summary>
        public virtual string MetaKeywords { get; set; }
    }
}