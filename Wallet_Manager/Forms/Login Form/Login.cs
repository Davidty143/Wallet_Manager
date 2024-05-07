using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class Login : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        Timer closeTimer;



        public Login()
        {
            InitializeComponent();

        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }


        private void loginButton_Click_1(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.");
                return;
            }
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            if (dataAccessLayer.LoginUser(email, password))
            {
                Properties.Settings.Default.IsLoggedIn = true;
                Properties.Settings.Default.LastUserID = GlobalData.GetUserID();
                Properties.Settings.Default.Save();

                GlobalEvents.OnTransactionUpdated();
                GlobalEvents.OnProfileInformationUpdated();
                Program.ShowDashboard();

                closeTimer = new System.Windows.Forms.Timer();
                closeTimer.Interval = 1; // Set the interval to 5 seconds (5000 milliseconds)
                closeTimer.Tick += CloseTimer_Tick; // Add event handler for the Tick event
                closeTimer.Start(); // Start the timer

               
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtPassword.Text = "";
            }

            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer to prevent continuous closing
            closeTimer.Stop();

            // Close the form
            this.Hide();
        }

        private void showPassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = showPassCheckBox.Checked ? '\0' : '*';
        }

        private void signupLabel_Click(object sender, EventArgs e)
        {
            Signup newSignup = new Signup();
            newSignup.ShowDialog();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}




