using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallet_Manager.Forms
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
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
        }

        private void button_transaction_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_transaction);
        }

        private void button_insights_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_analytics);
        }

        private void button_budget_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_budget);
        }

        private void button_goals_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_goals);
        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(button_dashboard);

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
        button_wallet, button_transaction, button_analytics, button_budget, button_goals, button_dashboard
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void transactionHistory1_Load(object sender, EventArgs e)
        {
                    }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddTransaction transaction =  new AddTransaction();
            transaction.ShowDialog();
        }
    }
}
