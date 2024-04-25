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
        public AddBudget(int userID)
        {
            InitializeComponent();
            PopulateCategories();
            PopulatePeriodTypes();
        }

        private void txtWallet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private List<Category> categoryList = new List<Category>();

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
            // Clear existing items if any
            txtPeriod.Items.Clear();

            // Add period type choices
            txtPeriod.Items.Add("Daily");
            txtPeriod.Items.Add("Weekly");
            txtPeriod.Items.Add("Monthly");

            // Optionally set a default value
            txtPeriod.SelectedIndex = 0;  // Sets the first item as the default selected item
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

        private void AddBudget_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Budget budget = CollectBudgetData();
            if (budget == null)
            {
                return;
            }

            // Validate the collected data
            if (!ValidateBudget(budget))
            {
                // Validation failed, appropriate messages already shown
                return;
            }

            // Setup the database connection string
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            // Initialize the data access layer
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);


            try
            {
                _dataAccessLayer.SaveBudget(budget);
                MessageBox.Show("Budget added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add budget: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private Budget CollectBudgetData()
        {
            var budget = new Budget
            {
                UserID = 1,
                BudgetName = txtName.Text,
                TotalAmount = float.Parse(txtAmount.Text),
                PeriodType = txtPeriod.SelectedItem.ToString(),
                StartDate = txtStartDate.Value,
                EndDate = txtEndDate.Value,
                CategoryIds = new List<int>()
            };


            // Collect category IDs from the CheckedListBox
            foreach (int index in txtCategory.CheckedIndices)
            {
                var category = categoryList[index]; // Directly access by index if aligned with CheckedListBox items
                if (category != null)
                {
                    budget.CategoryIds.Add(category.CategoryID); // Use the Id property of the Category class
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
            // Check if the budget name is not provided
            if (string.IsNullOrWhiteSpace(budget.BudgetName))
            {
                MessageBox.Show("Budget name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if the total amount is less than or equal to zero
            if (budget.TotalAmount <= 0)
            {
                MessageBox.Show("Total amount must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if no categories are selected
            if (budget.CategoryIds == null || budget.CategoryIds.Count == 0)
            {
                MessageBox.Show("At least one category must be selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if the start date is not before the end date
            if (budget.StartDate >= budget.EndDate)
            {
                MessageBox.Show("Start date must be before the end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // If all checks pass
            return true;
        }



    }
}
