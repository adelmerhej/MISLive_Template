using MISLive.UI.DatabaseLayer.DataAccess.Types;
using MISLive.UI.Models.Databases;
using MISLive.UI.Properties;
using MISLive.UI.Utilities;
using System;

namespace MISLive.UI.DatabaseLayer.DataAccess
{
    public class DatabaseFactory
    {
        public static IDatabase Get(string application, string databaseType, string databaseHost, int databasePort,
            string databaseName, string databaseUser, string databasePassword)
        {
            switch ((DatabaseTypes)Enum.Parse(typeof(DatabaseTypes), databaseType))
            {
                case DatabaseTypes.MySQL:
                    return new MySQLDatabaseModel()
                    {
                        Server = databaseHost,
                        Port = databasePort,
                        Database = databaseName,
                        User = databaseUser,
                        Password = databasePassword
                    };
                case DatabaseTypes.SQLite:
                    return (IDatabase)new SqlLiteDatabaseModel()
                    {
                        Application = application
                    };
                case DatabaseTypes.SQLServer:
                    return new SqlServerDatabaseModel()
                    {
                        Server = databaseHost,
                        Database = databaseName,
                        User = databaseUser,
                        Password = databasePassword
                    };

                default:
                    return null;
            }
        }

        public static void ConnectionParamsSet(ConnectionModel connection)
        {
            Settings.Default.DatabaseType = connection.DatabaseType;
            Settings.Default.DatabaseHost = connection.DatabaseHost;
            Settings.Default.DatabasePort = connection.DatabasePort;
            Settings.Default.DatabaseName = connection.DatabaseName;
            Settings.Default.DatabaseUser = connection.DatabaseUser;
            Settings.Default.DatabasePassword = connection.DatabasePassword;
            Settings.Default.Save();
        }

        public static ConnectionModel ConnectionParamsGet()
        {
            ConnectionModel connection = new ConnectionModel();

            connection.DatabaseType = Settings.Default.DatabaseType;
            connection.DatabaseHost = Settings.Default.DatabaseHost;
            connection.DatabasePort = Settings.Default.DatabasePort;
            connection.DatabaseName = Settings.Default.DatabaseName;
            connection.DatabaseUser = Settings.Default.DatabaseUser;
            connection.DatabasePassword = Settings.Default.DatabasePassword;

            return connection;
        }
    }
}
