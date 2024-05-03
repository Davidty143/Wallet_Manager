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

            // Initialize forms
            loginForm = new Login();
            dashboardForm = new Dashboard();
            signupForm = new Signup();


            // Check login status and show the appropriate form
            ShowAppropriateForm();

            // Start the application with the login form as the default form
            Application.Run();
        }

        private static void ShowAppropriateForm()
        {
            if (Properties.Settings.Default.IsLoggedIn)
            {
                GlobalData.SetUserID(Properties.Settings.Default.LastUserID);
                GlobalEvents.OnTransactionUpdated();
                Application.Run(dashboardForm);
                
            }
            else
            {
                GlobalEvents.OnTransactionUpdated();
                Application.Run(loginForm);
                
            }
        }

        public static void ShowDashboard()
        {
            dashboardForm.clickDashboard();
            dashboardForm.Visible = true;
            dashboardForm.Activate();  // Bring the form to the front if it's already open
        }
        public static void ShowLoginForm()
        {
            loginForm.Visible = true;
            loginForm.Activate();  // Bring the form to the front if it's already open
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

}
