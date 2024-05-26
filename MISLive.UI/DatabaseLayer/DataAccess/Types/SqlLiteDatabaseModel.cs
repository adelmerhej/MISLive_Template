using System;

namespace MISLive.UI.DatabaseLayer.DataAccess.Types
{
    public class SqlLiteDatabaseModel : AbstractDatabaseModel
    {
        public string Application { get; set; }


        #region SQLLite actions

        public void Import(string path) => throw new NotImplementedException();

        public void Export(string path) => throw new NotImplementedException();

        public string GetBackupExtension() => ".db";

        public void DeleteDatabase(string databaseName) => throw new NotImplementedException();

        #endregion


        #region Overrides of AbstractDatabaseModel

        public override bool CheckDatabaseExists(string databaseName) => true;


        #endregion
    }
}
