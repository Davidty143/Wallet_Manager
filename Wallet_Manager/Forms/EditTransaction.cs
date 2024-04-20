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

        public EditTransaction()
        {
            InitializeComponent();
            populateTransactionType();
            PopulateWalletsComboBox();
        }

        private void populateTransactionType()
        {
            txtTransactionType.Items.Add("Expense");
            txtTransactionType.Items.Add("Income");
            txtTransactionType.Items.Add("Transfer");
            txtTransactionType.SelectedIndex = 0;  
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCategoryComboBox(txtTransactionType.SelectedItem.ToString());
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
                        categories = dataAccessLayer.GetIncomeCategories();  // Assume similar method exists
                        break;
                    case "Transfer":
                        categories = dataAccessLayer.GetTransferCategories();  // Assume similar method exists
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

        private void LoadTransactionData(int transactionId)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            Transaction transaction = dataAccessLayer.GetTransactionById(transactionId);

            if (transaction != null)
            {
                txtTransactionType.SelectedItem = transaction.Type;
                txtCategory.SelectedValue = transaction.CategoryId;
                txtAmount.Text = transaction.Amount.ToString();
                txtDate.Value = transaction.Date;
                txtWallet.SelectedValue = transaction.WalletId;
                txtDescription.Text = transaction.Description;
            }
            else
            {
                MessageBox.Show("Transaction not found.");
            }
        }




        private void EditTransaction_Load(object sender, EventArgs e)
        {

        }
    }
}
