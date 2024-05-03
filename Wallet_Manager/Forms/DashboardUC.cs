using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Asn1.Mozilla;
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
    public partial class DashboardUC : UserControl
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
            SetDoubleBuffering(this, true);
            InitializeControlArrays();
            LoadCategoryImages();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint, true);
            this.UpdateStyles();



            
            LoadTransactions();
            UpdateSavingsLabel();
            UpdateExpenseLabel();
            UpdateMostUsedWalletDisplay();
            PopulateGunaBarDataSet();
            PopulateBudgetComboBox();


            GlobalEvents.TransactionUpdated += LoadTransactions;
            GlobalEvents.TransactionUpdated += UpdateSavingsLabel;
            GlobalEvents.TransactionUpdated += UpdateExpenseLabel;
            GlobalEvents.TransactionUpdated += UpdateMostUsedWalletDisplay;
            GlobalEvents.TransactionUpdated += PopulateGunaBarDataSet;
            GlobalEvents.TransactionUpdated += PopulateBudgetComboBox;
            GlobalEvents.BudgetUpdated += PopulateBudgetComboBox;
            GlobalEvents.BudgetUpdated += updateBudgetUI;
            //SetupChart();

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
                mostUsedWalletLabel.Text = "";
                mostUsedWalletTypeLabel.Text = "";
                mostUsedSpendingLabel.Text = "";
                mostUsedSavingsLabel.Text = "";
                mostUsedTotalAmountLabel.Text = "₱ 0.00";

            }
        }

        private  void PopulateGunaBarDataSet()
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

        public void UpdateProgressBar(Budget budget)
        {
            generalProgressBar.Maximum = (int)(budget.TotalAmount);
            // Calculate the amount spent
            float totalSpent = ComputeTotalSpendForBudget(budget);

            // Calculate the remaining budget
            float remainingBudget = budget.TotalAmount - totalSpent;

            // Calculate the progress bar value
            // Ensure the value is within the bounds of the progress bar's minimum and maximum
            int progressBarValue = (int)(totalSpent);  // Convert total spent to an integer
            if (progressBarValue > generalProgressBar.Maximum)
            {
                progressBarValue = generalProgressBar.Maximum;
            }
            else if (progressBarValue < generalProgressBar.Minimum)
            {
                progressBarValue = generalProgressBar.Minimum;
            }

            generalProgressBar.Value = progressBarValue;



            // Set the progress bar color based on budget comparison
            if (totalSpent > budget.TotalAmount)
            {
                overSpentLabel.Visible = true;
                generalProgressBar.ProgressColor = Color.Red;
                generalProgressBar.ProgressColor2 = Color.Red;
                remainingBudget = remainingBudget * -1;
                remainingBudgetLabel.Text = $"Overspent by {remainingBudget:C}";
            }
            else
            {
                generalProgressBar.ProgressColor = Color.LimeGreen;
                generalProgressBar.ProgressColor2 = Color.LimeGreen;
                overSpentLabel.Visible = false;
                remainingBudgetLabel.Text = $"{remainingBudget:C} Remaining";
            }

            if (totalSpent < budget.TotalAmount && totalSpent > (budget.TotalAmount * 0.7))
            {
                warningLabel.Visible = true;
            }

            else
            {
                warningLabel.Visible = false;
            }


            // Optionally, update a label to show the numeric value or percentage

            spentBudgetLabel.Text = $"{totalSpent:C} Spent";
        }

        public float ComputeTotalSpendForBudget(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            List<Transaction> transactions = dataAccessLayer.GetTransactionsByCategoryAndDate(budget.CategoryIds, budget.StartDate, budget.EndDate);
            float totalSpend = 0;

            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType.ToLower() == "expense") // Assuming 'expense' indicates money spent
                {
                    totalSpend += transaction.Amount;
                }
            }

            return totalSpend;
        }

        private void PopulateBudgetComboBox()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var budgets = dataAccessLayer.GetAllBudgets();

            // Filter to only include active budgets
            var activeBudgets = budgets.Where(b => b.IsActive).ToList();

            budgetComboBox.DataSource = activeBudgets;
            budgetComboBox.DisplayMember = "BudgetName";
            budgetComboBox.ValueMember = "BudgetID";
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllTransactions();

        }

        private void label22_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllWallets();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllAnalytics();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;
            dashboardParent.clickSeeAllBudgets();   
        }

        private void guna2CustomGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (budgetComboBox.SelectedItem is Budget selectedBudget)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                selectedBudget.CategoryIds = dataAccessLayer.GetCategoryIdsByBudgetId(selectedBudget.BudgetID);
                UpdateProgressBar(selectedBudget);
                dateLabel.Text = $"{selectedBudget.StartDate:MMMM d} - {selectedBudget.EndDate:MMMM d}";
                nameLabel.Text = selectedBudget.BudgetName;
            }
        }

        private void updateBudgetUI()
        {

                budgetComboBox.SelectedItem = budgetComboBox; // Update the selected item
                guna2ComboBox1_SelectedIndexChanged(budgetComboBox, EventArgs.Empty); // Manually invoke the handler if needed
        }


    }
}
