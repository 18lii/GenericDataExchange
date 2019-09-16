using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Helper
{
    public class ConnectionTest
    {
        public bool Test(string connectionString)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    conn.Close();
                    conn.Dispose();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
