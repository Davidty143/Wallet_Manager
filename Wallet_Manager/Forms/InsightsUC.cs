﻿using Guna.Charts.Interfaces;
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
           PopulateGunaBarDataSet();
            PopulateBarChart();
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


        private void PopulateBarChart()
        {

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var financialData = dataAccessLayer.CalculateFinancialSummaryForLast7Days();

            foreach (var entry in financialData)
            {
                chart1.Series["Income"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.totalIncome);
                chart1.Series["Expense"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.totalExpenses);
                chart1.Series["Savings"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.totalSavings);
            }
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

                gunaBarDataset1.DataPoints.Add(label, entry.Value.totalIncome);
                gunaBarDataset2.DataPoints.Add(label, entry.Value.totalExpenses);
                gunaBarDataset3.DataPoints.Add(label, entry.Value.totalSavings);
            }

            // Refresh the chart to update the display
            barChart1.Refresh();
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

        }
    }
}
