using Sequencer.Interface;
using System;
using System.Collections;
using System.Data;
using TransparentAgent.Interface;
using WCFService.DbUnitOfWork;

namespace WCFService.Interface
{
    public interface IDbUnitOfWork
    {
        ISequencerEntry SequencerEntry { get; set; }
        event Action<object> ResultEvent;
        /// <summary>
        /// 通过sql文及参数获取数据库记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Select(byte[] data);

        /// <summary>
        /// 通过sql文及参数向数据库插入记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Insert(byte[] data);

        /// <summary>
        /// 通过sql文及参数更新数据库记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Update(byte[] data);

        /// <summary>
        /// 通过sql文及参数从数据库删除记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Delete(byte[] data);

        /// <summary>
        /// 通过sql文及参数对数据库进行查询结果集的第一行第一列的值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] ExecuteScalar(byte[] data);

        /// <summary>
        /// 通过sql文及参数获取数据库中的记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] ExecuteReader(byte[] data);

        /// <summary>
        /// 通过sql文及参数进行数据库操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] ExecuteNoQuery(byte[] data);

        /// <summary>
        /// 通过sql文及参数执行数据库存储过程
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] ExecuteProcedure(byte[] data);

        /// <summary>
        /// 使用数据适配器对数据库进行查询
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Get(byte[] data);

        /// <summary>
        /// 使用数据适配器对数据库进行更改（插入，更新，删除）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Set(byte[] data);
        
        ///// <summary>
        ///// 通过定序器获取数据库记录
        ///// </summary>
        ///// <param name="data"></param>
        //byte[] SequentialSelect(byte[] data);
        
        ///// <summary>
        ///// 通过定序器向数据库插入记录
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialInsert(byte[] data);
        
        ///// <summary>
        ///// 通过定序器向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialUpdate(byte[] data);
        
        ///// <summary>
        ///// 通过定序器删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialDelete(byte[] data);
        
        ///// <summary>
        ///// 通过定序器进行数据库查询，返回值为查询结果第一行第一列的值
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialExecuteScalar(byte[] data);
        
        ///// <summary>
        ///// 通过定序器进行数据库查询，返回值为结果集
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialExecuteReader(byte[] data);
        
        ///// <summary>
        ///// 通过定序器进行数据库操作，返回值为受影响的记录数
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialExecuteNoQuery(byte[] data);
        
        ///// <summary>
        ///// 通过定序器进行数据库存储过程调用
        ///// <para>参数 <see cref="bool"/>
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialExecuteProcedure(byte[] data);
        
        ///// <summary>
        ///// 通过定序器使用数据适配器对数据进行查询
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialGet(byte[] data);
        
        ///// <summary>
        ///// 通过定序器使用数据适配器对数据库进行更改（插入，更新，删除）
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] SequentialSet(byte[] data);
        
        /// <summary>
        /// 通过定序器获取数据库记录
        /// </summary>
        /// <param name="data"></param>
        void SequentialSelectAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器向数据库插入记录
        /// </summary>
        /// <param name="data"></param>
        void SequentialInsertAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器向数据库更新记录
        /// </summary>
        /// <param name="data"></param>
        void SequentialUpdateAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// </summary>
        /// <param name="data"></param>
        void SequentialDeleteAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器进行数据库查询，返回值为查询结果第一行第一列的值
        /// </summary>
        /// <param name="data"></param>
        void SequentialExecuteScalarAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器进行数据库查询，返回值为结果集
        /// </summary>
        /// <param name="data"></param>
        void SequentialExecuteReaderAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器进行数据库操作，返回值为受影响的记录数
        /// </summary>
        /// <param name="data"></param>
        void SequentialExecuteNoQueryAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器进行数据库存储过程调用
        /// </summary>
        /// <param name="data"></param>
        void SequentialExecuteProcedureAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器使用数据适配器对数据进行查询
        /// </summary>
        /// <param name="data"></param>
        void SequentialGetAsync(byte[] data);
        
        /// <summary>
        /// 通过定序器使用数据适配器对数据库进行更改（插入，更新，删除）
        /// </summary>
        /// <param name="data"></param>
        void SequentialSetAsync(byte[] data);
    }
}
