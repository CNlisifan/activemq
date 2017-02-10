using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lfz.Collections;
using NHibernate;

namespace Lfz.Data.Nh
{
    public interface INhRepository<T>
    {
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity); 

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// ����IDɾ������
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// ���ƿ�¡����
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        void Copy(T source, T target);

        /// <summary>
        /// ���»���
        /// </summary>
        void Flush();

        /// <summary>
        /// ����ID��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// IQueryable ���ݱ�
        /// </summary>
        IQueryable<T> Table { get; }
       
        /// <summary> 
        /// ���ô��������� 
        /// </summary>
        /// <param name="queryName">The query can be either in HQL or SQL format.</param>
        /// <returns></returns>
        IQuery GetNamedQuery(string queryName);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// ����һ����ʵ���¼�б�
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// ����һ����ʵ���¼�б�
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order);

        /// <summary>
        /// ����һ����ʵ���¼�б�
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex">��0��ʼ����</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPageOfItems<T> GetPaged(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int pageIndex,
                                 int pageSize);

        bool Exists(Expression<Func<T, bool>> func);
    }
}