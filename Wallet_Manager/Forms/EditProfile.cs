using MySql.Data.MySqlClient;
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
    public partial class EditProfile : UserControl
    {
        public EditProfile()
        {
            InitializeComponent();
            LoadUserProfile();
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

        private void EditProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
