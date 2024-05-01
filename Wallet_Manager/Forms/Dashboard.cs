﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class Dashboard : Form
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

        public AddTransaction transactionForm = null;
        public SearchFilter searchFilter = null;
        public TransactionHistory transactionHistory = null;
        public Wallet_uc wallet = null;
        public InsightsUC insightsUC = null;
        public Budget1_uc budgetUC = null;
        public DashboardUC dashboardUC = null;
        public SettingsUC settingsUC = null;
        public DashboardUC dashboardUC1 = null;

        public Dashboard()
        {
            InitializeComponent();

            // Load the main dashboard component first
            EnsureDashboardUCLoaded();

            // Hook up the Load event to handle asynchronous loading of other components
            this.Load += Dashboard_Load;

            // Other initializations
            UpdateDisplayName();
            LoadUserProfilePicture();
            transactionForm = new AddTransaction();
        }



        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Asynchronously load other components to keep the UI responsive
            Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate
                {
                    // Load other components
                    EnsureSettingsUCLoaded();
                    EnsureTransactionHistoryLoaded();
                    EnsureWalletLoaded();
                    EnsureInsightsUCLoaded();
                    EnsureBudgetUCLoaded();

                    // Optionally, hide these controls initially if needed
                    settingsUC.Visible = false;
                    transactionHistory.Visible = false;
                    wallet.Visible = false;
                    insightsUC.Visible = false;
                    budgetUC.Visible = false;
                });
            });
        }





        private void EnsureTransactionHistoryLoaded()
        {
            if (transactionHistory == null)
            {
                transactionHistory = new TransactionHistory();
                transactionHistory.Dock = DockStyle.Fill;
                display_panel.Controls.Add(transactionHistory); // Assuming 'display_panel' is the container
            }
            transactionHistory.BringToFront();
        }

        private void EnsureWalletLoaded()
        {
            if (wallet == null)
            {
                wallet = new Wallet_uc();
                wallet.Dock = DockStyle.Fill;
                display_panel.Controls.Add(wallet);
            }
            wallet.BringToFront();
        }

        private void EnsureInsightsUCLoaded()
        {
            if (insightsUC == null)
            {
                insightsUC = new InsightsUC();
                insightsUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(insightsUC);
            }
            insightsUC.BringToFront();
        }

        private void EnsureBudgetUCLoaded()
        {
            if (budgetUC == null)
            {
                budgetUC = new Budget1_uc();
                budgetUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(budgetUC);
            }
            budgetUC.BringToFront();
        }

        private void EnsureDashboardUCLoaded()
        {
            if (dashboardUC == null)
            {
                dashboardUC = new DashboardUC();
                dashboardUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(dashboardUC);
            }
            dashboardUC.BringToFront();
        }

        private void EnsureSettingsUCLoaded()
        {
            if (settingsUC == null)
            {
                settingsUC = new SettingsUC();
                settingsUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(settingsUC);
            }
            settingsUC.BringToFront();
        }




        private void display_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }



        private void button_wallet_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_wallet);
            pageLabel.Text = "Wallet";
            EnsureWalletLoaded();
            wallet.Visible = true;
            //wallet_uc1.BringToFront();
        }


        internal void clickSeeAllWallets()
        {
            UpdateButtonStyles(button_wallet);
            pageLabel.Text = "Wallet";
            //wallet_uc1.BringToFront();

        }

        private void button_transaction_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_transaction);
            pageLabel.Text = "Transaction";
            EnsureTransactionHistoryLoaded();
            transactionHistory.Visible = true;
            //transactionHistory1.BringToFront();

        }

        internal void clickSeeAllTransactions()
        {
            UpdateButtonStyles(button_transaction);
            pageLabel.Text = "Transaction";
            //transactionHistory1.BringToFront();

        }

        private void button_insights_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_analytics);
            pageLabel.Text = "Analytics";
            EnsureInsightsUCLoaded();
            insightsUC.Visible = true;
            //insightsUC1.BringToFront();
        }

        internal void clickSeeAllAnalytics()
        {
            UpdateButtonStyles(button_analytics);
            pageLabel.Text = "Analytics";
            //insightsUC1.BringToFront();

        }

        private void button_budget_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_budget);
            pageLabel.Text = "Budget";
            EnsureBudgetUCLoaded();
            budgetUC.Visible = true;
            //budget1_uc1.BringToFront();
        }
        internal void clickSeeAllBudgets()
        {
            UpdateButtonStyles(button_budget);
            pageLabel.Text = "Budget";
            //budget1_uc1.BringToFront();

        }

        private void button_goals_Click(object sender, EventArgs e)
        {
            
        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_dashboard);
            pageLabel.Text = "Dashboard";
            EnsureDashboardUCLoaded();
            
            
            

            //dashboardUC1.BringToFront();

        }
        private void UpdateButtonStyles(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Define default and active styles
            Color defaultFillColor = Color.White;
            Color activeFillColor = Color.FromArgb(121, 105, 233);
            Color defaultForeColor = Color.FromArgb(138, 138, 138);
            Color activeForeColor = Color.White;

            // List of all buttons
            Guna.UI2.WinForms.Guna2Button[] buttons = {
        button_wallet, button_transaction, button_analytics, button_budget, button_profile, button_dashboard
    };

            foreach (var button in buttons)
            {
                if (button == activeButton)
                {
                    // Active button style
                    button.ForeColor = activeForeColor;
                    button.FillColor = activeFillColor;
                    // Set the active image
                    button.Image = (Image)Properties.Resources.ResourceManager.GetObject(button.Name + "_active");
                }
                else
                {
                    // Default style for inactive buttons
                    button.ForeColor = defaultForeColor;
                    button.FillColor = defaultFillColor;
                    // Set the inactive image
                    button.Image = (Image)Properties.Resources.ResourceManager.GetObject(button.Name + "_inactive");
                }
            }
        }

        private void LoadUserProfilePicture()
        {
            try
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Image profile = _dataAccessLayer.GetProfilePicture(GlobalData.GetUserID());
                if (profile != null)
                {
                    profilePicture.Image = profile;
                }

                else
                {
                    profilePicture.Image = Properties.Resources.user;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile picture: " + ex.Message);
            }
        }

        private void UpdateLabelText(string newText)
        {
            displayName.Text = newText;
            displayName.Location = new Point(profilePicture.Left - displayName.Width - 10, profilePicture.Top); // Recalculate position
        }

        private void UpdateDisplayName()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            string name = _dataAccessLayer.GetDisplayNameById();
            displayName.Text = name; // Assuming 'displayName' is a Label control on your form

            // Calculate the new location to align the label with the center of the profile picture
            int verticalCenter = profilePicture.Top + (profilePicture.Height / 2) - (displayName.Height / 2);


            displayName.Location = new Point(profilePicture.Left - displayName.Width - 10, verticalCenter);
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void transactionHistory1_Load(object sender, EventArgs e)
        {
         
        
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dashboardUC2_Load(object sender, EventArgs e)
        {

        }

        private void dashboardUC2_Load_1(object sender, EventArgs e)
        {
           
        }

        private void dashboardUC2_Load_2(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }

        private void editProfile_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_profile);
            pageLabel.Text = "Profile";
            //settingsUC1.BringToFront();
        }

        private void settingsUC1_Load(object sender, EventArgs e)
        {

        }

        private void insightsUC1_Load(object sender, EventArgs e)
        {

        }

        private void budget1_uc1_Load(object sender, EventArgs e)
        {

        }

        private void dashboardUC1_Load(object sender, EventArgs e)
        {

        }

        private void wallet_uc1_Load(object sender, EventArgs e)
        {

        }

        private void button_profile_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_profile);
            pageLabel.Text = "Profile";
            EnsureSettingsUCLoaded();
            settingsUC.Visible = true;  
           // settingsUC1.BringToFront();
        }

        private void pageLabel_Click(object sender, EventArgs e)
        {

        }

        private void dashboardUC1_Load_1(object sender, EventArgs e)
        {

        }

        private void dashboardUC1_Load_2(object sender, EventArgs e)
        {

        }

        private void displayName_Click(object sender, EventArgs e)
        {

        }

        private void dashboardUC1_Load_3(object sender, EventArgs e)
        {

        }

        private void dashboardUC2_Load_3(object sender, EventArgs e)
        {

        }
    }
}
