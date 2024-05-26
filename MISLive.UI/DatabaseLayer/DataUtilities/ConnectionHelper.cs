using MISLive.UI.DatabaseLayer.DataAccess;
using MISLive.UI.Models.Databases;

namespace MISLive.UI.DatabaseLayer.DataUtilities
{
    internal static class ConnectionHelper
    {
        public static string BuildConnectionString()
        {
            ConnectionModel connectionModel = new ConnectionModel();
            connectionModel = DatabaseFactory.ConnectionParamsGet();

            string connectionString = string.Concat("Server=", connectionModel.DatabaseHost, ";Database=", connectionModel.DatabaseName, ";" +
                "User Id=", connectionModel.DatabaseUser, ";Password=", connectionModel.DatabasePassword);

            return connectionString;
        }
    }
}
