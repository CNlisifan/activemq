namespace Lfz.Models
{
    /// <summary>
    /// ҳ����Ϣ�޸�
    /// </summary>
    public class PageIndexChangedHit : IViewStateSearchHit
    {
        public int PageIndex { get; set; }
        public string SearchHitViewState { get; set; }
    }
}