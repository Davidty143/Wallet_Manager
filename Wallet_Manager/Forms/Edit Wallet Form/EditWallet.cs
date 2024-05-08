using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class EditWallet : Form
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

        private Wallet currentWallet;
        public EditWallet(Wallet wallet)
        {
            InitializeComponent();
            currentWallet = wallet;
            walletNameTextBox.MaxLength = 13;
            savingsAmountTextBox.MaxLength = 7;
            spendingAmountTextBox.MaxLength = 7;
            PopulateWalletTypes();
            PopulateWalletDetails();
            

        }

        private void PopulateWalletTypes()
        {
            List<string> walletTypes = new List<string>
            {
                "Pocket Wallet",
                "E-Wallet",
                "Bank Wallet",
                "Crypto Wallet",
                "Travel Wallet"
            };

            walletTypeComboBox.DataSource = walletTypes;
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


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Wallet_Click_1(object sender, EventArgs e)
        {
            currentWallet.WalletName = walletNameTextBox.Text;
            currentWallet.WalletType = walletTypeComboBox.Text;

            if (!float.TryParse(spendingAmountTextBox.Text, out float newSpendingAmount))
            {
                MessageBox.Show("Invalid input for spending amount.");
                return;
            }

            if (!float.TryParse(savingsAmountTextBox.Text, out float newSavingsAmount))
            {
                MessageBox.Show("Invalid input for savings amount.");
                return;
            }

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            if (currentWallet.SpendingMoney != newSpendingAmount)
            {
                float amountDifference = newSpendingAmount - currentWallet.SpendingMoney;
                string transactionType = amountDifference > 0 ? "Income" : "Expense";
                int categoryID = amountDifference > 0 ? 8 : 17;

                Transaction transaction = new Transaction
                {
                    UserID = GlobalData.GetUserID(),
                    WalletID = currentWallet.WalletID,
                    WalletCategory = "Spending",
                    TransactionType = transactionType,
                    CategoryID = categoryID,
                    Amount = Math.Abs(amountDifference),
                    Date = DateTime.Now,
                    Description = "Edit Wallet"
                };
                if (!_dataAccessLayer.AddTransaction(transaction))
                {
                    MessageBox.Show("Failed to record spending transaction.");
                    return;
                }
            }

            if (currentWallet.SavingsMoney != newSavingsAmount)
            {
                float amountDifference = newSavingsAmount - currentWallet.SavingsMoney;
                string transactionType = amountDifference > 0 ? "Income" : "Expense";
                int categoryID = amountDifference > 0 ? 8 : 17;

                Transaction transaction = new Transaction
                {
                    UserID = GlobalData.GetUserID(),
                    WalletID = currentWallet.WalletID,
                    WalletCategory = "Savings",
                    TransactionType = transactionType,
                    CategoryID = categoryID,
                    Amount = Math.Abs(amountDifference),
                    Date = DateTime.Now,
                    Description = "Edit Wallet"
                };
                if (!_dataAccessLayer.AddTransaction(transaction))
                {
                    MessageBox.Show("Failed to record savings transaction.");
                    return;
                }
            }

            currentWallet.SpendingMoney = newSpendingAmount;
            currentWallet.SavingsMoney = newSavingsAmount;
            bool updateSuccess = _dataAccessLayer.UpdateWallet(currentWallet);

            if (updateSuccess)
            {
                MessageBox.Show("Wallet updated successfully.");
                GlobalEvents.OnTransactionUpdated();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error updating wallet.");
            }
        }

        private void savingsAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void spendingAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void walletNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
