using Org.BouncyCastle.Utilities.IO.Pem;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Wallet_Manager.Forms
{
    public partial class AddWallet : Form
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


        public AddWallet()
        {
            InitializeComponent();
            PopulateWalletTypes();
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);
        }

        private void PopulateWalletTypes()
        {
            List<string> walletTypes = new List<string>
            {
                "Pocket Wallet",
                "E-Wallet",
                "Bank Wallet",
                "Crypto Wallet",
                "Travel Wallet"
            };

            txtType.DataSource = walletTypes;
        }




        private void t_spending_TextChanged(object sender, EventArgs e)
        {

        }


        private void AddWallet_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Wallet_Click(object sender, EventArgs e)
        {
            string walletName = txtName.Text;
            string walletType = txtType.Text;

            // Validate input values for wallet name and type
            if (string.IsNullOrEmpty(walletName) || string.IsNullOrEmpty(walletType))
            {
                MessageBox.Show("Please fill out all the fields.");
                return;
            }

            // Safely parse spending and savings amounts
            if (!float.TryParse(spendingAmountTextBox.Text, out float spendingMoney))
            {
                MessageBox.Show("Invalid input for spending amount. Please enter a valid number.");
                return;
            }

            if (!float.TryParse(savingsAmountTextBox.Text, out float savingsMoney))
            {
                MessageBox.Show("Invalid input for savings amount. Please enter a valid number.");
                return;
            }

            Wallet newWallet = new Wallet
            {
                UserID = GlobalData.GetUserID(),
                WalletName = walletName,
                WalletType = walletType,
                SpendingMoney = spendingMoney,
                SavingsMoney = savingsMoney
            };

            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            // Attempt to create the new wallet
            bool isWalletCreated = dataAccessLayer.CreateWalletValidate(newWallet);

            if (isWalletCreated)
            {
                int newWalletId = dataAccessLayer.GetWalletIdByUserIdAndName(GlobalData.GetUserID(), walletName);

                // Record transactions if there's initial money in either account
                if (spendingMoney > 0)
                {
                    Transaction deposit = new Transaction
                    {
                        UserID = GlobalData.GetUserID(),
                        WalletID = newWalletId,
                        WalletCategory = "Spending",
                        TransactionType = "Income",
                        CategoryID = 8, // Assuming CategoryID is predefined
                        Amount = spendingMoney,
                        Date = DateTime.Now,
                        Description = "Add Wallet"
                    };
                    dataAccessLayer.AddTransaction(deposit);
                }

                if (savingsMoney > 0)
                {
                    Transaction deposit = new Transaction
                    {
                        UserID = GlobalData.GetUserID(),
                        WalletID = newWalletId,
                        WalletCategory = "Savings",
                        TransactionType = "Income",
                        CategoryID = 8, // Assuming CategoryID is predefined
                        Amount = savingsMoney,
                        Date = DateTime.Now,
                        Description = "Add Wallet"
                    };
                    dataAccessLayer.AddTransaction(deposit);
                }
                GlobalEvents.OnTransactionUpdated();

                MessageBox.Show("Wallet created successfully.");
            }
            else
            {
                MessageBox.Show("Failed to create wallet. A wallet of the same type may already exist.");
            }
        }



        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void spendingAmountTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void spendingAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void savingsAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void savingsAmountTextBox_Leave(object sender, EventArgs e)
        {

        }
    }
   
}
