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
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
