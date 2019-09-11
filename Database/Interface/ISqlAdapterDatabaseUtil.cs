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
        IGenericResult Get(SqlCommand command);
        IGenericResult Set(SqlCommand command, DataSet dataSet);
    }
}
