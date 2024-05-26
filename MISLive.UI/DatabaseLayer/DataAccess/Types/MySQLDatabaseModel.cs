using MISLive.UI.DatabaseLayer.DataUtilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MISLive.UI.DatabaseLayer.DataAccess.Types
{
    public class MySQLDatabaseModel : AbstractDatabaseModel
    {
        private DateTime _datetimeBackupInit;
        private DateTime _datetimeImportInit;

        public static int DefaultPort = 3306;
        public string Database { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public string User { get; set; }


        public MySQLDatabaseModel()
        {
        }

        public MySQLDatabaseModel(string server, int port, string database, string user, string password)
        {
            Server = server;
            Port = port;
            Database = database;
            User = user;
            Password = password;
        }

        public override ConnectionState CheckConnection()
        {
            string connectionString = ConnectionHelper.BuildConnectionString();
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    return sqlConnection.State;
                }
            }
            catch (SqlException ex)
            {
                return ConnectionState.Closed;
            }
        }

        public override bool CheckDatabaseExists(string databaseName)
        {
            bool num;
            using (MySqlConnection mySqlConnection = new MySqlConnection(string.Concat(new object[]
                       {"server=", Server, ";PORT=", Port, ";user=", User, ";pwd=", Password, ";"})))
            {
                mySqlConnection.Open();
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    return false;
                }
                else
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText =
                            string.Concat(
                                "SELECT count(SCHEMA_NAME) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '",
                                databaseName,
                                "'");
                        mySqlCommand.Connection = mySqlConnection;
                        num = Convert.ToInt32(mySqlCommand.ExecuteScalar()) == 1;
                    }
                }
            }
            return num;
        }
        public override void CreateDatabase(string name)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(string.Concat(new object[]
                       {"server=", Server, ";PORT=", Port, ";user=", User, ";pwd=", Password, ";"})))
            {
                mySqlConnection.Open();
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText = string.Concat("CREATE SCHEMA IF NOT EXISTS ", name);
                        mySqlCommand.Connection = mySqlConnection;
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}
