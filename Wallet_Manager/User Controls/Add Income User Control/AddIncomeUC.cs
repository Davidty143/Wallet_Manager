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
    public partial class AddIncomeUC : UserControl
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
        private AddTransaction addTransaction;
        public AddIncomeUC()
        {

            InitializeComponent();
            PopulateWalletsComboBox();
            PopulateIncomeCategoryComboBox();
            ClearForm();
            txtAmount.MaxLength = 7;
            txtDate.Value = DateTime.Today;
            GlobalEvents.TransactionUpdated += PopulateWalletsComboBox;
            ClearForm();

        }

        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName}",
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
            txtAmount.Clear();
            txtDescription.Clear();
            txtCategory.SelectedIndex = -1;
            txtWallet.SelectedIndex = -1;
            checkBoxSpending.Checked = false;
            checkBoxSavings.Checked = false;
            txtDate.Value = DateTime.Now;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(txtAmount.Text, out float amount) || amount <= 0 )
            {
                MessageBox.Show("Please enter a valid number for the amount.");
                return;
            }

            string walletCategory = checkBoxSpending.Checked ? "Spending" : checkBoxSavings.Checked ? "Savings" : string.Empty;
            if (string.IsNullOrEmpty(walletCategory))
            {
                MessageBox.Show("Please select a wallet category.");
                return;
            }

            if (!(txtCategory.SelectedValue is int categoryId))
            {
                MessageBox.Show("Please select a valid category.");
                return;
            }

            Transaction newIncome = new Transaction
            {
                UserID = GlobalData.GetUserID(), 
                WalletID = Convert.ToInt32(txtWallet.SelectedValue),
                WalletCategory = walletCategory,
                TransactionType = "Income",
                CategoryID = categoryId,
                Amount = amount,
                Date = txtDate.Value,
                Description = txtDescription.Text
            };

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            bool isIncomeAdded = dataAccessLayer.AddTransaction(newIncome);

            if (isIncomeAdded)
            {
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
                ClearForm();
                MessageBox.Show("Income added successfully!");
                GlobalEvents.OnTransactionUpdated();
            }
            else
            {
                MessageBox.Show("An error occurred. Please try again.");
            }
            
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.FindForm().Hide();
            ClearForm();

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox txt = sender as Guna2TextBox;
            if (txt.Text.Length > 13)
            {
                txt.Text = txt.Text.Substring(0, 13);
                txt.SelectionStart = txt.Text.Length;
            }
        }

        private void checkBoxSavings_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSavings.Checked)
            {
                checkBoxSpending.Checked = false;
            }
        }

        private void checkBoxSpending_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpending.Checked)
            {
                checkBoxSavings.Checked = false;
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
