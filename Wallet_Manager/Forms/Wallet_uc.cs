﻿using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class Wallet_uc : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private int currentWalletID;
        PictureBox editPictureBox = new System.Windows.Forms.PictureBox();
        PictureBox deletePictureBox = new System.Windows.Forms.PictureBox();

        private List<Transaction> transactions = new List<Transaction>();
        private Panel[] transactionPanels;
        private PictureBox[] categoryPictureBoxes;
        private Label[] descriptionLabels;
        private Label[] categoryLabels;
        private Label[] transactionTypeLabels;
        private Label[] amountLabels;
        private Label[] walletNameLabels;
        private Label[] dateLabels;
        private Image[] categoryImages;
        public Wallet_uc()
        {
            InitializeComponent();
            SetDoubleBuffering(this, true);
            LoadCategoryImages();
            InitializeControlArrays();
            PopulateWalletsComboBox();
            UpdateWalletDisplay();
            
            LoadTransactions();
            PopulateGunaBarDataSet();


            GlobalEvents.TransactionUpdated += UpdateWalletDisplay;
            GlobalEvents.TransactionUpdated += LoadTransactions;
            GlobalEvents.TransactionUpdated += PopulateGunaBarDataSet;
            
            editPictureBox.Click += editWalletPictureBox_Click;
            deletePictureBox.Click += deleteWalletPictureBox_Click;


        }

        public static void SetDoubleBuffering(Control control, bool value)
        {
            // Get the type of the control
            Type controlType = control.GetType();

            // Get the property info for the 'DoubleBuffered' property
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

            // Set the value of the DoubleBuffered property
            pi?.SetValue(control, value, null);

            // Recursively set DoubleBuffering to true for each child control
            foreach (Control childControl in control.Controls)
            {
                SetDoubleBuffering(childControl, value);
            }
        }




        private void UpdateWalletDisplay(Wallet wallet)
        {
            if (wallet != null)
            {
                walletNameLabel.Text = wallet.WalletName ?? "N/A";
                walletTypeLabel.Text = wallet.WalletType ?? "N/A";
                spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C"); // Assuming currency format
                savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C"); // Assuming currency format
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

            selectWalletComboBox.DisplayMember = "Text";
            selectWalletComboBox.ValueMember = "Value";
            selectWalletComboBox.DataSource = walletBindingList;
        }

        private void UpdateWalletDisplay()
        {
            int walletId = 0;
            if (int.TryParse(selectWalletComboBox.SelectedValue?.ToString(), out walletId))
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Wallet wallet = _dataAccessLayer.GetWalletById(walletId);
                if (wallet != null)
                {
                    currentWalletID = wallet.WalletID;
                    walletNameLabel.Text = wallet.WalletName ?? "N/A";
                    walletTypeLabel.Text = wallet.WalletType ?? "N/A";
                    spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C");
                    savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C");
                    totalBalanceLabel.Text = (wallet.SavingsMoney + wallet.SpendingMoney).ToString("C");
                }
            }
            else
            {
                // Handle the case where walletId is not valid or conversion failed
                MessageBox.Show("Selected wallet ID is invalid.");
            }
        }

        private void InitializeControlArrays()
        {
            transactionPanels = new Guna2CustomGradientPanel[] { recentTransactionPanel1, recentTransactionPanel2, recentTransactionPanel3 };
            categoryPictureBoxes = new PictureBox[] { iconPictureBox1, iconPictureBox2, iconPictureBox3 };
            descriptionLabels = new Label[] { descriptionLabel1, descriptionLabel2, descriptionLabel3 };
            categoryLabels = new Label[] { categoryLabel1, categoryLabel2, categoryLabel3 };
            transactionTypeLabels = new Label[] { transactionTypeLabel1, transactionTypeLabel2, transactionTypeLabel3 };
            amountLabels = new Label[] { amountLabel1, amountLabel2, amountLabel3 };
            walletNameLabels = new Label[] { walletLabel1, walletLabel2, walletLabel3 };
            dateLabels = new Label[] { dateLabel1, dateLabel2, dateLabel3 };
        }

        private void LoadCategoryImages()
        {
            categoryImages = new Image[19]; // Create an array to hold 19 images
            for (int i = 0; i < categoryImages.Length; i++)
            {
                string imageName = (i + 1).ToString(); // This will generate "1", "2", ..., "19"
                categoryImages[i] = (Image)Properties.Resources.ResourceManager.GetObject(imageName);

                if (categoryImages[i] == null)
                {
                    Console.WriteLine($"Image '{imageName}.png' not found for category ID: {i + 1}, using default image.");
                    categoryImages[i] = Properties.Resources.button_budget_active; // Fallback to a default image
                    if (categoryImages[i] == null)
                    {
                        Console.WriteLine("Default image is also not found. Check resource file.");
                    }
                }
            }


        }

        private void LoadTransactions()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            transactions = _dataAccessLayer.GetLatestThreeWalletTransactions(currentWalletID);
            UpdateTransactionDisplay();

            for (int i = 0; i < transactions.Count; i++)
            {
                if (i < transactionPanels.Length)
                {
                    var transaction = transactions[i];
                    transactionPanels[i].Visible = true;
                    descriptionLabels[i].Text = transaction.Description;
                    categoryLabels[i].Text = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    transactionTypeLabels[i].Text = transaction.TransactionType;
                    amountLabels[i].Text = $"{transaction.Amount:C}".Insert(1, " ");
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID) + " - " + transaction.WalletCategory.ToString();
                    if (transaction.Date.Date == DateTime.Today)
                    {
                        dateLabels[i].Text = "         Today";
                    }
                    else if (transaction.Date.Date == DateTime.Today.AddDays(-1))
                    {
                        dateLabels[i].Text = "   Yesterday";

                    }
                    else
                    {
                        dateLabels[i].Text = transaction.Date.ToString("d");
                    }
                }
                else
                {
                    transactionPanels[i].Visible = false;
                }
            }
        }

        private void UpdateTransactionDisplay()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            // Calculate the number of transactions to display (up to 3)
            int numberOfTransactionsToShow = Math.Min(3, transactions.Count);

            for (int i = 0; i < transactionPanels.Length; i++)
            {
                if (i < numberOfTransactionsToShow)
                {
                    var transaction = transactions[i]; // Get transaction by index
                    transactionPanels[i].Visible = true;
                    descriptionLabels[i].Text = transaction.Description;
                    categoryLabels[i].Text = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    transactionTypeLabels[i].Text = transaction.TransactionType;
                    amountLabels[i].Text = $"₱ {transaction.Amount}";
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    dateLabels[i].Text = transaction.Date.ToString("d");
                    // Check for null PictureBox and Image
                    if (categoryPictureBoxes[i] == null)
                    {
                        Console.WriteLine("PictureBox at index " + i + " is null.");
                        continue; // Skip this iteration
                    }

                    int imageIndex = transaction.CategoryID - 1; // Calculate the index
                    if (imageIndex < 0 || categoryImages[imageIndex] == null)
                    {
                        Console.WriteLine("Invalid or missing image for Category ID: " + transaction.CategoryID);
                        categoryPictureBoxes[i].Image = Properties.Resources.button_budget_active; // Use default image
                    }
                    else
                    {
                        categoryPictureBoxes[i].Image = categoryImages[imageIndex];
                    }
                }
                else
                {
                    transactionPanels[i].Visible = false;
                }
            }
        }

        private void PopulateGunaBarDataSet()
        {

            gunaBarDataset1.DataPoints.Clear();
            gunaBarDataset2.DataPoints.Clear();
            gunaBarDataset3.DataPoints.Clear();

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);


            var last7DaysTransaction = dataAccessLayer.CalculateFinancialSummaryForLast7Days(currentWalletID);

            foreach (var entry in last7DaysTransaction)
            {
                string dateText = entry.Key.Date == DateTime.Today ? "Today" :
                                  entry.Key.Date == DateTime.Today.AddDays(-1) ? "Yesterday" :
                                  entry.Key.ToString("MMMM d");

                gunaBarDataset1.DataPoints.Add(dateText, entry.Value.totalIncome); // totalIncome
                gunaBarDataset2.DataPoints.Add(dateText, entry.Value.totalExpenses); // totalExpenses
                gunaBarDataset3.DataPoints.Add(dateText, entry.Value.totalSavings); // totalSavings
            }

            barChart1.Refresh();
        }










        private void EditPictureBox_Click(object sender, EventArgs e)
        {

        }





        private void Wallet_uc_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void selectWalletComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateWalletDisplay();
            PopulateGunaBarDataSet();
            LoadTransactions();

        }


        private void editWalletPictureBox_Click(object sender, EventArgs e)
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            Wallet wallet = _dataAccessLayer.GetWalletById(currentWalletID);
            if (wallet != null)
            {
                // Open an edit form and pass the wallet object
                EditWallet editForm = new EditWallet(wallet);
                editForm.ShowDialog();
                UpdateWalletDisplay();
                PopulateWalletsComboBox();
            }
            else
            {
                MessageBox.Show("Wallet not found.");
            }
        }

        private void deleteWalletPictureBox_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this wallet?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                bool success = _dataAccessLayer.DeleteWallet(currentWalletID);
                if (success)
                {
                    MessageBox.Show("Wallet deleted successfully.");
                    UpdateWalletDisplay(); // Refresh the display
                    PopulateWalletsComboBox();
                }
                else
                {
                    MessageBox.Show("Failed to delete wallet.");
                }
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddWallet addWallet = new AddWallet();
            addWallet.ShowDialog();
        }

        private void walletLabel2_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void walletTypeLabel_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
