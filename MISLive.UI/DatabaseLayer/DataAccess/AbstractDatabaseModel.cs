using System.Data;

namespace MISLive.UI.DatabaseLayer.DataAccess
{
    public abstract class AbstractDatabaseModel : IDatabase
    {
        #region Implementation of IDatabase

        public virtual bool AllowCreateDataBase() => false;

        public virtual ConnectionState CheckConnection()
        {
            ConnectionState state = ConnectionState.Closed;
            return state;
        }

        public abstract bool CheckDatabaseExists(string databaseName);

        public virtual void CreateDatabase(string databaseName)
        {

        }

        #endregion
    }
}
