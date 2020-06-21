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
        public CommandContext(ISqlCommandAccessor dbUtil) : base(dbUtil) { }
        /// <summary>
        /// 数据库上下文操作方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Tuple<bool, object> Activing(Tuple<int, ConcurrentDictionary<string, Hashtable>> context)
        {
            var operate = context.Item1;
            var parameters = context.Item2;
            var result = default(Tuple<bool, object>);
            SetConnection(out var id);
            if (parameters != null)
            {
                Parallel.ForEach(parameters, (kv, state) =>
                {
                    var dyParam = new DynamicParameters();
                    if (kv.Value != null)
                    {
                        foreach (DictionaryEntry item in kv.Value)
                        {
                            dyParam.Add(item.Key.ToString(), item.Value);
                        }
                        result = Accept(id, (CmdOperate)operate, kv.Key, dyParam);
                        
                    }
                    else
                    {
                        result = Accept(id, (CmdOperate)operate, kv.Key, dyParam);
                    }
                    if (!result.Item1)
                    {
                        state.Stop();
                    }
                });
                if (result.Item1)
                {
                    var commitResult = DbCommit(id);
                    if (!commitResult.Item1)
                    {
                        result = commitResult;
                    }
                }
                else
                {
                    DbRollback(id);
                }
                return result;
            }
            else
            {
                DbRollback(id);
                return new Tuple<bool, object>(false, "参数错误，没有要执行的SQL文");
            }
        }
    }
}
