using Guna.Charts.Interfaces;
using Guna.Charts.WinForms;
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
using Wallet_Manager.Enums;

namespace Wallet_Manager.Forms
{
    public partial class InsightsUC : UserControl
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

        public InsightsUC()
        {
            InitializeComponent();
            SetDoubleBuffering(this, true);
            PopulateComboBox();
            PopulateWalletsComboBox();
            guna2CustomGradientPanel1.Visible = true;


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

        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            // Convert wallets to a binding-friendly format
            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName}",
                Value = wallet.WalletID
            }).ToList();

            // Add an "All" option at the beginning of the list
            walletBindingList.Insert(0, new { Text = "All Wallets", Value = 0 });

            selecWalletComboBox.DisplayMember = "Text";
            selecWalletComboBox.ValueMember = "Value";
            selecWalletComboBox.DataSource = walletBindingList;

            selecWalletComboBox.SelectedIndex = 0;
        }



        private void PopulateComboBox()
        {
            // Assuming comboBoxPeriod is your ComboBox control
            timeFrameComboBox.Items.Clear();  // Clear existing items

            // Add time period options to the ComboBox
            timeFrameComboBox.Items.Add("1 Week");
            timeFrameComboBox.Items.Add("1 Month");
            timeFrameComboBox.Items.Add("1 Year");

            // Optionally set the default selected item
            timeFrameComboBox.SelectedIndex = 0;  // Selects the first item by default
        }


        private void PopulateGunaBarDataSet()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            string selectedPeriod = timeFrameComboBox.SelectedItem.ToString();
            int selectedWalletId = Convert.ToInt32(selecWalletComboBox.SelectedValue);
            dynamic financialData;

            switch (selectedPeriod)
            {
                case "1 Week":
                    financialData = _dataAccessLayer.CalculateFinancialSummaryForLast7Days(selectedWalletId);
                    break;
                case "1 Month":
                    financialData = _dataAccessLayer.CalculateFinancialSummaryForLastMonth(selectedWalletId);
                    break;
                case "1 Year":
                    financialData = _dataAccessLayer.CalculateFinancialSummaryForLastYear(selectedWalletId);
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
            if (period == "1 Week")
                return date.ToString("MMMM d");
            else if (period == "1 Month")
                return date.ToString("MMMM d");
            else if (period == "1 Year")
                return date.ToString("MMM");
            return "";
        }




        private void PopulatePieChart()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            // Determine the selected time frame from the combo box
            string selectedPeriod = timeFrameComboBox.SelectedItem.ToString();
            int selectedWalletId = Convert.ToInt32(selecWalletComboBox.SelectedValue);
            Dictionary<string, float> expenses;

            switch (selectedPeriod)
            {
                case "1 Week":
                    expenses = _dataAccessLayer.GetExpenseCategoriesLast7Days(selectedWalletId);
                    break;
                case "1 Month":
                    expenses = _dataAccessLayer.GetExpenseCategoriesLast30Days(selectedWalletId);
                    break;
                case "1 Year":
                    expenses = _dataAccessLayer.GetExpenseCategoriesLastYear(selectedWalletId); 
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


        public void PopulateSpLineChart()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            // Determine the selected time frame from the combo box
            string selectedPeriod = timeFrameComboBox.SelectedItem.ToString();
            int selectedWalletId = Convert.ToInt32(selecWalletComboBox.SelectedValue);
            dynamic netWorth;

            switch (selectedPeriod)
            {
                case "1 Week":
                    netWorth = _dataAccessLayer.CalculateNetWorthOver7Days(selectedWalletId);
                    break;
                case "1 Month":
                    netWorth = _dataAccessLayer.CalculateNetWorthOver1Month(selectedWalletId);
                    break;
                case "1 Year":
                    netWorth = _dataAccessLayer.CalculateNetWorthOver12Months(selectedWalletId);
                    break;
                default:
                    netWorth = new Dictionary<string, float>(); // Default to empty if no valid selection
                    break;
            }

            splineAreaDataset1.DataPoints.Clear(); // Clear existing data points before adding new ones

            foreach (var net in netWorth)
            {
                // Format the date as a string if necessary, depending on how the chart component expects it
                string label = FormatLabel(net.Key, selectedPeriod);
                splineAreaDataset1.DataPoints.Add(label, net.Value);
            }
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
            PopulateSpLineChart();
        }

        private void pieChart1_Load(object sender, EventArgs e)
        {

        }

        private void splineAreaChart1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Show the relevant panel
            guna2CustomGradientPanel1.Visible = true;
            guna2CustomGradientPanel2.Visible = false;
            guna2CustomGradientPanel3.Visible = false;
            financialOverviewLabel.ForeColor = Color.Black;
            expenseCategoryLabel.ForeColor = Color.Gray;
            netWorthLabel.ForeColor = Color.Gray;

            // Load data specific to this panel
            PopulateGunaBarDataSet();
        }

        private void guna2CustomGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SpendingCategoryLabel_Click(object sender, EventArgs e)
        {
            // Show the relevant panel
            guna2CustomGradientPanel1.Visible = false;
            guna2CustomGradientPanel2.Visible = true;
            guna2CustomGradientPanel3.Visible = false;
            expenseCategoryLabel.ForeColor = Color.Black;
            financialOverviewLabel.ForeColor = Color.Gray;
            netWorthLabel.ForeColor = Color.Gray;

            // Load data specific to this panel
            PopulatePieChart();

        }

        private void netWorthLabel_Click(object sender, EventArgs e)
        {
            // Show only the relevant panel
            guna2CustomGradientPanel1.Visible = false;
            guna2CustomGradientPanel2.Visible = false;
            guna2CustomGradientPanel3.Visible = true;
            expenseCategoryLabel.ForeColor = Color.Gray;
            financialOverviewLabel.ForeColor = Color.Gray;
            netWorthLabel.ForeColor = Color.Black;

            // Load data specific to this panel
            PopulateSpLineChart();
        }

        private void selecWalletComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2CustomGradientPanel1.Visible)
                PopulateGunaBarDataSet();
            else if (guna2CustomGradientPanel2.Visible)
                PopulatePieChart();
            else if (guna2CustomGradientPanel3.Visible)
                PopulateSpLineChart();
        }
    }
}
