using System.Collections.Generic;

namespace Lfz.WebApi
{
    /// <summary>
    /// ��ҳ���ݽ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedApiResult<T> : ApiResult<IEnumerable<T>>
    {

        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// ��ҳ��С
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// �ܼ�¼����
        /// </summary>
        public int TotalItemCount { get; set; }
    }
}