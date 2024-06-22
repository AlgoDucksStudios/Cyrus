using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.DB
{
    public abstract class Database : IDisposable
    {
        protected string ConnectionString;
        protected IDbConnection instance;

        public abstract IDbConnection Connect(string connectionString = null);

        protected string BuildConnectionString(string server, string database, string username, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = database;
            builder.UserID = username;
            builder.Password = password;
            // Optionally, you can set additional properties here, like timeout, etc.
            // builder.ConnectTimeout = 30; // Example

            return builder.ConnectionString;

        }

        public int ExecCommand(string command, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var cmd = instance.CreateCommand())
                {
                    cmd.CommandText = command;
                    if (parameters != null)
                    {
                        foreach (var kvp in parameters)
                        {
                            var parameter = cmd.CreateParameter();
                            parameter.ParameterName = kvp.Key;
                            parameter.Value = kvp.Value;
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    instance.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (instance.State == ConnectionState.Open)
                    instance.Close();
            }
        }

        public DataSet ExecQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var cmd = instance.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (parameters != null)
                    {
                        foreach (var kvp in parameters)
                        {
                            var parameter = cmd.CreateParameter();
                            parameter.ParameterName = kvp.Key;
                            parameter.Value = kvp.Value;
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    var adapter = GetDataAdapter(cmd);
                    DataSet dataTable = new DataSet();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            finally
            {
                if (instance.State == ConnectionState.Open)
                    instance.Close();
            }
        }

        protected abstract IDbDataAdapter GetDataAdapter(IDbCommand command);

        public void Dispose()
        {
            instance.Close();
            GC.SuppressFinalize(this);
        }
    }
}
