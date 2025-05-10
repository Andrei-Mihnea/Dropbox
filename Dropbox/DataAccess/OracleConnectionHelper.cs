using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public static class OracleConnectionHelper
    {
        private const string ConnectionString = "User Id=dropbox;Password=ip2025;Data Source=localhost:1521/XEPDB1;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(ConnectionString);
        }
    }
}
