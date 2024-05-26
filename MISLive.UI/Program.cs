using DevExpress.Utils.Taskbar;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MISLive.UI.Forms.Main;
using MISLive.UI.Forms.Users;
using MISLive.UI.Utilities;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

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
                if (LoadingProcess())
                {
                    using (LoginForm usr = new LoginForm())
                    {
                        if (usr.ShowDialog() == DialogResult.No)
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    Application.Run(new MainForms());
                }
            }
            else
            {
                XtraMessageBox.Show("Another process is already running." + Environment.NewLine + "Exiting...",
                    "MultiProcesses off.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

        }

        private static bool LoadingProcess()
        {
            int varSleep = 0;
            try
            {
                // Show a splashScreen.
                FluentSplashScreenOptions op = new FluentSplashScreenOptions();
                op.Title = "When Only The Best Will Do";
                op.Subtitle = "Exceptional Operations Logistics SAL";
                op.RightFooter = "Starting...";
                op.LeftFooter = "Copyright © " + DateTime.Now.Year.ToString() + " XOLOG" +
                                Environment.NewLine + "All Rights reserved.";
                op.LoadingIndicatorType = FluentLoadingIndicatorType.Dots;
                op.OpacityColor = Color.FromArgb(16, 110, 190);
                op.Opacity = 130;

                SplashScreenManager.ShowFluentSplashScreen(op, useFadeIn: true, useFadeOut: true);

                //Check Database structure and Database connection
                op.RightFooter = "Check Database Structure...";
                SplashScreenManager.Default.SendCommand(FluentSplashScreenCommand.UpdateOptions, op);
                HelperApplication.CheckDatabaseConnection();
                Thread.Sleep(varSleep);

                //Apply default configuration
                op.RightFooter = "Apply default settings...";
                SplashScreenManager.Default.SendCommand(FluentSplashScreenCommand.UpdateOptions, op);
                //HelperApplication.ApplyDefaultSettings();
                Thread.Sleep(varSleep);

                //Apply default configuration
                op.RightFooter = "Load default settings...";
                SplashScreenManager.Default.SendCommand(FluentSplashScreenCommand.UpdateOptions, op);
                //HelperApplication.LoadDefaultSettings();
                Thread.Sleep(varSleep);

                //Check Database structure and Database connection
                op.RightFooter = "Finishing...";
                SplashScreenManager.Default.SendCommand(FluentSplashScreenCommand.UpdateOptions, op);
                //InitializeServices();
                Thread.Sleep(varSleep);

            }
            catch (Exception e)
            {
                MessageBox.Show(@"Initialize Data Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NotificationException(e);
                Application.Exit();
            }
            finally
            {
                //_logger.Info("Application started.");
                //Close the splashScreen
                SplashScreenManager.CloseForm();

            }
            return true;
        }

        private static void NotificationException(Exception e)
        {
            (new Thread(() =>
            {
                try
                {
                    //_logger.Error(string.Concat("Program Main Error: ", e.Message, 
                    //    Environment.NewLine, "StackTrace: ", e.StackTrace));
                }
                catch (Exception)
                {
                    // ignored
                }
            })).Start();
        }
    }
}
