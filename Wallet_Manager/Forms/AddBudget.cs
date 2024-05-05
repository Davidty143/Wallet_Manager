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
using MySql.Data.MySqlClient;

namespace Wallet_Manager.Forms
{
    public partial class AddBudget : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        
        private List<Category> categoryList = new List<Category>();

        public AddBudget()
        {
            InitializeComponent();
            PopulateCategories();
            PopulatePeriodTypes();
            txtAmount.MaxLength = 7;
            txtPeriod.SelectedIndex = 0;
            UpdateDateVisibility();

          

            GlobalEvents.TransactionUpdated += PopulateCategories;
            GlobalEvents.TransactionUpdated += PopulatePeriodTypes;

            ClearForm();
        }


        private void PopulateCategories()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            DataTable categories = _dataAccessLayer.GetExpenseCategories();
            txtCategory.Items.Clear();
            categoryList.Clear();

            foreach (DataRow row in categories.Rows)
            {
                string categoryName = row["Name"].ToString();
                int categoryId = Convert.ToInt32(row["CategoryId"]);
                txtCategory.Items.Add(categoryName);
                categoryList.Add(new Category(categoryId, categoryName));
            }
        }


        private void PopulatePeriodTypes()
        {
            txtPeriod.Items.Clear();
                
            
            txtPeriod.Items.Add("Daily");
            txtPeriod.Items.Add("Weekly");
            txtPeriod.Items.Add("Monthly");
            txtPeriod.Items.Add("Custom");


            txtPeriod.SelectedIndex = 0; 
        }

        private void UpdateDateVisibility()
        {
            bool isCustom = txtPeriod.SelectedItem.ToString() == "Custom";
            txtStartDate.Visible = isCustom;
            txtEndDate.Visible = isCustom;
            startDateLabel.Visible = isCustom;
            endDateLabel.Visible = isCustom;
            pictureBox10.Visible = isCustom;
            pictureBox11.Visible = isCustom;    
        }

        private List<string> GetSelectedCategories  ()
        {
            List<string> selectedCategories = new List<string>();
            foreach (var item in txtCategory.CheckedItems)
            {
                selectedCategories.Add(item.ToString());
            }
            return selectedCategories;
        }



        private Budget CollectBudgetData()
        {
            var budget = new Budget
            {
                UserID = GlobalData.GetUserID(),
                BudgetName = txtName.Text,
                TotalAmount = float.Parse(txtAmount.Text),
                PeriodType = txtPeriod.SelectedItem.ToString(),
                CategoryIds = new List<int>()
            };

            switch (budget.PeriodType)
            {
                case "Daily":
                    budget.StartDate = DateTime.Today; 
                    budget.EndDate = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;

                case "Weekly":
                    budget.StartDate = DateTime.Today;
                    budget.EndDate = DateTime.Today.AddDays(7).AddTicks(-1);
                    break;

                case "Monthly":
                    budget.StartDate = DateTime.Today;
                    budget.EndDate = DateTime.Today.AddMonths(1).AddTicks(-1);
                    break;

                case "Custom":
                    budget.StartDate = txtStartDate.Value.Date;

                    budget.EndDate = txtEndDate.Value.Date.AddHours(23).AddMinutes(59);
                    break;

            }

            budget.IsActive = DateTime.Today >= budget.StartDate && DateTime.Today <= budget.EndDate;

            foreach (int index in txtCategory.CheckedIndices)
            {
                var category = categoryList[index];
                if (category != null)
                {
                    budget.CategoryIds.Add(category.CategoryID);
                }
                else
                {
                    MessageBox.Show("Invalid category selection.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return budget;
        }

        public void ClearForm()
        {
            txtName.Clear();
            txtAmount.Clear();
            txtPeriod.SelectedIndex = 0;
            txtStartDate.Value = DateTime.Today;
            txtEndDate.Value = DateTime.Today;
            txtCategory.ClearSelected();
        }




        private bool ValidateBudget(Budget budget)
        {
            if (string.IsNullOrWhiteSpace(budget.BudgetName))
            {
                MessageBox.Show("Budget name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (budget.TotalAmount <= 0)
            {
                MessageBox.Show("Total amount must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (budget.CategoryIds == null || budget.CategoryIds.Count == 0)
            {
                MessageBox.Show("At least one category must be selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (budget.StartDate >= budget.EndDate)
            {
                MessageBox.Show("Start date must be before the end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (budget.PeriodType != "Custom")
            {
                DateTime expectedEndDate = DateTime.Today;
                switch (budget.PeriodType)
                {
                    case "Daily":
                        expectedEndDate = DateTime.Today.AddDays(1).AddTicks(-1);
                        break;
                    case "Weekly":
                        expectedEndDate = DateTime.Today.AddDays(7).AddTicks(-1);
                        break;
                    case "Monthly":
                        expectedEndDate = DateTime.Today.AddMonths(1).AddTicks(-1);
                        break;
                }

                if (budget.StartDate != DateTime.Today || budget.EndDate != expectedEndDate)
                {
                    MessageBox.Show($"{budget.PeriodType} period type should start today and end correctly based on the selected period.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Budget budget = CollectBudgetData();


            if (budget == null)
            {
                return;
            }

            if (!ValidateBudget(budget))
            {
                return;
            }

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);


            try
            {
                _dataAccessLayer.SaveBudget(budget);
                MessageBox.Show("Budget added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalEvents.OnTransactionUpdated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add budget: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPeriod_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateDateVisibility();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (float.Parse(txtAmount.Text) == 0)
            {
                MessageBox.Show("Please enter a valid amount");
                txtAmount.Text = "";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
