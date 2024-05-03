using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Wallet_Manager.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Wallet_Manager.Forms
{
    public partial class EditProfile : UserControl
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
        public EditProfile()
        {
            InitializeComponent();
            LoadUserProfile();
            LoadUserProfilePicture();
        }

        private void LoadUserProfile()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT FirstName, LastName, Email FROM User WHERE UserID = @UserID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                // Assuming you have a way to get the current user's ID, such as a global variable or passed parameter
                cmd.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile: " + ex.Message);
                }
            }
        }

        private void LoadUserProfilePicture()
        {
            try
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Image profile = _dataAccessLayer.GetProfilePicture(GlobalData.GetUserID());
                if (profile != null)
                {
                    profilePicture.Image = profile;
                }
                if (profile == null)
                {
                    profilePicture.Image = Properties.Resources.user;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile picture: " + ex.Message);
            }
        }

        private void EditProfile_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    profilePicture.Image = Image.FromFile(openFileDialog.FileName);
                    profilePicture.Tag = openFileDialog.FileName; // Store file path to use when saving
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Use standard email regex pattern
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }




        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("First and Last Name cannot be empty. Please enter valid names.");
                return; // Stop further execution if validation fails
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.");
                return; // Stop further execution if validation fails
            }
            try
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                string first = txtFirstName.Text;
                string last = txtLastName.Text;
                string email = txtEmail.Text;
                byte[] imageBytes = null;

                if (profilePicture.Tag != null)
                    imageBytes = dataAccessLayer.ImageToByteArray(profilePicture.Tag.ToString());

                dataAccessLayer.UpdateUserProfile(GlobalData.GetUserID(), first, last, email, imageBytes);
                GlobalEvents.OnProfileInformationUpdated();

                MessageBox.Show("Profile updated successfully!");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update profile: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            profilePicture.Image = Properties.Resources.user;
        }
    }
}
