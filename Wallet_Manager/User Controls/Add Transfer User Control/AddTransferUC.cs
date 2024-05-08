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
    public partial class AddTransferUC : UserControl
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
        public AddTransferUC()
        {
            InitializeComponent();
            PopulateWalletsComboBox();
            InitializeComboBoxes();
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

            var sourceWalletBindingList = wallets.Select(wallet => new
            {
                Text = wallet.WalletName,
                Value = wallet.WalletID
            }).ToList();

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

        private void ClearForm()
        {
            txtAmount.Clear();
            txtDescription.Clear();
            txtSourceWallet.SelectedIndex = -1;
            txtDestinationWallet.SelectedIndex = -1;
            ScheckBoxSavings.Checked = false;
            ScheckBoxSpending.Checked = false;
            DcheckBoxSavings.Checked = false;
            DcheckBoxSpending.Checked = false;
            txtDate.Value = DateTime.Now;
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
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
            if (string.IsNullOrEmpty(sourceCategory) || string.IsNullOrEmpty(targetCategory))
            {
                MessageBox.Show("Please select both source and target categories.");
                return;
            }

            int sourceWalletId = Convert.ToInt32(txtSourceWallet.SelectedValue);
            int targetWalletId = Convert.ToInt32(txtDestinationWallet.SelectedValue);


            float amount;

            if (!float.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

                
            string description = txtDescription.Text;

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);


            try
            {
                bool transferSuccess = dataAccessLayer.Transfer(sourceWalletId, sourceCategory, targetWalletId, targetCategory, amount, GlobalData.GetUserID(), description);
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
            ClearForm();

        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ScheckBoxSpending_CheckedChanged(object sender, EventArgs e)
        {
            if (ScheckBoxSpending.Checked)
            {
                ScheckBoxSavings.Checked = false;
            }
        }

        private void ScheckBoxSavings_CheckedChanged(object sender, EventArgs e)
        {
            if (ScheckBoxSavings.Checked)
            {
                ScheckBoxSpending.Checked = false;
            }
        }

        private void DcheckBoxSpending_CheckedChanged(object sender, EventArgs e)
        {
            if (DcheckBoxSpending.Checked)
            {
                DcheckBoxSavings.Checked = false;
            }
        }

        private void DcheckBoxSavings_CheckedChanged(object sender, EventArgs e)
        {
            if (DcheckBoxSavings.Checked)
            {
                DcheckBoxSpending.Checked = false;      
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox txt = sender as Guna2TextBox;
            if (txt.Text.Length > 17)
            {
                txt.Text = txt.Text.Substring(0, 17);
                txt.SelectionStart = txt.Text.Length;
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {

        }
    }
}
