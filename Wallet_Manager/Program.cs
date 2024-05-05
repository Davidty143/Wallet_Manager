using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;
using Wallet_Manager.Forms;

namespace Wallet_Manager
{

    internal static class Program
    {
        public static Login loginForm;
        public static Dashboard dashboardForm;
        public static Signup signupForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetProcessDPIAware();

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            dataAccessLayer.UpdateBudgetStatuses();

            loginForm = new Login();
            dashboardForm = new Dashboard();
            signupForm = new Signup();


            ShowAppropriateForm();

            Application.Run();
        }

        private static void ShowAppropriateForm()
        {
            if (Properties.Settings.Default.IsLoggedIn)
            {
                GlobalData.SetUserID(Properties.Settings.Default.LastUserID);
                GlobalEvents.OnTransactionUpdated();
                GlobalEvents.OnProfileInformationUpdated();
                Application.Run(dashboardForm);
                
            }
            else
            {
                GlobalEvents.OnTransactionUpdated();
                GlobalEvents.OnProfileInformationUpdated();
                Application.Run(loginForm);
                
            }
        }

        public static void ShowDashboard()
        {
            dashboardForm.clickDashboard();
            dashboardForm.Visible = true;
            dashboardForm.Activate();
        }
        public static void ShowLoginForm()
        {
            loginForm.Visible = true;
            loginForm.Activate();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

}
