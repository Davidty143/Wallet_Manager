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
    public partial class EditBudget : Form
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

        private Budget currentBudget;
        private List<Category> categoryList = new List<Category>();


        public EditBudget(Budget budget)
        {
            InitializeComponent();
            currentBudget = budget;
            PopulateCategories();
            PopulatePeriodTypes();
            
            LoadBudgetData();
            UpdateDateVisibility();

            GlobalEvents.TransactionUpdated += PopulateCategories;

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

        private List<string> GetSelectedCategories()
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

        private void LoadBudgetData()
        {
            txtName.Text = currentBudget.BudgetName;
            txtAmount.Text = currentBudget.TotalAmount.ToString();
            txtPeriod.SelectedItem = currentBudget.PeriodType;
            txtStartDate.Value = currentBudget.StartDate;
            txtEndDate.Value = currentBudget.EndDate;

            foreach (var category in categoryList)
            {
                int index = txtCategory.Items.IndexOf(category.Name);
                if (index != -1 && currentBudget.CategoryIds.Contains(category.CategoryID))
                {
                    txtCategory.SetItemChecked(index, true);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Budget budget = CollectBudgetData();
            if (budget != null && ValidateBudget(budget))
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                dataAccessLayer.DeleteBudget(currentBudget.BudgetID);
                dataAccessLayer.SaveBudget(budget);
                
                MessageBox.Show("Budget updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalEvents.onBudgetUpdated();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateVisibility();
        }
    }
}
