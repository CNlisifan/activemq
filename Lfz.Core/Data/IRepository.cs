using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Lfz.Collections;
using Lfz.Logging;

namespace Lfz.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        T GetFirstOrDefault(string whereClause="");
        /// <summary>
        /// ��־�ṩ�ӿ�
        /// </summary>
        ILogger Logger { get; set; }
            

        IEnumerable<T> FormatModel(DataSet reader, int count);

        T FormatModel(DataRow reader );
          
        /// <summary>
        /// ��ҳ���ݻ�ȡ
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="fields"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPageOfItems<T> GetPaged(string whereClause, string fields, string orderBy, int pageIndex, int pageSize);

        /// <summary>
        /// ����Id��ȡʵ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetModel(int id);

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="whereClause"></param> 
        /// <param name="orderBy"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(string whereClause, string orderBy, int count);

        /// <summary>
        /// ��¼������ȡ
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        int Count(string whereClause);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        bool Exists(string whereClause);

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool Exists(string whereClause, SqlTransaction transaction);

        /// <summary>
        /// ��: ����ӷ����������������۳�����
        /// </summary>
        /// <param name="model">ArmlenoperateModel ʵ��</param> 
        /// <returns>���ؽ����0(��Ӵ���)��>0(��ӳɹ�)</returns>
        int Create(T model);

        /// <summary>
        /// ��: ����ӷ����������������۳�����
        /// </summary>
        /// <param name="model">ArmlenoperateModel ʵ��</param>
        /// <param name="transaction">���ݿ��������</param>
        /// <returns>���ؽ����0(��Ӵ���)��>0(��ӳɹ�)</returns>
        int Create(T model, SqlTransaction transaction);

        /// <summary>
        /// ��:T���޸ķ����������������۳�����
        /// </summary>
        /// <param name="model">T ʵ��</param> 
        /// <returns>���ؽ����0(�޸Ĵ���)��>0(�޸ĳɹ�)</returns>
        int Update(T model);

        /// <summary>
        /// ���������ֶ�ֵ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fieldname"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        int Update(int id,string fieldname,object fieldValue);

        /// <summary>
        /// ��:T ���޸ķ����������������۳�����
        /// </summary>
        /// <param name="model">T ʵ��</param>
        /// <param name="transaction">���ݿ��������</param>
        /// <returns>���ؽ����0(�޸Ĵ���)��>0(�޸ĳɹ�)</returns>
        int Update(T model, SqlTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// ��:d_ArmLenOperate��ɾ�������������������۳�����
        /// </summary>
        /// <param name="id">����ID</param> 
        /// <param name="transaction">���ݿ��������</param>
        /// <returns>���ؽ����0(ɾ������)��>0(ɾ���ɹ�)</returns>
        int Delete(int id, SqlTransaction transaction);

        /// <summary>
        /// ���ݺ�ϻ�ӱ��ɾ����¼
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="transaction"></param>
        /// <param name="fieldname"></param>
        void DeleteByField<TElement>(string fieldname, T fieldValue, SqlTransaction transaction);

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlClause"></param>
        void Execute(string sqlClause);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string sqlClause, params SqlParameter[] commandParameters);
    }
}