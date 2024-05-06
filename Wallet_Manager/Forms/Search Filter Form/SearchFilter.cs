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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private static SearchFilter instance = null;
        private TransactionHistory _transactionHistory;
        private const string ALL = "All";
        private const int ALL_ID = 0;


        public SearchFilter(TransactionHistory transactionHistory)
        {
            InitializeComponent();
            populateTransactionType();
            PopulateWalletCategoryComboBox();
            PopulateWalletsComboBox();
            _transactionHistory = transactionHistory;
            startDatePicker.Value = DateTime.Today;
            endDatePicker.Value = DateTime.Today;

            GlobalEvents.TransactionUpdated += PopulateWalletsComboBox;
        }

        public void PopulateWalletCategoryComboBox()
        {
            walletCategoryComboBox.Items.Add("All");
            walletCategoryComboBox.Items.Add("Spending");
            walletCategoryComboBox.Items.Add("Savings");
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
                    case "All":
                        categories = dataAccessLayer.GetAllCategories();
                        break;
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

                DataRow newRow = categories.NewRow();
                newRow["CategoryId"] = DBNull.Value;
                newRow["Name"] = "All";
                categories.Rows.InsertAt(newRow, 0);

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

            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName}",
                Value = wallet.WalletID
            }).ToList();

            walletBindingList.Insert(0, new { Text = "All", Value = 0 });

            walletComboBox.DisplayMember = "Text";
            walletComboBox.ValueMember = "Value";
            walletComboBox.DataSource = walletBindingList;
        }

        private void populateTransactionType()
        {
            transactionTypeComboBox.Items.Add("All");
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

        private void transactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCategoryComboBox(transactionTypeComboBox.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string transactionType = transactionTypeComboBox.SelectedItem?.ToString();
            string category = categoryComboBox.SelectedValue?.ToString();
            string wallet = walletComboBox.SelectedValue?.ToString();
            string walletCategory = walletCategoryComboBox.SelectedItem?.ToString();
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date.AddDays(1);

            _transactionHistory.ApplyFilters(transactionType, category, wallet, walletCategory, startDate, endDate);
            this.Hide();
        }

        private void transactionTypeComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            PopulateCategoryComboBox(transactionTypeComboBox.Text);
        }

    }
}
