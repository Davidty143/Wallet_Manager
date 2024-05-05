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


        public Login()
        {
            InitializeComponent();

        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }


        private void t_login_button_Click_1(object sender, EventArgs e)
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
                this.Hide();
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtPassword.Text = "";
            }

            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }


        private void cb_show_pass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cb_show_pass.Checked ? '\0' : '*';
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Signup newSignup = new Signup();
            newSignup.ShowDialog();
            this.Hide();
        }

    }
}
