using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Interface
{
    public interface ISqlAdapterAccessor
    {
        Tuple<bool, object> Get(SqlCommand command);
        Tuple<bool, object> Set(SqlCommand command, DataSet dataSet);
    }
}
