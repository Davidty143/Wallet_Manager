using MySql.Data.MySqlClient;
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
        private static SearchFilter instance = null;
        private TransactionHistory _transactionHistory;

        public SearchFilter(TransactionHistory transactionHistory)
        {
            InitializeComponent();
            populateTransactionType();
            PopulateWalletCategoryComboBox();
            PopulateWalletsComboBox();
            _transactionHistory = transactionHistory;
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

        public static SearchFilter GetInstance(TransactionHistory transactionHistory)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new SearchFilter(transactionHistory);
            }
            return instance;
        }


        private void FilterTransactions()
        {
            string transactionType = transactionTypeComboBox.SelectedItem?.ToString();
            string category = categoryComboBox.SelectedItem?.ToString();
            string wallet = walletComboBox.SelectedItem?.ToString();
            string walletCategory = walletCategoryComboBox.SelectedItem?.ToString();
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date.AddDays(1); // Include the end date in the search

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                StringBuilder query = new StringBuilder("SELECT * FROM Transaction WHERE 1=1 ");
                List<MySqlParameter> parameters = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(transactionType))
                {
                    query.Append("AND TransactionType = @TransactionType ");
                    parameters.Add(new MySqlParameter("@TransactionType", transactionType));
                }
                if (!string.IsNullOrEmpty(category))
                {
                    query.Append("AND CategoryID = @CategoryID ");
                    parameters.Add(new MySqlParameter("@CategoryID", category));
                }
                if (!string.IsNullOrEmpty(wallet))
                {
                    query.Append("AND WalletID = @WalletID ");
                    parameters.Add(new MySqlParameter("@WalletID", wallet));
                }
                if (!string.IsNullOrEmpty(walletCategory))
                {
                    query.Append("AND WalletCategory = @WalletCategory ");
                    parameters.Add(new MySqlParameter("@WalletCategory", walletCategory));
                }
                query.Append("AND DATE BETWEEN @StartDate AND @EndDate");
                parameters.Add(new MySqlParameter("@StartDate", startDate));
                parameters.Add(new MySqlParameter("@EndDate", endDate));

                using (MySqlCommand cmd = new MySqlCommand(query.ToString(), conn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {

                        DataTable filteredTransactions = new DataTable();
                        adapter.Fill(filteredTransactions);
                        //transactionsDataGridView.DataSource = filteredTransactions;
                    }
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string transactionType = transactionTypeComboBox.SelectedItem?.ToString();
            string category = categoryComboBox.SelectedValue?.ToString();
            string wallet = walletComboBox.SelectedValue?.ToString();
            string walletCategory = walletCategoryComboBox.SelectedItem?.ToString();
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date.AddDays(1); // Include the end date in the search

            // Call ApplyFilters on the TransactionHistory form
            _transactionHistory.ApplyFilters(transactionType, category, wallet, walletCategory, startDate, endDate);
            this.Close(); // Optionally close the SearchFilter form
        }
    }
}
