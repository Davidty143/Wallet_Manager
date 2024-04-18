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
            PopulateCategory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new Transaction object with the data from the form
            float amount;
            if (!float.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Please enter a valid number for the amount.");
                return;
            }

            string walletCategory = "";

            if (checkBoxSpending.Checked)
            {
                walletCategory = "Spending";
            }
            else if (checkBoxSavings.Checked)
            {
                walletCategory = "Savings";
            }

            Transaction newIncome = new Transaction
            {
                UserID = 1,
                WalletID = Convert.ToInt32(txtWallet.SelectedValue),
                WalletCategory = walletCategory,
                TransactionType = "Income",
                Category = txtCategory.Text,
                Amount = float.Parse(txtAmount.Text),
                Date = txtDate.Value,
                Description = txtDescription.Text,
            };

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";

            // Add the new income to the database
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            bool isIncomeAdded = _dataAccessLayer.AddTransaction(newIncome);

            if (isIncomeAdded)
            {
                // If the income was added successfully, update the wallet balance
                Wallet wallet = _dataAccessLayer.GetWallet(newIncome.WalletID);

                if (newIncome.WalletCategory == "Spending")
                {
                    wallet.SpendingMoney += newIncome.Amount;
                }
                else if (newIncome.WalletCategory == "Savings")
                {
                    wallet.SavingsMoney += newIncome.Amount;
                }

                _dataAccessLayer.UpdateWallet(wallet);

                // Clear the form and show a success message
                ClearForm();
                MessageBox.Show("Income added successfully!");
            }
            else
            {
                // If the income was not added, show an error message
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
        public void PopulateCategory()
        {

            foreach (IncomeCategory value in Enum.GetValues(typeof(IncomeCategory)))
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                DisplayEnum displayNameAttribute = (DisplayEnum)fieldInfo.GetCustomAttribute(typeof(DisplayEnum));
                string displayName = displayNameAttribute != null ? displayNameAttribute.DisplayName : value.ToString();
                txtCategory.Items.Add(new KeyValuePair<IncomeCategory, string>(value, displayName));

                txtCategory.DisplayMember = "Value";
                txtCategory.ValueMember = "Key";
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
