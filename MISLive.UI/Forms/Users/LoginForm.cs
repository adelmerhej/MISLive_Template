using DevExpress.XtraEditors;
using MISLive.UI.Models.Users;
using MISLive.UI.Repositories;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using System.Windows.Forms;
using System.Xml.Linq;
using MISLive.UI.Properties;

namespace MISLive.UI.Forms.Users
{
    public partial class LoginForm : XtraForm
    {
        private UserModel _userModel = new UserModel();

        private UserRepository _userRepository = new UserRepository();

        public LoginForm()
        {
            InitializeComponent();

            InitData();
            WireUpData();
        }

        private void InitData()
        {
            _userModel = _userRepository.SelectUser(Settings.Default.UserId);

        }
        private void WireUpData()
        {
            txtUserName.Text = _userModel.UserName;
            txtPassword.Text = _userModel.Password;
            chkSavePassword.CheckState = _userModel.Active ? CheckState.Checked : CheckState.Unchecked;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.No;
            }
        }
    }
}