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
        public Login()
        {
            InitializeComponent();
        }

        private void t_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void t_login_button_Click(object sender, EventArgs e)
        {

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

            BusinessLogic businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));

            if (businessLogic.LoginUser(email, password))
            {
                MessageBox.Show("Login successful.");
                // Navigate to main form...
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
    }
}
