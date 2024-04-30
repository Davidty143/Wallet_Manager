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
    public partial class EditPassword : UserControl
    {
        public EditPassword()
        {
            InitializeComponent();
        }

        private void EditPassword_Load(object sender, EventArgs e)
        {

        }

        private void updatePass_Click(object sender, EventArgs e)
        {

            string currentPassword = currPass.Text;
            string newPassword = newPass.Text;
            string confirmPassword = confirmPass.Text;

            if(currentPassword == "" || newPassword == "" || confirmPassword == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if(newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }

            if(newPassword == currentPassword)
            {
                MessageBox.Show("New password must be different from current password.");
                return;
            }

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            if (!dataAccessLayer.ValidateCurrentPassword(GlobalData.GetUserID(), currentPassword))
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.");
                return;
            }

            if (dataAccessLayer.UpdatePassword(GlobalData.GetUserID(), newPassword))
            {
                MessageBox.Show("Password successfully updated.");
            }
            else
            {
                MessageBox.Show("Failed to update password.");
            }
        }

        private void currPass_TextChanged(object sender, EventArgs e)
        {
            currPass.PasswordChar = '*';
        }

        private void newPass_TextChanged(object sender, EventArgs e)
        {
            newPass.PasswordChar = '*';
        }

        private void confirmPass_TextChanged(object sender, EventArgs e)
        {
            confirmPass.PasswordChar = '*';
        }

        private void showPassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
            newPass.PasswordChar = showPassCheckBox.Checked ? '\0' : '*';
            confirmPass.PasswordChar = showPassCheckBox.Checked ? '\0' : '*';
        }

        private void showPassCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            currPass.PasswordChar = showPassCheckBox2.Checked ? '\0' : '*';
        }
    }

}
