﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace MISLive.UI.DatabaseLayer.DataAccess.Types
{
    public class SqlServerDatabaseModel : AbstractDatabaseModel
    {
        public static int DefaultPort = 1433;

        private DateTime _datetimeBackupInit;
        private DateTime _datetimeImportInit;

        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public SqlServerDatabaseModel()
        {
        }
        public SqlServerDatabaseModel(string server, int port, string database, string user, string password)
        {
            Server = server;
            Port = port;
            Database = database;
            User = user;
            Password = password;
        }

        public override ConnectionState CheckConnection()
        {
            string connectionString = BuildConnectionString(false);
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
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

        internal string BuildConnectionString(bool database = true)
        {
            string connectionString = "Server=" + (Port == 0 || Port == DefaultPort ? Server : Server + "," + Port.ToString()) + ";User Id=" + User + ";Password=" + Password;
            if (database)
                connectionString = connectionString + ";Database=" + Database;
            return connectionString;
        }

        #region Overrides of AbstractDatabaseModel

        public override bool CheckDatabaseExists(string databaseName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
