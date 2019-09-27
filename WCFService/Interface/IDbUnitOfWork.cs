using System;
using System.Collections;
using System.Data;

namespace WCFService.Interface
{
    public interface IDbUnitOfWork
    {
        /// <summary>
        /// 数据库操作返回，同步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object Result(Guid id);
        /// <summary>
        /// 数据库操作返回，以GUID进行获取，可等待
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void ResultAsync(Guid id, AsyncCallback callback);
        /// <summary>
        /// 通过sql文及参数获取数据库记录
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> Select(string sqlText, Hashtable param);
        /// <summary>
        /// 通过定序器获取数据库记录, 参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        Guid SequentialSelect(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 通过sql文及参数向数据库插入记录
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> Insert(string[] sqls, Hashtable[] param);
        /// <summary>
        /// 通过定序器向数据库插入记录，参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialInsert(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 通过sql文及参数更新数据库记录
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> Update(string[] sqls, Hashtable[] param);
        /// <summary>
        /// 通过定序器向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialUpdate(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 通过sql文及参数从数据库删除记录
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> Delete(string[] sqls, Hashtable[] param);
        /// <summary>
        /// 通过定序器删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialDelete(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 通过sql文及参数对数据库进行查询结果集的第一行第一列的值
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> ExecuteScalar(string sqlText, Hashtable param);
        /// <summary>
        /// 通过定序器进行数据库查询，返回值为查询结果第一行第一列的值
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialExecuteScalar(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 通过sql文及参数获取数据库中的记录
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> ExecuteReader(string sqlText, Hashtable param);
        /// <summary>
        /// 通过定序器进行数据库查询，返回值为结果集，类型为<see cref="DataTable"/>[]
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialExecuteReader(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 通过sql文及参数进行数据库操作
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> ExecuteNoQuery(string[] sqls, Hashtable[] param);
        /// <summary>
        /// 通过定序器进行数据库操作，返回值为受影响的记录数
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialExecuteNoQuery(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 通过sql文及参数执行数据库存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<bool, object> ExecuteProcedure(string procedureName, Hashtable param);
        /// <summary>
        /// 通过定序器进行数据库存储过程调用
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialExecuteProcedure(string procedureName, Hashtable param, bool sequence);
        /// <summary>
        /// 使用数据适配器对数据库进行查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        Tuple<bool, object> Get(string sqlText);
        /// <summary>
        /// 通过定序器使用数据适配器对数据进行查询，参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialGet(string sqlText, bool sequence);
        /// <summary>
        /// 使用数据适配器对数据库进行更改（插入，更新，删除）
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        Tuple<bool, object> Set(string[] sqlText, DataSet[] dataSet);
        /// <summary>
        /// 通过定序器使用数据适配器对数据库进行更改（插入，更新，删除），参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid SequentialSet(string[] sqls, DataSet[] dataSet, bool sequence);
    }
}
