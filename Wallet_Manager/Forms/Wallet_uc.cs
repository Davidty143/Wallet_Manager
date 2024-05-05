using Guna.Charts.WinForms;
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
            GlobalEvents.TransactionUpdated += PopulateWalletsComboBox;

            editPictureBox.Click += editWalletPictureBox_Click;
            deletePictureBox.Click += deleteWalletPictureBox_Click;


        }

        public static void SetDoubleBuffering(Control control, bool value)
        {
            Type controlType = control.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi?.SetValue(control, value, null);

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
                spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C");
                savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C"); 
            }
        }




        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            var walletBindingList = wallets.Select(wallet => new
            {
                Text = wallet.WalletName,
                Value = wallet.WalletID
            }).ToList();

            walletBindingList.Insert(0, new { Text = "All", Value = 0 });

            selectWalletComboBox.DisplayMember = "Text";
            selectWalletComboBox.ValueMember = "Value";
            selectWalletComboBox.DataSource = walletBindingList;
        }



        private void UpdateWalletDisplay()
        {
            if (!int.TryParse(selectWalletComboBox.SelectedValue?.ToString(), out int walletId))
            {
                MessageBox.Show("Selected wallet ID is invalid.");
                return;
            }

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            Wallet wallet;
            if (walletId == 0) 
            {
                wallet = _dataAccessLayer.GetTotalBalancesForAllWallets();
                walletTypeLabel.Visible = false;
            }
            else 
            {
                wallet = _dataAccessLayer.GetWalletById(walletId);
                walletTypeLabel.Visible = true;
            }

            if (wallet != null)
            {
                currentWalletID = wallet.WalletID;
                walletNameLabel.Text = wallet.WalletName ?? "N/A";
                walletTypeLabel.Text = wallet.WalletType ?? "N/A";
                spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C");
                savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C");
                totalBalanceLabel.Text = (wallet.SavingsMoney + wallet.SpendingMoney).ToString("C");
            }
            else
            {
                MessageBox.Show("Wallet not found.");
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
            categoryImages = new Image[19];
            for (int i = 0; i < categoryImages.Length; i++)
            {
                string imageName = (i + 1).ToString();
                categoryImages[i] = (Image)Properties.Resources.ResourceManager.GetObject(imageName);

                if (categoryImages[i] == null)
                {
                    Console.WriteLine($"Image '{imageName}.png' not found for category ID: {i + 1}, using default image.");
                    categoryImages[i] = Properties.Resources.button_budget_active; 
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

            List<Transaction> transactions;
            if (currentWalletID == 0)
            {
                transactions = _dataAccessLayer.GetLastTransactionsForAllWallets(3);
            }
            else
            {
                transactions = _dataAccessLayer.GetTransactionsByWalletId(currentWalletID, 3);
            }

            int numberOfTransactionsToShow = Math.Min(3, transactions.Count);

            for (int i = 0; i < transactionPanels.Length; i++)
            {
                if (i < numberOfTransactionsToShow)
                {
                    var transaction = transactions[i];
                    transactionPanels[i].Visible = true;
                    descriptionLabels[i].Text = transaction.Description;
                    categoryLabels[i].Text = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    transactionTypeLabels[i].Text = transaction.TransactionType;
                    amountLabels[i].Text = $"₱ {transaction.Amount}";
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID) + " - " + transaction.WalletCategory.ToString();
                    dateLabels[i].Text = transaction.Date.ToString("d");

                    if (categoryPictureBoxes[i] == null)
                    {
                        Console.WriteLine("PictureBox at index " + i + " is null.");
                        continue; 
                    }

                    int imageIndex = transaction.CategoryID - 1;
                    if (imageIndex < 0 || categoryImages[imageIndex] == null)
                    {
                        Console.WriteLine("Invalid or missing image for Category ID: " + transaction.CategoryID);
                        categoryPictureBoxes[i].Image = Properties.Resources.button_budget_active;
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

                gunaBarDataset1.DataPoints.Add(dateText, entry.Value.totalIncome);
                gunaBarDataset2.DataPoints.Add(dateText, entry.Value.totalExpenses);
                gunaBarDataset3.DataPoints.Add(dateText, entry.Value.totalSavings); 
            }

            barChart1.Refresh();
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
                    UpdateWalletDisplay();
                    PopulateWalletsComboBox();
                }
                else
                {
                    MessageBox.Show("Failed to delete wallet.");
                }
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddWallet addWallet = new AddWallet();
            addWallet.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllAnalytics();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllTransactions();
        }

        private void walletLabel3_Click(object sender, EventArgs e)
        {

        }

        private void amountLabel1_Click(object sender, EventArgs e)
        {

        }

        private void walletLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Wallet_uc_Load(object sender, EventArgs e)
        {

        }
    }
}
