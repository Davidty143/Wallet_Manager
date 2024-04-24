﻿using System;
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

namespace Wallet_Manager.Forms
{
    public partial class Budget1_uc : UserControl
    {
        public Budget1_uc()
        {
            InitializeComponent();
            PopulateBudgetComboBox();
            
        }

        public void UpdateProgressBar(Budget budget)
        {
            // Calculate the amount spent
            float totalSpent = ComputeTotalSpendForBudget(budget);


            // Calculate the remaining budget
            float remainingBudget = budget.TotalAmount - totalSpent;

            // Calculate the progress bar value
            // Ensure the value is within the bounds of the progress bar's minimum and maximum
            int progressBarValue = (int)(totalSpent);  // Convert total spent to an integer
            if (progressBarValue > progressBar1.Maximum)
            {
                progressBarValue = progressBar1.Maximum;
            }
            else if (progressBarValue < progressBar1.Minimum)
            {
                progressBarValue = progressBar1.Minimum;
            }

            // Set the progress bar value
            generalProgressBar.Value = progressBarValue;

            // Optionally, update a label to show the numeric value or percentage
            remainingBudgetLabel.Text = $"{remainingBudget:C}".Insert(1, " ");
            spentBudgetLabel.Text = $"{totalSpent:C}".Insert(1, " ");

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
            budgetComboBox.DataSource = budgets;
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
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
                selectedBudget.CategoryIds = dataAccessLayer.GetCategoryIdsByBudgetId(selectedBudget.BudgetID);
                UpdateProgressBar(selectedBudget);
                PopulateDoughnutChart(selectedBudget);
                PopulateSplineChart(selectedBudget);
                
            }
        }
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




        private void PopulateSplineChart(Budget budget)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var expensesData = dataAccessLayer.GetBudgetDailyExpenses(budget.BudgetID, budget.StartDate, budget.EndDate);

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
    }
}
