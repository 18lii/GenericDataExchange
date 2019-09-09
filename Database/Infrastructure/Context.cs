using Core.Interface;
using Dapper;
using Database.Interface;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
    public class Context : BaseContext, IContext
    {
        public Context(string connectionString)
            : base(connectionString) { }
        
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
            if(context.Params != null)
            {
                Parallel.ForEach(context.Params, p =>
                {
                    var dyParam = new DynamicParameters();
                    foreach (var item in p)
                    {
                        dyParam.Add(item.Key, item.Value);
                    }
                    result = Accept(SqlConnection[userId], context.DbOperate, context.SqlText, dyParam, SqlTransaction[id]);
                });
            }
            else
            {
                result = Accept(SqlConnection[userId], context.DbOperate, context.SqlText, new DynamicParameters(), SqlTransaction[id]);
            }
            
            var sqlLog = new StringBuilder();
            if (result.ResultType == 0)
            {
                result.LogMessage = sqlLog.ToString();
                var cr = DbCommit(userId, id);
                if(cr.ResultType != 0)
                {
                    result = cr;
                }
            }
            else
            {
                result = DbRollback(userId, id);
            }
            return result;
        }
    }
}
