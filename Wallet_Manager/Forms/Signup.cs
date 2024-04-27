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

        public Signup()
        {
            InitializeComponent();
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            BusinessLogic _businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));
        }

        private void t_login_button_Click(object sender, EventArgs e)   
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_show_pass_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (!IsGmailAddress(email))
            {
                MessageBox.Show("Please enter a valid Gmail address.");
                txtEmail.Text = "";
                return; // Exit the method to prevent further execution
            }

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

        private bool IsGmailAddress(string email)
        {
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }

        private void cb_show_pass_CheckedChanged_1(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cb_show_pass.Checked ? '\0' : '*';
        }
    }
}
