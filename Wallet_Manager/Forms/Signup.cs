using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class Signup : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        private BusinessLogic _businessLogic;

        public Signup()
        {
            InitializeComponent();
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            _businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));
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

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid Gmail address.");
                txtEmail.Text = "";
                return; // Exit the method to prevent further execution
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Please enter at least 8 characters, at least one letter.");
                txtPassword.Text = "";
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


        public bool IsValidEmail(string email)
        {
            // Simple regex for email validation
            string emailPattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool IsValidPassword(string password)
        {
            // Regex to check if the password has at least 8 characters, at least one letter, and at least one number
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private void cb_show_pass_CheckedChanged_1(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cb_show_pass.Checked ? '\0' : '*';
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Login newlogin= new Login();
            newlogin.ShowDialog();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
