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
    public partial class Signup : Form
    {
        private BusinessLogic _businessLogic;

        public Signup()
        {
            InitializeComponent();
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            _businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));
        }

        private void t_login_button_Click(object sender, EventArgs e)   
        {

                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                // Perform input validation
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please fill out all the fields.");
                    return;
                }

                // Call a method in your business logic to create the account
                bool signupSuccess = _businessLogic.CreateAccount(firstName, lastName, email, password);

                if (signupSuccess)
                {
                    MessageBox.Show("Account created successfully. You can now log in.");
                    // Optionally, navigate to the login form
                }
                else
                {
                    MessageBox.Show("An error occurred while creating your account. Account already exists");
                }
        }
    }
}
