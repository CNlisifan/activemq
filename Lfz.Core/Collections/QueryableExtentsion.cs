using System;
using System.Collections.Generic;
using System.Linq;

namespace Lfz.Collections
{
    /// <summary>
    /// IQueryable�б���չ����
    /// </summary>
    public static class QueryableExtentsion
    {
        /// <summary>
        /// ��ȡ��ҳ�б�queryable��0��ʼ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfItems<T> GetPagedFromQueryable<T>(this IQueryable<T> queryable, int totalCount, int pageIndex, int pageSize)
        {
            //��ʼ��ҳ����Ϣ
            var pageOfItems = new PageOfItems<T>
                                  {
                                      PageIndex = pageIndex,
                                      PageSize = pageSize,
                                      TotalItemCount = totalCount
                                  };
            pageOfItems.AddRange(queryable.Skip(pageOfItems.StartIndex).Take(pageOfItems.PageSize).ToReadOnlyCollection().ToList());
            return pageOfItems;
        }

        /// <summary>
        /// ��ȡ��ҳ�б�IEnumerable��0��ʼ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfItems<T> GetPaged<T>(this IEnumerable<T> queryable, int totalCount, int pageIndex, int pageSize)
        {
            //��ʼ��ҳ����Ϣ
            var pageOfItems = new PageOfItems<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemCount = totalCount
            };
            pageOfItems.AddRange((queryable.Skip(pageOfItems.StartIndex).Take(pageOfItems.PageSize).ToReadOnlyCollection()).ToList());
            return pageOfItems;
        }

        /// <summary>
        /// ������T�ķ�ҳ�����б�ת��Ϊ����TView�ķ�ҳ�����б�
        /// </summary>
        /// <typeparam name="T">ԭʼ��������</typeparam>
        /// <typeparam name="TView">���ص���������</typeparam>
        /// <param name="items">�����б�</param>
        /// <param name="selector">����ת��ѡ����</param>
        /// <returns>����TView�ķ�ҳ�����б�</returns>
        public static IPageOfItems<TView> ToView<T, TView>(this IPageOfItems<T> items, Func<T, TView> selector)
        {
            //��ʼ��ҳ����Ϣ
            var pageOfItems = new PageOfItems<TView>
            {
                PageIndex = items.PageIndex,
                PageSize = items.PageSize,
                TotalItemCount = items.TotalItemCount,
            };
            var list = items.Select(selector);
            pageOfItems.AddRange(list.ToList());
            return pageOfItems;
        }
         
    }
}