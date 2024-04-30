using Guna.Charts.WinForms;
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
using System.Windows.Forms.DataVisualization.Charting;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class DashboardUC : UserControl
    {
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
        public DashboardUC()
        {
            InitializeComponent();
            InitializeControlArrays();
            LoadCategoryImages();
            LoadTransactions();
            UpdateSavingsLabel();
            UpdateExpenseLabel();
            UpdateMostUsedWalletDisplay();
            PopulateGunaBarDataSet();
           
            //SetupChart();

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

            transactions = _dataAccessLayer.GetLatestThreeTransactions();
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




        private void UpdateExpenseLabel()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            float totalExpenses = _dataAccessLayer.GetTotalExpensesForToday();
            expenseTodayLabel.Text = $"{totalExpenses:C}".Insert(1, " ");
        }


        private void UpdateSavingsLabel()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            float totalSavings = _dataAccessLayer.CalculateTotalSavingsForToday();

            // Assuming labelTotalSavings is the Label control on your form
            savingsTodayLabel.Text = $"{totalSavings:C}".Insert(1, " ");
        }


        private void UpdateMostUsedWalletDisplay()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            Wallet mostUsedWallet = _dataAccessLayer.GetMostUsedWallet();
            if (mostUsedWallet != null)
            {
                mostUsedWalletLabel.Text = $"{mostUsedWallet.WalletName}";
                mostUsedWalletTypeLabel.Text = $"{mostUsedWallet.WalletType}";
                mostUsedSpendingLabel.Text = $"{mostUsedWallet.SpendingMoney:C}".Insert(1, " ");
                mostUsedSavingsLabel.Text = $"{mostUsedWallet.SavingsMoney:C}".Insert(1, " ");
                mostUsedTotalAmountLabel.Text = $"{mostUsedWallet.SavingsMoney + mostUsedWallet.SpendingMoney:C}".Insert(1, " ");
            }
            else
            {
                mostUsedWalletLabel.Text = "Most Used Wallet: Not Available";
                mostUsedWalletTypeLabel.Text = "Type: N/A";
                mostUsedSpendingLabel.Text = "Spending Money: N/A";
                mostUsedSavingsLabel.Text = "Savings Money: N/A";
            }
        }

        private void PopulateGunaBarDataSet()
        {
            gunaBarDataset1.DataPoints.Clear();
            gunaBarDataset2.DataPoints.Clear();
            gunaBarDataset3.DataPoints.Clear();


            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var last7DaysTransaction = dataAccessLayer.CalculateFinancialSummaryForLast7Days(0);

            foreach (var entry in last7DaysTransaction)
            {
                string dateText = entry.Key.Date == DateTime.Today ? "Today" :
                                  entry.Key.Date == DateTime.Today.AddDays(-1) ? "Yesterday" :
                                  entry.Key.ToString("MMMM d");

                gunaBarDataset1.DataPoints.Add(dateText, entry.Value.Item1); // totalIncome
                gunaBarDataset2.DataPoints.Add(dateText, entry.Value.Item2); // totalExpenses
                gunaBarDataset3.DataPoints.Add(dateText, entry.Value.Item3); // totalSavings
            }

            barChart1.Refresh();

        }

       





        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void DashboardUC_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            

           if (dashboardParent.transactionForm == null || dashboardParent.transactionForm.IsDisposed)
            {
                dashboardParent.transactionForm = new AddTransaction();
            }
            dashboardParent.transactionForm.Show();
            dashboardParent.transactionForm.BringToFront();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void spentBudgetLabel_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void recentTransactionPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            AddWallet addWallet = new AddWallet();
            addWallet.ShowDialog();
        }

        private void descriptionLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click_1(object sender, EventArgs e)
        {

        }
    }
}
