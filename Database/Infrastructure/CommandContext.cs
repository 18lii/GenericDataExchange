using Dapper;
using Database.Helper;
using Database.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
    internal class CommandContext : BaseContext, ICommandContext
    {
        public CommandContext(ISqlCommandDatabaseUtil dbUtil) : base(dbUtil) { }
        /// <summary>
        /// 数据库上下文操作方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Tuple<bool, object> Activing(Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>> context)
        {
            var operate = context.Item1;
            var parameters = context.Item2;
            var result = default(Tuple<bool, object>);
            if (parameters != null)
            {
                Parallel.ForEach(parameters, kv =>
                {
                    var dyParam = new DynamicParameters();
                    if (kv.Value != null)
                    {
                        foreach (DictionaryEntry item in kv.Value)
                        {
                            dyParam.Add(item.Key.ToString(), item.Value);
                        }
                        result = Accept(Connection, operate, kv.Key, dyParam, Transaction);
                    }
                    else
                    {
                        result = Accept(Connection, operate, kv.Key, dyParam, Transaction);
                    }
                });
            }
            if (result.Item1)
            {
                var commitResult = DbCommit();
                if (!commitResult.Item1)
                {
                    result = commitResult;
                }
            }
            else
            {
                DbRollback();
            }
            return result;
        }
    }
}
