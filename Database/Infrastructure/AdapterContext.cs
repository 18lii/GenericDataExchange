using Database.Helper;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Infrastructure
{
    internal class AdapterContext : BaseContext, IAdapterContext
    {
        public AdapterContext(ISqlAdapterDatabaseUtil dbUtil) : base(dbUtil) { }
        public Tuple<bool, object> Activing(Tuple<AptOperate, string[], DataSet[]> context)
        {
            var operate = context.Item1;
            var sqlTexts = context.Item2;
            var dataSets = context.Item3;
            var result = default(Tuple<bool, object>);
            for (var i = 0; i < sqlTexts.Length; i++)
            {
                var command = new SqlCommand(sqlTexts[i], Connection, Transaction);
                command.CommandTimeout = 60;
                switch (operate)
                {
                    case AptOperate.Get:
                        result = Accept(operate, command);
                        break;
                    case AptOperate.Set:
                        result = Accept(operate, command, dataSets[i]);
                        break;
                }
                command.Dispose();
            }
            var commitResult = DbCommit();
            if (!commitResult.Item1)
            {
                result = commitResult;
            };
            return new Tuple<bool, object>(result.Item1, result.Item2);
        }
    }
}
