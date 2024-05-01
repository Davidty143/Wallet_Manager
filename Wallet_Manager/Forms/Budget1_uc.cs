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
            UpdateUIVisibility();
            PopulateBudgetComboBox();
            






        }
        private void UpdateUIVisibility()
        {
            bool hasActiveBudgets = budgetComboBox.Items.Count > 0;

            // Set visibility of all related UI components based on the presence of active budgets
            label9.Visible = hasActiveBudgets;
            label2.Visible = hasActiveBudgets;
            label4.Visible = hasActiveBudgets;
            label5.Visible = hasActiveBudgets;
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
            doughnutChart1.Visible = hasActiveBudgets;
            splineChart.Visible = hasActiveBudgets;

            // Do not set visibility for panels and progress bars here
            // Let PopulatePanels method handle the visibility of individual panels
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
            // Set all panels to not visible at the beginning
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

            // This index tracks which panel is being populated
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
                recordPanels[panelIndex].Visible = true; // Only make the panel visible if there's data to display
                if (entry.Value > totalBudget)
                {
                    progressBar[panelIndex].ProgressColor = Color.Red;
                    progressBar[panelIndex].ProgressColor2 = Color.Red; 
                }

            }

            // Hide any unused panels beyond the last populated one
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

            // Set the progress bar value
            generalProgressBar.Value = progressBarValue;

            // Optionally, update a label to show the numeric value or percentage
            remainingBudgetLabel.Text = $"{remainingBudget:C}".Insert(1, " ");
            spentBudgetLabel.Text = $"{totalSpent:C}".Insert(1, " ");

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


        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void Budget1_uc_Load(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (budgetComboBox.SelectedItem is Budget selectedBudget)
            {
                currentBudget = selectedBudget;
                actualRecordsCount = (selectedBudget.EndDate - selectedBudget.StartDate).Days + 1;
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                selectedBudget.CategoryIds = dataAccessLayer.GetCategoryIdsByBudgetId(selectedBudget.BudgetID);
                UpdateProgressBar(selectedBudget);
                //PopulateDoughnutChart(selectedBudget);
                // PopulateSplineChart(selectedBudget);
                PopulateGunaDoughnutChart(selectedBudget);
                PopulateGunaSplineChart(selectedBudget);
                PopulatePanels(selectedBudget);
                dateLabel.Text = $"{selectedBudget.StartDate:MMMM d} - {selectedBudget.EndDate:MMMM d}";
                UpdateUIVisibility();


            }
        }

        private void PopulateGunaDoughnutChart(Budget budget)
        {
            // Use the category IDs and date range from the budget object
            List<int> categoryIds = budget.CategoryIds;
            DateTime startDate = budget.StartDate;
            DateTime endDate = budget.EndDate;

            // Create an instance of SqlDataAccessLayer and get the expenses data
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var categoryExpenses = dataAccessLayer.GetCategoryExpensesByDate(categoryIds, startDate, endDate);

            doughnutDataset1.DataPoints.Clear();

            foreach (var expense in categoryExpenses)
            {
                int categoryId = expense.Key;
                string categoryName = dataAccessLayer.GetCategoryNameById(categoryId);
                doughnutDataset1.DataPoints.Add(categoryName, expense.Value);
                doughnutChart1.Refresh();
            }

            // Refresh the chart to display the new data
        }




        /*
        private void PopulateDoughnutChart(Budget budget)
        {
            // Use the category IDs and date range from the budget object
            List<int> categoryIds = budget.CategoryIds;
            DateTime startDate = budget.StartDate;
            DateTime endDate = budget.EndDate;

            // Create an instance of SqlDataAccessLayer and get the expenses data
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var categoryExpenses = dataAccessLayer.GetCategoryExpensesByDate(categoryIds, startDate, endDate);

            // Assuming 'doughnutCategoryChart' is your System.Windows.Forms.DataVisualization.Charting.Chart control
            doughnutCategoryChart.Series.Clear();
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Expenses",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut
            };

            // Add data points to the series
            foreach (var expense in categoryExpenses)
            {
                int categoryId = expense.Key;
                string categoryName = dataAccessLayer.GetCategoryNameById(categoryId);
                series.Points.AddXY(categoryName, expense.Value);
            }

            // Add the series to the chart
            doughnutCategoryChart.Series.Add(series);

            // Customize the chart: Hide labels around the doughnut, show them in the legend
            doughnutCategoryChart.Series["Expenses"]["PieLabelStyle"] = "Disabled"; // Disable the labels on the slices
            series.IsValueShownAsLabel = false; // Ensure values are not shown as labels on the chart
            doughnutCategoryChart.Legends[0].Font = new Font("Segoe UI", 8, FontStyle.Bold); // Customize the legend font
           // doughnutCategoryChart.Legends[0].ForeColor = Color.DarkGray; // Customize the legend font color
            doughnutCategoryChart.Legends[0].Enabled = true; // Ensure the legend is enabled

            // Optional: Further customize the chart, e.g., colors, labels, etc.
            doughnutCategoryChart.ChartAreas[0].RecalculateAxesScale();

            // Refresh the chart to display the new data
            doughnutCategoryChart.Invalidate();
        }

        */


        /*
         * 
         * 

        private void PopulateSplineChart(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var expensesData = dataAccessLayer.GetDailyExpensesUnderBudget(budget);

            splineDailyExpenseChart.Series.Clear();
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Spending Trend",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline,
                LabelFormat = $"{0:C}".Insert(1, " "),
                BorderWidth = 2, // Increase the line thicknes
                BorderColor = Color.FromArgb(151, 91, 206) // Change the line color
            };

            foreach (var item in expensesData)
            {
                // Format the date as "Month Day"
                string formattedDate = item.Key.ToString("MMMM d");
                series.Points.AddXY(formattedDate, item.Value);
                series.Points.Last().Label = $"{item.Value:C}".Insert(1, " ");
        }
            splineDailyExpenseChart.ChartAreas[0].AxisY.LabelStyle.Format = $"{0:C}".Insert(1, " ");
            splineDailyExpenseChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            splineDailyExpenseChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            splineDailyExpenseChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            splineDailyExpenseChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            splineDailyExpenseChart.Series.Add(series);
            splineDailyExpenseChart.Invalidate(); // Refresh the chart
        }
        */

        private void PopulateGunaSplineChart(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var expensesData = dataAccessLayer.GetDailyExpensesUnderBudget(budget);

            // Clear existing data points before adding new ones
            splineDataset1.DataPoints.Clear();

            foreach (var item in expensesData)
            {
                splineDataset1.DataPoints.Add(item.Key.ToString("MMMM d"), item.Value);
            }
            splineChart.Refresh(); // Refresh the chart to display the new data
        }







        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void doughnutCategoryChart_Click(object sender, EventArgs e)
        {

        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {
            
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void generalProgressBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (currentRecordPage * recordsPerPage < actualRecordsCount)
            {
                currentRecordPage++;
                PopulatePanels(currentBudget); // Assuming currentBudget is accessible
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

        private void paginationLabel_Click(object sender, EventArgs e)
        {

        }

        private void splineDailyExpenseChart_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rpanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splineChart_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddBudget budget = new AddBudget();
            budget.ShowDialog();

        }
    }
}
