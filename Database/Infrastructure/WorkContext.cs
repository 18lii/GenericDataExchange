using Core.Entities;
using Core.Interface;
using Dapper;
using Database.Interface;
using DatabaseFactory.Interface;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
    internal class WorkContext : BaseContext, IWorkContext
    {
        public WorkContext(ISqlServerDatabaseUtil dbUtil, ISqlAdapterDatabaseUtil apUtil) : base(dbUtil, apUtil) { }
        /// <summary>
        /// 数据库上下文操作方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IGenericResult Activing(IGenericEventArg<IFactoryContext> e)
        {
            var context = e.Item;
            var id = e.Id;
            var userId = e.UserId;
            var result = default(IGenericResult);
            SqlTransaction.TryAdd(id, SetConnection(userId).BeginTransaction());
            if(context.PolicyType == PolicyType.Command)
            {
                if (context.Params != null)
                {
                    Parallel.ForEach(context.Params, p =>
                    {
                        var dyParam = new DynamicParameters();
                        foreach (var item in p)
                        {
                            dyParam.Add(item.Key, item.Value);
                        }
                        result = Accept(SqlConnection[userId], context.DbOperate, context.SqlText[0], dyParam, SqlTransaction[id]);
                    });
                }
                else
                {
                    result = Accept(SqlConnection[userId], context.DbOperate, context.SqlText[0], new DynamicParameters(), SqlTransaction[id]);
                }

                var sqlLog = new StringBuilder();
                if (result.ResultType == 0)
                {
                    result.LogMessage = sqlLog.ToString();
                    var cr = DbCommit(userId, id);
                    if (cr.ResultType != 0)
                    {
                        result = cr;
                    }
                }
                else
                {
                    result = DbRollback(userId, id);
                }
            }
            else
            {
                for(var i= 0; i < context.SqlText.Length; i++)
                {
                    var command = new SqlCommand(context.SqlText[i], SqlConnection[userId], SqlTransaction[id]);
                    command.CommandTimeout = 60;
                    switch (context.AptOperate)
                    {
                        case AdapterOperate.Get:
                            result = Accept(context.AptOperate, command);
                            break;
                        case AdapterOperate.Set:
                            result = Accept(context.AptOperate, command, context.DataSet[i]);
                            break;
                    }
                    command.Dispose();
                }
                var commitResult = DbCommit(userId, id);
                if (commitResult.ResultType != 0)
                {
                    result = commitResult;
                };
            }
            return result;
        }
    }
}
