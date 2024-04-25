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
                addIncomeUC1.BringToFront();
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
                addTransferUC1.BringToFront();
            }
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            if (page != 2)
            {
                page = 2;
                addExpenseUC1.BringToFront();
            }
        }
    }
}
