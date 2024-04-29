using Guna.Charts.Interfaces;
using Guna.Charts.WinForms;
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
    public partial class InsightsUC : UserControl
    {
        private Dictionary<string, float> expensesLast7Days;
        private Dictionary<string, float> expensesLastMonth;
        private Dictionary<string, float> expensesLastYear;
        dynamic financialDataWeek;
        dynamic financialDataMonth;
        dynamic financialDataYear;

        public InsightsUC()
        {
            InitializeComponent();
            FetchAllFinancialData();
            PopulateComboBox();
            
        }

        private void PopulateComboBox()
        {
            // Assuming comboBoxPeriod is your ComboBox control
            timeFrame.Items.Clear();  // Clear existing items

            // Add time period options to the ComboBox
            timeFrame.Items.Add("Last 7 Days");
            timeFrame.Items.Add("Last Month");
            timeFrame.Items.Add("Last Year");

            // Optionally set the default selected item
            timeFrame.SelectedIndex = 0;  // Selects the first item by default
        }



        private void FetchAllFinancialData()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            expensesLast7Days = dataAccessLayer.GetExpenseCategoriesLast7Days();
            expensesLastMonth = dataAccessLayer.GetExpenseCategoriesLast30Days();
            expensesLastYear = dataAccessLayer.GetExpenseCategoriesLastYear();
            financialDataWeek = dataAccessLayer.CalculateFinancialSummaryForLast7Days();
            financialDataMonth = dataAccessLayer.CalculateFinancialSummaryForLastMonth();
            financialDataYear = dataAccessLayer.CalculateFinancialSummaryForLastYear();
        }
        private void PopulateGunaBarDataSet()
        {
            string selectedPeriod = timeFrame.SelectedItem.ToString();
            dynamic financialData;

            switch (selectedPeriod)
            {
                case "Last 7 Days":
                    financialData = financialDataWeek;
                    break;
                case "Last Month":
                    financialData = financialDataMonth;
                    break;
                case "Last Year":
                    financialData = financialDataYear;
                    break;
                default:
                    financialData = new SortedDictionary<DateTime, (float, float, float)>();
                    break;
            }

            gunaBarDataset1.DataPoints.Clear();
            gunaBarDataset2.DataPoints.Clear();
            gunaBarDataset3.DataPoints.Clear();

            foreach (var entry in financialData)
            {
                string label = FormatLabel(entry.Key, selectedPeriod);
                gunaBarDataset1.DataPoints.Add(label, entry.Value.Item1); // totalIncome
                gunaBarDataset2.DataPoints.Add(label, entry.Value.Item2); // totalExpenses
                gunaBarDataset3.DataPoints.Add(label, entry.Value.Item3); // totalSavings
            }

            barChart1.Refresh();
        }

        private string FormatLabel(DateTime date, string period)
        {
            if (period == "Last 7 Days")
                return date.ToString("MMMM d");
            else if (period == "Last Month")
                return date.Day == 1 ? date.ToString("MMMM d") : date.Day.ToString();
            else if (period == "Last Year")
                return date.ToString("MMM");
            return "";
        }




        private void PopulatePieChart()
        {
            // Determine the selected time frame from the combo box
            string selectedPeriod = timeFrame.SelectedItem.ToString();
            Dictionary<string, float> expenses;

            switch (selectedPeriod)
            {
                case "Last 7 Days":
                    expenses = expensesLast7Days;
                    break;
                case "Last Month":
                    expenses = expensesLastMonth;
                    break;
                case "Last Year":
                    expenses = expensesLastYear;
                    break;
                default:
                    expenses = new Dictionary<string, float>(); // Default to empty if no valid selection
                    break;
            }

            // Clear existing data points
            gunaPieDataset1.DataPoints.Clear();

            // Populate the pie chart with the fetched data
            foreach (var expense in expenses)
            {
                gunaPieDataset1.Label = "Spent";
                gunaPieDataset1.DataPoints.Add(expense.Key, expense.Value);
            }

            // Refresh the chart to display the new data
            pieChart1.Refresh();
        }






        private void InsightsUC_Load(object sender, EventArgs e)
        {

        }

        private void barChart1_Load(object sender, EventArgs e)
        {

        }

        private void barChart1_Load_1(object sender, EventArgs e)
        {
            
        }

        private void timeFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
              PopulateGunaBarDataSet();
              PopulatePieChart();
        }

        private void pieChart1_Load(object sender, EventArgs e)
        {

        }
    }
}
