using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;
using System.Threading.Tasks;

namespace DatabaseUtil
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 数据库操作返回，以GUID进行获取，可等待
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<object> Result(Guid id);
        /// <summary>
        /// 数据库记录获取, 参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        Guid Get(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 向数据库插入记录，参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid Insert(string[] sqls, Hashtable[] param, bool sequence);

        /// <summary>
        /// 向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid Update(string[] sqls, Hashtable[] param, bool sequence);

        /// <summary>
        /// 删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid Delete(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 数据库查询，返回值为查询结果第一行第一列的值
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid ExecuteScalar(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 数据库查询，返回值为结果集，类型为<see cref="DataTable"/>[]
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid ExecuteReader(string sqlText, Hashtable param, bool sequence);
        /// <summary>
        /// 数据库操作，返回值为受影响的记录数
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid ExecuteNoQuery(string[] sqls, Hashtable[] param, bool sequence);
        /// <summary>
        /// 数据库存储过程调用
        /// <para>参数 <see cref="bool"/> sequence 表示是否顺序执行</para>
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid ExecuteProcedure(string procedureName, Hashtable param, bool sequence);
        /// <summary>
        /// 数据适配器查询，参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid Get(string sqlText, bool sequence);
        /// <summary>
        /// 数据适配器更改（插入，更新，删除），参数 <see cref="bool"/> sequence 表示是否顺序执行
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Guid Set(string[] sqls, DataSet[] dataSet, bool sequence);
    }
}
