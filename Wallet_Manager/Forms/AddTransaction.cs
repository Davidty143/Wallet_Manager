using Guna.UI2.AnimatorNS;
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
    public partial class AddTransaction : Form
    {
        int page = 1;
        public AddTransaction()
        {
            InitializeComponent();
            //addIncomeUC1.BringToFront();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addTransferUC1_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {

        }

        private void addIncomeUC1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addIncomeUC1_Load_1(object sender, EventArgs e)
        {

        }

        private void addTransferUC1_Load_1(object sender, EventArgs e)
        {

        }

        private void addIncomeUC1_Load_2(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            if (page != 1)
            {
                page = 1;
               // addIncomeUC1.BringToFront();
            }
        }

        private void guna2CustomGradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton3_Click_1(object sender, EventArgs e)
        {
            if (page != 3)
            {
                page = 3;
                //addTransferUC1.BringToFront();
            }
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            if (page != 2)
            {
                page = 2;
                //addExpenseUC1.BringToFront();
            }
        }

        private void guna2CustomGradientPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button_income_Click(object sender, EventArgs e)
        {
            if (page != 1)
            {
                page = 1;

                addIncomeUC1.BringToFront();
                label_transaction.Text = "Income";
                button_income.HoverState.FillColor = System.Drawing.Color.White;
                button_expense.HoverState.FillColor = System.Drawing.Color.LightGray;
                button_transfer.HoverState.FillColor = System.Drawing.Color.LightGray;

                button_income.BorderThickness = 2;
                button_income.BorderColor = System.Drawing.Color.Black;
                button_income.ForeColor = System.Drawing.Color.Black;

                button_expense.BorderThickness = 1;
                button_expense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_expense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));


                button_transfer.BorderThickness = 1;
                button_transfer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_transfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));

            }

            button_income.Cursor = Cursors.Default;
            button_expense.Cursor = Cursors.Hand;
            button_transfer.Cursor = Cursors.Hand;
        }

        private void button_expense_Click(object sender, EventArgs e)
        {
            if (page != 2)
            {
                page = 2;

                addExpenseUC1.BringToFront();
                label_transaction.Text = "Expense";

                button_expense.HoverState.FillColor = System.Drawing.Color.White;
                button_income.HoverState.FillColor = System.Drawing.Color.LightGray;
                button_transfer.HoverState.FillColor = System.Drawing.Color.LightGray;

                button_expense.BorderThickness = 2;
                button_expense.BorderColor = System.Drawing.Color.Black;
                button_expense.ForeColor = System.Drawing.Color.Black;

                button_income.BorderThickness = 1;
                button_income.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_income.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));

                button_transfer.BorderThickness = 1;
                button_transfer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_transfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));

            }

            button_expense.Cursor = Cursors.Default;
            button_income.Cursor = Cursors.Hand;
            button_transfer.Cursor = Cursors.Hand;
        }

        private void button_transfer_Click(object sender, EventArgs e)
        {
            if (page != 4)
            {
                page = 4;

                addTransferUC1.BringToFront();

                label_transaction.Text = "Transfer";


                button_transfer.HoverState.FillColor = System.Drawing.Color.White;
                button_expense.HoverState.FillColor = System.Drawing.Color.LightGray;
                button_income.HoverState.FillColor = System.Drawing.Color.LightGray;

                button_transfer.BorderThickness = 2;
                button_transfer.BorderColor = System.Drawing.Color.Black;
                button_transfer.ForeColor = System.Drawing.Color.Black;

                button_expense.BorderThickness = 1;
                button_expense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_expense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));

                button_income.BorderThickness = 1;
                button_income.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
                button_income.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));


            }

            button_transfer.Cursor = Cursors.Default;
            button_expense.Cursor = Cursors.Hand;
            button_income.Cursor = Cursors.Hand;
        }
    }
}
