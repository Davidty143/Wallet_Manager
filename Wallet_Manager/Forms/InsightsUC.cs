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
            PopulateGunaBarDataSet();
            PopulateBarChart();
        }

        private void PopulateBarChart()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);
            var financialData = dataAccessLayer.GetFinancialSummaryForLast7Days();

            foreach (var entry in financialData)
            {
                chart1.Series["Income"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.income);
                chart1.Series["Expense"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.spending);
                chart1.Series["Savings"].Points.AddXY(entry.Key.ToShortDateString(), entry.Value.savings);
            }
        }


        private void PopulateGunaBarDataSet()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";

            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(connectionString);

            var financialData = dataAccessLayer.GetFinancialSummaryForLast7Days();

            gunaBarDataset1.DataPoints.Clear();

            foreach (var entry in financialData)
            {
                gunaBarDataset1.DataPoints.Add("", entry.Value.income);
                gunaBarDataset2.DataPoints.Add("", entry.Value.spending);
                gunaBarDataset3.DataPoints.Add(DateTime.Today.ToString(),  entry.Value.savings);
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
    }
}
