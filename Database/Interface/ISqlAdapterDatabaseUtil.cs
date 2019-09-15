using Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    public interface ISqlAdapterDatabaseUtil
    {
        Tuple<bool, object> Get(SqlCommand command);
        Tuple<bool, object> Set(SqlCommand command, DataSet dataSet);
    }
}
