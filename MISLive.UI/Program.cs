using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Taskbar;
using DevExpress.XtraEditors;
using MISLive.UI.Forms.Main;
using MISLive.UI.Properties;

namespace MISLive.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            switch (SystemInformation.TerminalServerSession)
            {
                case false when Screen.AllScreens.Length > 1:
                    WindowsFormsSettings.SetPerMonitorDpiAware();
                    break;
                default:
                    WindowsFormsSettings.SetDPIAware();
                    break;
            }

            // check if another instance is running
            string appGuid =
                ((GuidAttribute)
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value;

            Mutex mutex = new Mutex(true, $"{{{appGuid}}}");

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();

                TaskbarAssistant.Default.Initialize();
                //HelperApplication.InitDefaultStyle();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Application.Run(new MainForms());
            }
            else
            {
                XtraMessageBox.Show("Another process is already running." + Environment.NewLine + "Exiting...",
                    "MultiProcesses off.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }



        }
    }
}
