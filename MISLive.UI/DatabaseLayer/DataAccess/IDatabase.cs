using System.Data;

namespace MISLive.UI.DatabaseLayer.DataAccess
{
    public interface IDatabase
    {
        bool AllowCreateDataBase();
        ConnectionState CheckConnection();
        bool CheckDatabaseExists(string databaseName);
        void CreateDatabase(string databaseName);
    }
}
