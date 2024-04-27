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
    public partial class AddExpenseUC : UserControl
    {
        private AddTransaction addTransaction;
        public AddExpenseUC()
        {
            InitializeComponent();
             //this.addTransaction = addTransaction;
            PopulateWalletsComboBox();
            PopulateExpenseCategoryComboBox();
            
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
        private void PopulateExpenseCategoryComboBox()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            try
            {
                DataTable expenseCategories = dataAccessLayer.GetExpenseCategories();

                txtCategory.DisplayMember = "Name";
                txtCategory.ValueMember = "CategoryId";
                txtCategory.DataSource = expenseCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating expense categories: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtAmount.Clear();
            txtDescription.Clear();
            txtCategory.SelectedIndex = -1;
            txtWallet.SelectedIndex = -1;
            checkBoxSpending.Checked = false;
            checkBoxSavings.Checked = false;
            txtDate.Value = DateTime.Now;
        }


        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.FindForm().Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Parse the amount from the text box
            if (!float.TryParse(txtAmount.Text, out float amount))
            {
                MessageBox.Show("Please enter a valid number for the amount.");
                return;
            }

            // Determine the wallet category based on checkbox selection
            string walletCategory = checkBoxSpending.Checked ? "Spending" : checkBoxSavings.Checked ? "Savings" : string.Empty;
            if (string.IsNullOrEmpty(walletCategory))
            {
                MessageBox.Show("Please select a wallet category.");
                return;
            }

            // Ensure a category is selected
            if (txtCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }
            int categoryId = Convert.ToInt32(txtCategory.SelectedValue);

            // Create a new Transaction object
            Transaction newExpense = new Transaction
            {
                UserID = 1, // Assuming a static user ID for simplicity
                WalletID = Convert.ToInt32(txtWallet.SelectedValue),
                WalletCategory = walletCategory,
                TransactionType = "Expense",
                CategoryID = categoryId,
                Amount = amount,
                Date = txtDate.Value,
                Description = txtDescription.Text
            };

            // Initialize the data access layer
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            // Check if the wallet has enough balance before adding the expense
            Wallet wallet = dataAccessLayer.GetWallet(newExpense.WalletID);
            if ((walletCategory == "Spending" && wallet.SpendingMoney < amount) ||
                (walletCategory == "Savings" && wallet.SavingsMoney < amount))
            {
                MessageBox.Show("Not enough balance in " + walletCategory + " Money.");
                return;
            }

            // Add the new expense to the database
            bool isExpenseAdded = dataAccessLayer.AddTransaction(newExpense);
            if (isExpenseAdded)
            {
                // Update the wallet balance
                if (walletCategory == "Spending")
                {
                    wallet.SpendingMoney -= amount;
                }
                else if (walletCategory == "Savings")
                {
                    wallet.SavingsMoney -= amount;
                }

                dataAccessLayer.UpdateWallet(wallet);

                // Clear the form and show a success message
                ClearForm();
                MessageBox.Show("Expense added successfully!");
            }
            else
            {
                MessageBox.Show("An error occurred. Please try again.");
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.FindForm().Hide();
        }

        private void guna2CustomGradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
