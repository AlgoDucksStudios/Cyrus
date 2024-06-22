using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBUtils.Provider;

namespace Cyrus.DB
{
    public class MYSQL : Database
    {
        public override IDbConnection Connect(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = env("CONNECTION_STRING");
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = BuildConnectionString(env("DB_HOST", "127.0.0.1"), env("DB_DATABASE"), env("DB_USERNAME", "root"), env("DB_PASSWORD", ""));
                    if (string.IsNullOrEmpty(connectionString))
                        throw new Exception("[X] Enter the Connection String");
                }
            }
            else
                ConnectionString = connectionString;

            instance = new SqlConnection(ConnectionString);
            return instance;
        }

        protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }
    }
}
