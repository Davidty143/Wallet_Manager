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


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            dataAccessLayer.UpdateBudgetStatuses(); // to update the active and inactive budgets

            if (Properties.Settings.Default.IsLoggedIn)
            {
                ShowDashboard();
            }
            else
            {
                ShowLoginForm();
            }

            //Application.Run(new Login());
        }

        static void ShowDashboard()
        {
            Application.Run(new Dashboard());
        }

        public static void ShowLoginForm()
        {
            Application.Run(new Login());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
