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
using Guna.Charts;
using Guna.Charts.WinForms;
using Guna.Charts.Interfaces;
using Guna.UI2.WinForms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace Wallet_Manager.Forms
{
    public partial class Budget1_uc : UserControl
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

        private Guna2CustomGradientPanel[] recordPanels;
        private Label[] spentLabels;
        private Label[] dateLabels;
        private Label[] percentageLabels;
        private Guna2ProgressBar[] progressBar;

        private Budget currentBudget;
        private int currentRecordPage = 1;
        private int recordsPerPage = 4; // Adjust as needed
        private int actualRecordsCount = 0;


        public Budget1_uc()
        {
            InitializeComponent();
            initializeControlArrays();
            PopulateBudgetComboBox();
            UpdateUIVisibility();

            GlobalEvents.TransactionUpdated += UpdateUIVisibility;

            GlobalEvents.TransactionUpdated += PopulateBudgetComboBox;

            GlobalEvents.BudgetUpdated += PopulateBudgetComboBox;
            GlobalEvents.BudgetUpdated += updateBudgetUI;




        }
        private void UpdateUIVisibility()
        {
            bool hasActiveBudgets = budgetComboBox.Items.Count > 0;

            label9.Visible = hasActiveBudgets;
            label6.Visible = hasActiveBudgets;
            remainingBudgetLabel.Visible = hasActiveBudgets;
            spentBudgetLabel.Visible = hasActiveBudgets;
            dateLabel.Visible = hasActiveBudgets;
            paginationLabel.Visible = hasActiveBudgets;
            generalProgressBar.Visible = hasActiveBudgets;
            nonVisibleLabel1.Visible = !hasActiveBudgets;
            nonVisibleLabel2.Visible = !hasActiveBudgets;
            nonVisibleLabel3.Visible = !hasActiveBudgets;
            nonVisibleLabel4.Visible = !hasActiveBudgets;
            doughnutCategoryChart.Visible = hasActiveBudgets;
            splineChart.Visible = hasActiveBudgets;
        }





        public void initializeControlArrays()
        {
            recordPanels = new Guna2CustomGradientPanel[] { rpanel1, rpanel2, rpanel3, rpanel4 };
            spentLabels = new Label[] { spentLabel1, spentLabel2, spentLabel3, spentLabel4 };
            dateLabels = new Label[] { dateLabel1, dateLabel2, dateLabel3, dateLabel4 };
            percentageLabels = new Label[] { percentageLabel1, percentageLabel2, percentageLabel3, percentageLabel4 };
            progressBar = new Guna2ProgressBar[] { progressBar1, progressBar2, progressBar3, progressBar4 };

        }


        public void PopulatePanels(Budget budget)
        {
            foreach (var panel in recordPanels)
            {
                panel.Visible = false;
            }

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var dailyAmounts = dataAccessLayer.GetDailyExpensesUnderBudget(budget).ToList();
            dailyAmounts.Reverse();

            actualRecordsCount = dailyAmounts.Count;
            int totalPages = (int)Math.Ceiling((double)actualRecordsCount / recordsPerPage);
            UpdatePaginationLabel();

            int start = (currentRecordPage - 1) * recordsPerPage;
            int end = Math.Min(start + recordsPerPage, actualRecordsCount);

            int panelIndex = 0;
            for (int i = start; i < end; i++, panelIndex++)
            {
                var entry = dailyAmounts[i];
                dateLabels[panelIndex].Text = FormatDate(entry.Key);
                spentLabels[panelIndex].Text = $"{entry.Value:C2}";
                float totalBudget = budget.TotalAmount;
                float percentageSpent = (entry.Value / totalBudget) * 100;
                progressBar[panelIndex].Value = Math.Min((int)percentageSpent, 100);
                percentageLabels[panelIndex].Text = $"{percentageSpent:F2}% of total budget";
                recordPanels[panelIndex].Visible = true;
            }

            for (int j = panelIndex; j < recordPanels.Length; j++)
            {
                recordPanels[j].Visible = false;
            }
        }

        private string FormatDate(DateTime date)
        {
            if (date.Date == DateTime.Today)
                return "Today";
            else if (date.Date == DateTime.Today.AddDays(-1))
                return "Yesterday";
            else
                return date.ToString("yyyy-MM-dd");
        }

        public void UpdateProgressBar(Budget budget)
        {
            generalProgressBar.Maximum = (int)(budget.TotalAmount);
            float totalSpent = ComputeTotalSpendForBudget(budget);
            float remainingBudget = budget.TotalAmount - totalSpent;
            int progressBarValue = (int)(totalSpent);
            generalProgressBar.Value = progressBarValue;

            if (progressBarValue > generalProgressBar.Maximum)
            {
                progressBarValue = generalProgressBar.Maximum;
            }
            else if (progressBarValue < generalProgressBar.Minimum)
            {
                progressBarValue = generalProgressBar.Minimum;
            }

            

            if (totalSpent > budget.TotalAmount)
            {
                overSpentLabel.Visible = true;
                remainingBudget = remainingBudget * -1;
                remainingBudgetLabel.Text = $"Overspent by {remainingBudget:C}";
                generalProgressBar.ProgressColor = Color.Red;
                generalProgressBar.ProgressColor2 = Color.Red;
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
            
            spentBudgetLabel.Text = $"{totalSpent:C} Spent";
        }


        private void UpdatePaginationLabel()
        {
            int totalPages = (int)Math.Ceiling((double)actualRecordsCount / recordsPerPage);
            paginationLabel.Text = $"Page {currentRecordPage} of {totalPages}";
        }




        public float ComputeTotalSpendForBudget(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            List<Transaction> transactions = dataAccessLayer.GetTransactionsByCategoryAndDate(budget.CategoryIds, budget.StartDate, budget.EndDate);
            float totalSpend = 0;

            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType.ToLower() == "expense")
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


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentRecordPage = 1;
            if (budgetComboBox.SelectedItem is Budget selectedBudget)
            {
                currentBudget = selectedBudget;
                actualRecordsCount = (selectedBudget.EndDate - selectedBudget.StartDate).Days + 1;
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                selectedBudget.CategoryIds = dataAccessLayer.GetCategoryIdsByBudgetId(selectedBudget.BudgetID);
                UpdateProgressBar(selectedBudget);
                PopulateDoughnutChart(selectedBudget);
                PopulateGunaSplineChart(selectedBudget);
                PopulatePanels(selectedBudget);
                dateLabel.Text = $"{selectedBudget.StartDate:MMMM d} - {selectedBudget.EndDate:MMMM d}";
                UpdateUIVisibility();
                
            }
        }

        private void updateBudgetUI()
        {
            if (budgetComboBox.SelectedItem != currentBudget)
            {
                budgetComboBox.SelectedItem = currentBudget;
                guna2ComboBox1_SelectedIndexChanged(budgetComboBox, EventArgs.Empty); 
            }
        }


        private void PopulateDoughnutChart(Budget budget)
        {
            List<int> categoryIds = budget.CategoryIds;
            DateTime startDate = budget.StartDate;
            DateTime endDate = budget.EndDate;

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var categoryExpenses = dataAccessLayer.GetCategoryExpensesByDate(categoryIds, startDate, endDate);

            doughnutCategoryChart.Series.Clear();
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Expenses",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut
            };

            foreach (var expense in categoryExpenses)
            {
                int categoryId = expense.Key;
                string categoryName = dataAccessLayer.GetCategoryNameById(categoryId);
                series.Points.AddXY(categoryName, expense.Value);
            }

            doughnutCategoryChart.Series.Add(series);
            doughnutCategoryChart.Series["Expenses"]["PieLabelStyle"] = "Disabled";
            series.IsValueShownAsLabel = false; 
            doughnutCategoryChart.Legends[0].Font = new Font("Segoe UI", 8, FontStyle.Bold); 
            doughnutCategoryChart.Legends[0].Enabled = true; 
            doughnutCategoryChart.ChartAreas[0].RecalculateAxesScale();
            doughnutCategoryChart.Invalidate();
        }

  
        private void PopulateGunaSplineChart(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var expensesData = dataAccessLayer.GetDailyExpensesUnderBudget(budget);

            splineDataset1.DataPoints.Clear();

            foreach (var item in expensesData)
            {
                splineDataset1.DataPoints.Add(item.Key.ToString("MMMM d"), item.Value);
            }
            splineChart.Refresh(); 
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (currentRecordPage * recordsPerPage < actualRecordsCount)
            {
                currentRecordPage++;
                PopulatePanels(currentBudget);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (currentRecordPage > 1)
            {
                currentRecordPage--;
                PopulatePanels(currentBudget);
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddBudget budget = new AddBudget();
            budget.ShowDialog();

        }


        private void editBudgetLabel_Click(object sender, EventArgs e)
        {
            EditBudget editBudget = new EditBudget(currentBudget);
            editBudget.ShowDialog();
        }

        private void deleteBudgetLabel_Click(object sender, EventArgs e)
        {
            if (currentBudget != null)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this budget?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                    SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                    dataAccessLayer.DeleteBudget(currentBudget.BudgetID);
                    MessageBox.Show("Budget deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GlobalEvents.onBudgetUpdated();
                }
            }
            else
            {
                MessageBox.Show("No budget selected to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Budget1_uc_Load(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
    }
}
