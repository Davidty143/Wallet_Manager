using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class SearchFilter : Form
    {
        private static SearchFilter instance;

        public SearchFilter()
        {
            InitializeComponent();
            populateTransactionType();
            PopulateWalletCategoryComboBox();
            PopulateWalletsComboBox();
        }

        private void walletComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void PopulateWalletCategoryComboBox()
        {
            walletCategoryComboBox.Items.Add("Savings");
            walletCategoryComboBox.Items.Add("Spending");
            walletCategoryComboBox.SelectedIndex = 0;

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

                categoryComboBox.DisplayMember = "Name";
                categoryComboBox.ValueMember = "CategoryId";
                categoryComboBox.DataSource = categories;
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

            walletComboBox.DisplayMember = "Text";
            walletComboBox.ValueMember = "Value";
            walletComboBox.DataSource = walletBindingList;
        }

        private void populateTransactionType()
        {
            transactionTypeComboBox.Items.Add("Expense");
            transactionTypeComboBox.Items.Add("Income");
            transactionTypeComboBox.Items.Add("Transfer");
            transactionTypeComboBox.SelectedIndex = 0;
        }

        public static SearchFilter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchFilter();
                }
                return instance;
            }
        }





        private void SearchFilter_Load(object sender, EventArgs e)
        {

        }

        private void transactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCategoryComboBox(transactionTypeComboBox.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void walletTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
