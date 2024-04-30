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
    public partial class AddTransferUC : UserControl
    {
        private AddTransaction addTransaction;
        public AddTransferUC()
        {
            InitializeComponent();
            //this.addTransaction = addTransaction;
            PopulateWalletsComboBox();
            InitializeComboBoxes();
            txtDate.Value = DateTime.Today;
        }

        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            // Create a binding list for the source wallet ComboBox
            var sourceWalletBindingList = wallets.Select(wallet => new
            {
                Text = wallet.WalletName,
                Value = wallet.WalletID
            }).ToList();

            // Create a binding list for the destination wallet ComboBox
            var destinationWalletBindingList = wallets.Select(wallet => new
            {
                Text = wallet.WalletName,
                Value = wallet.WalletID
            }).ToList();

            txtSourceWallet.DisplayMember = "Text";
            txtSourceWallet.ValueMember = "Value";
            txtSourceWallet.DataSource = sourceWalletBindingList;

            txtDestinationWallet.DisplayMember = "Text";
            txtDestinationWallet.ValueMember = "Value";
            txtDestinationWallet.DataSource = destinationWalletBindingList;
        }

        private void InitializeComboBoxes()
        {
            txtSourceWallet.DisplayMember = "Text";
            txtDestinationWallet.DisplayMember = "Text";
            txtSourceWallet.ValueMember = "Value";
            txtDestinationWallet.ValueMember = "Value";
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.FindForm().Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceCategory = "";
            string targetCategory = "";

            if (ScheckBoxSpending.Checked)
            {
                sourceCategory = "Spending";
            }
            else if (ScheckBoxSavings.Checked)
            {
                sourceCategory = "Savings";
            }

            if (DcheckBoxSpending.Checked)
            {
                targetCategory = "Spending";
            }
            else if (DcheckBoxSavings.Checked)
            {
                targetCategory = "Savings";
            }

            // Ensure that both categories are selected
            if (string.IsNullOrEmpty(sourceCategory) || string.IsNullOrEmpty(targetCategory))
            {
                MessageBox.Show("Please select both source and target categories.");
                return;
            }

            // Get the selected wallets, categories, amount, and description
            int sourceWalletId = Convert.ToInt32(txtSourceWallet.SelectedValue);
            int targetWalletId = Convert.ToInt32(txtDestinationWallet.SelectedValue);


            float amount;
            if (!float.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }
            string description = txtDescription.Text;
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";

            // Create a new instance of BusinessLogic
            BusinessLogic businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));


            // Perform the transfer
            try
            {
                bool transferSuccess = businessLogic.Transfer(sourceWalletId, sourceCategory, targetWalletId, targetCategory, amount, 1, description);
                if (transferSuccess)
                {
                    MessageBox.Show("Transfer completed successfully.");
                    GlobalEvents.OnTransactionUpdated();
                }
                else
                {
                    MessageBox.Show("An error occurred while performing the transfer. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.FindForm().Hide();
        }

        private void guna2CustomGradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
