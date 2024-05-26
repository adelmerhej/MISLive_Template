using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MISLive.UI.DatabaseLayer.DataAccess;
using MISLive.UI.Models.Databases;
using System.Data;
using MISLive.UI.Forms.Main;

namespace MISLive.UI.Utilities
{
    public static class HelperApplication
    {
        private static bool _changeDatabase = false;

        public static void CheckDatabaseConnection()
        {
            try
            {
                ConnectionModel connectionModel = new ConnectionModel();

                connectionModel = DatabaseFactory.ConnectionParamsGet();

                IDatabase database = DatabaseFactory.Get(connectionModel.DatabaseName, connectionModel.DatabaseType,
                    connectionModel.DatabaseHost, connectionModel.DatabasePort, connectionModel.DatabaseName,
                    connectionModel.DatabaseUser, connectionModel.DatabasePassword);

                if (database.CheckConnection() == ConnectionState.Open)
                {
                    if (database.AllowCreateDataBase() && !database.CheckDatabaseExists(connectionModel.DatabaseName))
                    {
                        if (XtraMessageBox.Show(@"The database " + connectionModel.DatabaseName + @" could not be found. Do you want to create it?",
                                $@"Create database {connectionModel.DatabaseName}", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //If yes then we can create the database, set default values and login to the app
                            database.CreateDatabase(connectionModel.DatabaseName);
                        }
                        else
                        {
                            //If user choose No to create database, exit the app
                            System.Windows.Forms.Application.Exit();
                        }
                    }

                    if (_changeDatabase)
                    {
                        ServerConfigurationForm dbForm = new ServerConfigurationForm();
                        if (dbForm.ShowDialog() == DialogResult.OK)
                        {
                            CheckDatabaseConnection();
                            System.Windows.Forms.Application.Exit();
                        }
                    }
                    //if create success, configure schema tables and apply default configuration
                    //ISchemaDatabase schemaDatabase = SchemaDatabaseFactory.Get(database);
                    //schemaDatabase.Configure();
                    //schemaDatabase.Upgrade();
                }
                else
                {
                    // if connection error for any reason exit the app
                    //TODO: later, we have to catch the error return from connection check to define what type of error we got
                    XtraMessageBox.Show(@"Initialize Data Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ServerConfigurationForm dbForm = new ServerConfigurationForm();
                    if (dbForm.ShowDialog() == DialogResult.OK)
                    {
                        CheckDatabaseConnection();
                        return;
                    }
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, $@"Error {e.HResult.ToString()}", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                ServerConfigurationForm dbForm = new ServerConfigurationForm();
                if (dbForm.ShowDialog() == DialogResult.OK)
                {
                    CheckDatabaseConnection();
                    return;
                }
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
