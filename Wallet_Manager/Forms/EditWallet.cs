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
    public partial class EditWallet : Form
    {
        private Wallet currentWallet;
        public EditWallet(Wallet wallet)
        {
            InitializeComponent();
            currentWallet = wallet;
            PopulateWalletDetails();

        }

        private void PopulateWalletDetails()
        {
            if (currentWallet != null)
            {
                walletNameTextBox.Text = currentWallet.WalletName;
                walletTypeComboBox.Text = currentWallet.WalletType;
                spendingAmountTextBox.Text = currentWallet.SpendingMoney.ToString();
                savingsAmountTextBox.Text = currentWallet.SavingsMoney.ToString();
            }
            else
            {
                MessageBox.Show("Error: No wallet data provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EditWallet_Load(object sender, EventArgs e)
        {

        }

        private void add_Wallet_Click(object sender, EventArgs e)
        {
            // Validate input values here
            currentWallet.WalletName = walletNameTextBox.Text;
            currentWallet.WalletType = walletTypeComboBox.Text;
            currentWallet.SpendingMoney = float.Parse(spendingAmountTextBox.Text); // Add error handling for parsing
            currentWallet.SavingsMoney = float.Parse(savingsAmountTextBox.Text); // Add error handling for parsing

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            bool updateSuccess = _dataAccessLayer.CheckAndUpdateWallet(currentWallet);
            if (updateSuccess)
            {
                MessageBox.Show("Wallet updated successfully.");
                this.Hide(); // Optionally close the form
            }
            else
            {
                MessageBox.Show("Error updating wallet.");
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void spendingAmountTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
