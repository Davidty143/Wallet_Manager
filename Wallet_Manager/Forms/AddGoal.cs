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
    public partial class AddGoal : Form
    {
        public AddGoal(int userID)
        {
            InitializeComponent();
            PopulateWalletsComboBox();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
                
        }
        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            // Convert wallets to a binding-friendly format
            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName} (ID: {wallet.WalletID})",
                Value = wallet.WalletID
            }).ToList();

            txtWallet.DisplayMember = "Text";
            txtWallet.ValueMember = "Value";
            txtWallet.DataSource = walletBindingList;
        }

        private void AddGoal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate and convert inputs
                string goalName = txtName.Text;
                if (string.IsNullOrWhiteSpace(goalName))
                {
                    MessageBox.Show("Please enter a valid goal name.");
                    return;
                }

                if (!float.TryParse(txtTargetAmount.Text, out float targetAmount) || targetAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid target amount.");
                    return;
                }

                if (!float.TryParse(txtCurrentAmount.Text, out float currentAmount) || currentAmount < 0)
                {
                    MessageBox.Show("Please enter a valid target amount.");
                    return;
                }   



                DateTime? deadline = txtDate.Value;
                int walletId = Convert.ToInt32(txtWallet.SelectedValue);

                // Create a new Goal object
                Goal newGoal = new Goal
                {
                    UserID = 1,
                    GoalName = goalName,
                    TargetAmount = targetAmount,
                    CurrentAmount = currentAmount,
                    Deadline = deadline,    
                    WalletID = walletId
                };

                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                dataAccessLayer.AddGoal(newGoal);

                MessageBox.Show("Goal added successfully!");
                this.Close(); // Optionally close the form or clear the inputs for a new entry
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add goal. Error: " + ex.Message);
            }
        }
    }
}
