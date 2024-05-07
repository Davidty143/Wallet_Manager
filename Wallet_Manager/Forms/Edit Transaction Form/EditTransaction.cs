using Guna.UI2.WinForms;
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
    public partial class EditTransaction : Form
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

        int txtTransactionID;
        int txtUserID = 1; 
        public EditTransaction(int transactionID)
        {
            txtTransactionID = transactionID;
            InitializeComponent();
            populateTransactionType();
            PopulateWalletsComboBox();
            LoadTransactionDetails(txtTransactionID);
            txtAmount.MaxLength = 7;
            GlobalEvents.TransactionUpdated += PopulateWalletsComboBox;
        }

        private void populateTransactionType()
        {
            txtTransactionType.Items.Add("Expense");
            txtTransactionType.Items.Add("Income");
            txtTransactionType.Items.Add("Transfer");
            txtTransactionType.SelectedIndex = 0;  
        }

        private void PopulateCategoryComboBox(string transactionType)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            DataTable categories;
            try
            {
                switch (transactionType)
                {
                    case "Expense":
                        categories = dataAccessLayer.GetExpenseCategories();
                        break;
                    case "Income":
                        categories = dataAccessLayer.GetIncomeCategories();
                        break;
                    case "Transfer":
                        categories = dataAccessLayer.GetTransferCategories(); 
                        break;
                    default:
                        categories = new DataTable();
                        break;
                }

                txtCategory.DisplayMember = "Name";
                txtCategory.ValueMember = "CategoryId";
                txtCategory.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating categories: " + ex.Message);
            }
        }
        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName} (ID: {wallet.WalletID})",
                Value = wallet.WalletID
            }).ToList();

            txtWallet.DisplayMember = "Text";
            txtWallet.ValueMember = "Value";
            txtWallet.DataSource = walletBindingList;
        }


        private void LoadTransactionDetails(int transactionId)
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            var transaction = dataAccessLayer.GetTransactionById(transactionId);
            if (transaction != null)
            {
                txtWallet.Text = dataAccessLayer.GetWalletNameById(transaction.WalletID); // Display wallet name
                txtCategory.Text = dataAccessLayer.GetCategoryNameById(transaction.CategoryID); // Display category name
                checkBoxSavings.Checked = transaction.WalletCategory == "Savings";
                checkBoxSpending.Checked = transaction.WalletCategory == "Spending";
                txtTransactionType.Text = transaction.TransactionType;
                txtAmount.Text = transaction.Amount.ToString(); // Format for currency
                txtDate.Value = transaction.Date;
                txtDescription.Text = transaction.Description;
            }
            else
            {
                MessageBox.Show("Transaction not found.");
            }
        }

        private bool AdjustWalletBalance(Transaction originalTransaction, Transaction updatedTransaction)
        {
            float originalEffect = originalTransaction.TransactionType == "Income" ? originalTransaction.Amount : -originalTransaction.Amount;
            float updatedEffect = updatedTransaction.TransactionType == "Income" ? updatedTransaction.Amount : -updatedTransaction.Amount;

            float netChange = updatedEffect - originalEffect;

            if (originalTransaction.WalletID != updatedTransaction.WalletID)
            {
                if (!UpdateWalletBalance(originalTransaction.WalletID, -originalEffect) ||
                    !UpdateWalletBalance(updatedTransaction.WalletID, updatedEffect))
                {
                    return false;
                }
            }
            else
            {
                if (!UpdateWalletBalance(originalTransaction.WalletID, netChange))
                {
                    return false;
                }
            }

            return true;
        }


        private bool UpdateWalletBalance(int walletId, float amountChange)
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            Wallet wallet = dataAccessLayer.GetWallet(walletId);
            if (wallet == null)
            {
                return false; 
            }

            float newSpendingMoney = wallet.SpendingMoney + amountChange;
            if (amountChange < 0 && newSpendingMoney < 0)
            {
                return false;
            }

            wallet.SpendingMoney = newSpendingMoney; 

            return dataAccessLayer.UpdateWallet(wallet);
        }

        private void txtWallet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {


                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Transaction originalTransaction = dataAccessLayer.GetTransactionById(txtTransactionID);

                Transaction updatedTransaction = new Transaction
                {
                    TransactionID = txtTransactionID,
                    UserID = txtUserID,
                    WalletID = Convert.ToInt32(txtWallet.SelectedValue),
                    CategoryID = Convert.ToInt32(txtCategory.SelectedValue),
                    WalletCategory = checkBoxSavings.Checked ? "Savings" : "Spending",
                    TransactionType = txtTransactionType.Text,
                    Amount = float.Parse(txtAmount.Text),
                    Date = txtDate.Value,
                    Description = txtDescription.Text
                };

                if (originalTransaction.WalletID != updatedTransaction.WalletID || originalTransaction.Amount != updatedTransaction.Amount)
                {
                    if (!AdjustWalletBalance(originalTransaction, updatedTransaction))
                    {
                        MessageBox.Show("Insufficient funds to update the transaction.");
                        return;
                    }
                }

                dataAccessLayer.UpdateTransaction(updatedTransaction);
                MessageBox.Show("Transaction updated successfully.");
                GlobalEvents.OnTransactionUpdated();
                GlobalEvents.onBudgetUpdated();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating transaction: " + ex.Message);
            }
        }

        private void txtTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCategoryComboBox(txtTransactionType.SelectedItem.ToString());

        }

        private void checkBoxSpending_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSpending.Checked)
            {
                checkBoxSavings.Checked = false;
            }
        }

        private void checkBoxSavings_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxSavings.Checked)
            {
                checkBoxSpending.Checked = false;
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox txt = sender as Guna2TextBox;
            if (txt.Text.Length > 13)
            {
                txt.Text = txt.Text.Substring(0, 17);
                txt.SelectionStart = txt.Text.Length;
            }
        
        }
    }
}
