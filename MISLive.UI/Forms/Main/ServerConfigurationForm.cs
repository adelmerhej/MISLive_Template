using DevExpress.XtraEditors;
using MISLive.UI.DatabaseLayer.DataAccess;
using MISLive.UI.Models.Databases;
using MISLive.UI.Properties;
using MISLive.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace MISLive.UI.Forms.Main
{
    public partial class ServerConfigurationForm : DevExpress.XtraEditors.XtraForm
    {
        ConnectionModel _connectionModel = new ConnectionModel();


        public ServerConfigurationForm()
        {
            InitializeComponent();

            ConnectionParams();
            InitializeBindings();
            WireUpBindings();
        }

        private void ConnectionParams()
        {
            _connectionModel = DatabaseFactory.ConnectionParamsGet();
        }

        private void InitializeBindings()
        {
            List<InitDataObjectModel> InitDataObjectModelList1 = new List<InitDataObjectModel>();
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Fatal",
                Value = LogLevel.Critical.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Error",
                Value = LogLevel.Error.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Warning",
                Value = LogLevel.Warning.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Info",
                Value = LogLevel.Information.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Debug",
                Value = LogLevel.Debug.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Trace",
                Value = LogLevel.Trace.ToString()
            });
            InitDataObjectModelList1.Add(new InitDataObjectModel()
            {
                Name = "Off",
                Value = LogLevel.None.ToString()
            });
            cboLevelFile.Properties.DataSource = InitDataObjectModelList1;
            cboLevelFile.Properties.DisplayMember = "Name";
            cboLevelFile.Properties.ValueMember = "Value";
            //cboLevelFile.EditValue = Settings.Default.LevelAppLog;

            cboLevelNHibernate.Properties.DataSource = InitDataObjectModelList1;
            cboLevelNHibernate.Properties.DisplayMember = "Name";
            cboLevelNHibernate.Properties.ValueMember = "Value";
            //cboLevelNHibernate.EditValue = Settings.Default.LevelNHibernateLog;

            //Get Database Type info
            List<InitDataObjectModel> InitDataObjectModelList2 = new List<InitDataObjectModel>();
            InitDataObjectModelList2.Add(new InitDataObjectModel()
            {
                Name = "MySQL",
                Value = DatabaseTypes.MySQL
            });
            InitDataObjectModelList2.Add(new InitDataObjectModel()
            {
                Name = "SQLServer",
                Value = DatabaseTypes.SQLServer
            });
            InitDataObjectModelList2.Add(new InitDataObjectModel()
            {
                Name = "SqlLite",
                Value = DatabaseTypes.SQLite
            });
            cboDatabaseType.Properties.DisplayMember = "Name";
            cboDatabaseType.Properties.ValueMember = "Name";
            cboDatabaseType.Properties.DataSource = InitDataObjectModelList2;
            //cboDatabaseType.EditValue = Settings.Default.DatabaseType;
        }
        private void WireUpBindings()
        {
            cboDatabaseType.EditValue = _connectionModel.DatabaseType;
            txtDatabaseHost.Text = _connectionModel.DatabaseHost;
            txtDatabasePort.Text = _connectionModel.DatabasePort.ToString();
            txtDatabaseName.Text = _connectionModel.DatabaseName;
            txtUserName.Text = _connectionModel.DatabaseUser;
            txtPassword.Text = _connectionModel.DatabasePassword;
        }

        private void btnLogConfiguration_Click(object sender, System.EventArgs e)
        {
            if (!infoPanel.Visible)
            {
                databasePanel.Visible = false;
                infoPanel.Visible = true;
                btnLogConfiguration.Image = Resources.left_arrow_16_light;
            }
            else
            {
                infoPanel.Visible = false;
                databasePanel.Visible = true;
                btnLogConfiguration.Image = Resources.file_16_light;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                _connectionModel.DatabaseType = (string)cboDatabaseType.EditValue;
                _connectionModel.DatabaseHost = txtDatabaseHost.Text.Trim();
                int.TryParse(txtDatabasePort.Text, out var result);
                _connectionModel.DatabasePort = result;
                _connectionModel.DatabaseName = txtDatabaseName.Text.Trim();
                _connectionModel.DatabaseUser = txtUserName.Text;
                _connectionModel.DatabasePassword = txtPassword.Text;

                _connectionModel.LevelAppLog = (string)cboLevelFile.EditValue;
                _connectionModel.LevelSqlLog = (string)cboLevelNHibernate.EditValue;

                DatabaseFactory.ConnectionParamsSet(_connectionModel);

                XtraMessageBox.Show("Please restart your application to apply the new settings.",
                    "DB Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                DialogResult = DialogResult.No;
            }
            Close();
        }
    }
}