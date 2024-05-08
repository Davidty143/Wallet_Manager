using System;
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
        private bool isExitTriggered = false;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
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
            this.FormClosing += Dashboard_FormClosing;
            EnsureDashboardUCLoaded();
            this.Load += Dashboard_Load;
            UpdateDisplayName();
            LoadUserProfilePicture();
            transactionForm = new AddTransaction();

            GlobalEvents.ProfileInformationUpdated += LoadUserProfilePicture;
            GlobalEvents.ProfileInformationUpdated += UpdateDisplayName;


        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isExitTriggered)
            {
                e.Cancel = true;
                this.Hide();
            }
        }



        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Asynchronously load other components to keep the UI responsive
            Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate
                {
                    EnsureSettingsUCLoaded();
                    EnsureTransactionHistoryLoaded();
                    EnsureWalletLoaded();
                    EnsureInsightsUCLoaded();
                    EnsureBudgetUCLoaded();

                    settingsUC.Visible = false;
                    transactionHistory.Visible = false;
                    wallet.Visible = false;
                    insightsUC.Visible = false;
                    budgetUC.Visible = false;
                });
            });
        }

        public void EnsureTransactionHistoryLoaded()
        {
            if (transactionHistory == null)
            {
                transactionHistory = new TransactionHistory();
                transactionHistory.Dock = DockStyle.Fill;
                display_panel.Controls.Add(transactionHistory); // Assuming 'display_panel' is the container
            }
            transactionHistory.BringToFront();
        }

        public void EnsureWalletLoaded()
        {
            if (wallet == null)
            {
                wallet = new Wallet_uc();
                wallet.Dock = DockStyle.Fill;
                display_panel.Controls.Add(wallet);
            }
            wallet.BringToFront();
        }

        public void EnsureInsightsUCLoaded()
        {
            if (insightsUC == null)
            {
                insightsUC = new InsightsUC();
                insightsUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(insightsUC);
            }
            insightsUC.BringToFront();
        }

        public void EnsureBudgetUCLoaded()
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

        public void EnsureSettingsUCLoaded()
        {
            if (settingsUC == null)
            {
                settingsUC = new SettingsUC();
                settingsUC.Dock = DockStyle.Fill;
                display_panel.Controls.Add(settingsUC);
            }
            settingsUC.BringToFront();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button_wallet_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_wallet);
            pageLabel.Text = "Wallet";
            EnsureWalletLoaded();
            wallet.Visible = true;
        }


        internal void clickSeeAllWallets()
        {
            button_wallet_Click(this, EventArgs.Empty);

        }

        private void button_transaction_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_transaction);
            pageLabel.Text = "Transaction";
            EnsureTransactionHistoryLoaded();
            transactionHistory.Visible = true;

        }

        internal void clickSeeAllTransactions()
        {
            UpdateButtonStyles(button_transaction);
            pageLabel.Text = "Transaction";
            EnsureTransactionHistoryLoaded();
            transactionHistory.Visible = true;
            transactionHistory.BringToFront();

        }

        private void button_insights_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_analytics);
            pageLabel.Text = "Analytics";
            EnsureInsightsUCLoaded();
            insightsUC.Visible = true;
        }

        internal void clickSeeAllAnalytics()
        {
            button_insights_Click(this, EventArgs.Empty);

        }

        private void button_budget_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_budget);
            pageLabel.Text = "Budget";
            EnsureBudgetUCLoaded();
            budgetUC.Visible = true;
        }

        internal void clickSeeAllBudgets()
        {
            button_budget_Click(this, EventArgs.Empty);

        }
        private void button_dashboard_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_dashboard);
            pageLabel.Text = "Dashboard";
            EnsureDashboardUCLoaded();
        }

        internal void clickDashboard()
        {
            button_dashboard_Click(this, EventArgs.Empty);
        }

        private void UpdateButtonStyles(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Color defaultFillColor = Color.White;
            Color activeFillColor = Color.FromArgb(121, 105, 233);
            Color defaultForeColor = Color.FromArgb(138, 138, 138);
            Color activeForeColor = Color.White;

            Guna.UI2.WinForms.Guna2Button[] buttons = {
        button_wallet, button_transaction, button_analytics, button_budget, button_profile, button_dashboard
    };

            foreach (var button in buttons)
            {
                if (button == activeButton)
                {
                    button.ForeColor = activeForeColor;
                    button.FillColor = activeFillColor;
                    button.Image = (Image)Properties.Resources.ResourceManager.GetObject(button.Name + "_active");
                }
                else
                {
                    button.ForeColor = defaultForeColor;
                    button.FillColor = defaultFillColor;
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
            displayName.Text = name;

            int verticalCenter = profilePicture.Top + (profilePicture.Height / 2) - (displayName.Height / 2);


            displayName.Location = new Point(profilePicture.Left - displayName.Width - 10, verticalCenter);
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            isExitTriggered = true;
            Application.Exit();

        }

        private void editProfile_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_profile);
            pageLabel.Text = "Profile";
        }

        private void button_profile_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_profile);
            pageLabel.Text = "Profile";
            EnsureSettingsUCLoaded();
            settingsUC.Visible = true;  
        }

        private void button_signout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to sign out?", "Sign Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Properties.Settings.Default.IsLoggedIn = false;
                Properties.Settings.Default.LastUserID = 0;
                Properties.Settings.Default.Save();

                Program.ShowLoginForm();


                this.Close();
            }
            else
            {
                return;
            }
        }

        private void displayName_Click(object sender, EventArgs e)
        {

        }
    }
}
