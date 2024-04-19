﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;
using Wallet_Manager;
using System.Reflection;
using Wallet_Manager.Enums;

namespace Wallet_Manager.Forms
{
    public partial class AddIncome : Form
    {
        public AddIncome()
        {
            InitializeComponent();
            PopulateWalletsComboBox();
            PopulateIncomeCategoryComboBox();
        }

private void button1_Click(object sender, EventArgs e)
{
    // Validate and parse the amount entered
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

    // Validate and parse the selected category ID
    if (!(txtCategory.SelectedValue is int categoryId))
    {
        MessageBox.Show("Please select a valid category.");
        return;
    }

    // Create a new Transaction object
    Transaction newIncome = new Transaction
    {
        UserID = 1, // Assuming a static user ID for simplicity
        WalletID = Convert.ToInt32(txtWallet.SelectedValue),
        WalletCategory = walletCategory,
        TransactionType = "Income",
        CategoryID = categoryId,
        Amount = amount,
        Date = txtDate.Value,
        Description = txtDescription.Text
    };

    // Database connection string
    string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";

    // Add the new income to the database
    SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
    bool isIncomeAdded = dataAccessLayer.AddTransaction(newIncome);

    if (isIncomeAdded)
    {
        // Update the wallet balance
        Wallet wallet = dataAccessLayer.GetWallet(newIncome.WalletID);
        if (walletCategory == "Spending")
        {
            wallet.SpendingMoney += amount;
        }
        else if (walletCategory == "Savings")
        {
            wallet.SavingsMoney += amount;
        }

        dataAccessLayer.UpdateWallet(wallet);

        // Clear the form and show a success message
        ClearForm();
        MessageBox.Show("Income added successfully!");
    }
    else
    {
        MessageBox.Show("An error occurred. Please try again.");
    }
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


        private void PopulateIncomeCategoryComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            try
            {
                DataTable incomeCategories = dataAccessLayer.GetIncomeCategories();
                txtCategory.DataSource = incomeCategories;
                txtCategory.DisplayMember = "Name";
                txtCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating income category combo box: " + ex.Message);
            }
        }


        private void ClearForm()
        {

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtWallet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddIncome_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxSavings_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
