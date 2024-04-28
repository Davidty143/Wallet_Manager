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
        public InsightsUC()
        {
            InitializeComponent();
            PopulateComboBox();
            PopulatePieChart();



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




        private void PopulateGunaBarDataSet()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            // Assuming comboBoxPeriod is your ComboBox control with options like "Last 7 Days", "Last Month", "Last Year"
            string selectedPeriod = timeFrame.SelectedItem.ToString();
            dynamic financialData;

            switch (selectedPeriod)
            {
                case "Last 7 Days":
                    financialData = dataAccessLayer.CalculateFinancialSummaryForLast7Days();
                    break;
                case "Last Month":
                    financialData = dataAccessLayer.CalculateFinancialSummaryForLastMonth();
                    break;
                case "Last Year":
                    financialData = dataAccessLayer.CalculateFinancialSummaryForLastYear();
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
                string label = "";
                if (selectedPeriod == "Last 7 Days")
                {
                    label = entry.Key.ToString("MMMM d"); // "September 1"
                }
                else if (selectedPeriod == "Last Month")
                {
                    label = entry.Key.Day == 1 ? entry.Key.ToString("MMMM d") : entry.Key.Day.ToString(); // "September 1", "2", "3", ...
                }
                else if (selectedPeriod == "Last Year")
                {
                    label = entry.Key.ToString("MMM"); // "Jan", "Feb", "Mar", ...
                }

                // Access tuple elements by Item1, Item2, Item3
                gunaBarDataset1.DataPoints.Add(label, entry.Value.Item1); // totalIncome
                gunaBarDataset2.DataPoints.Add(label, entry.Value.Item2); // totalExpenses
                gunaBarDataset3.DataPoints.Add(label, entry.Value.Item3); // totalSavings
            }

            // Refresh the chart to update the display
            barChart1.Refresh();
        }

        private void PopulatePieChart()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            // Fetch the data
            Dictionary<string, float> expenses = dataAccessLayer.GetExpenseCategoriesLast7Days();

            // Clear existing series and add new one
            pieChart2.Series.Clear();
            Series series = new Series("Expenses")
            {
                ChartType = SeriesChartType.Pie
            };
            pieChart2.Series.Add(series);

            // Populate the series with data
            foreach (var expense in expenses)
            {
                pieChart2.Series["Expenses"].Points.AddXY(expense.Key, expense.Value);
            }

            // Optional: Configure the chart appearance
            pieChart2.Series["Expenses"]["PieLabelStyle"] = "Inside";
            pieChart2.Series["Expenses"].Label = "#PERCENT{P1} - #VALX";
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
        }
    }
}
