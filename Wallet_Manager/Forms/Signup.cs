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
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public Signup()
        {
            InitializeComponent();
        }


        private void Signup_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
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
                return; 
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Please enter at least 8 characters, at least one letter.");
                txtPassword.Text = "";
                return; 
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill out all the fields.");
                return;
            }

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            bool signupSuccess = dataAccessLayer.CreateAccount(firstName, lastName, email, password);

            if (signupSuccess)
            {
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";

                MessageBox.Show("Account created successfully. You can now log in.");

                Login newlogin = new Login();
                newlogin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while creating your account. Account already exists");
            }
        }


        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool IsValidPassword(string password)
        {
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
    }
}
