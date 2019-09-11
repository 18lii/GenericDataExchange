using Core.Entities;
using Core.Helper;
using Core.Interface;
using Database.Entities;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Util
{
    internal class SqlAdapterDatabaseUtil : ISqlAdapterDatabaseUtil
    {
        public IGenericResult Get(SqlCommand command)
        {
            try
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    var tb = new DataTable();
                    adapter.Fill(tb);
                    return new GenericResultImpl(ResultType.Success, tb);
                }
            }
            catch(Exception e)
            {
                return new GenericResultImpl(ResultType.Error, new ExceptionMessage(e).ExMessage);
            }
            
        }
        public IGenericResult Set(SqlCommand command, DataSet dataSet)
        {
            try
            {
                using(var adapter = new SqlDataAdapter(command))
                {
                    return new GenericResultImpl(ResultType.Success, adapter.Update(dataSet));
                }
            }
            catch (Exception e)
            {
                return new GenericResultImpl(ResultType.Error, new ExceptionMessage(e).ExMessage);
            }
        }
    }
}
