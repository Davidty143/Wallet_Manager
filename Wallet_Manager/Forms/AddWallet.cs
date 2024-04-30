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
        private BusinessLogic _businessLogic;
        public AddWallet()
        {
            InitializeComponent();
            PopulateWalletTypes();
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            _businessLogic = new BusinessLogic(new SqlDataAccessLayer(_connectionString));
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
            {
                string walletName = txtName.Text;
                string walletType = txtType.Text;
                float spendingMoney;
                float savingsMoney;


                if (string.IsNullOrEmpty(walletName) || string.IsNullOrEmpty(walletType))
                {
                    MessageBox.Show("Please fill out all the fields.");
                    return;
                }

                Wallet newWallet = new Wallet
                {
                    UserID = GlobalData.GetUserID(),
                    WalletName = walletName,
                    WalletType = walletType,
                    SpendingMoney = 0,
                    SavingsMoney = 0
                };


                bool isWalletCreated = _businessLogic.CreateWallet(newWallet);

                if (isWalletCreated)
                {
                    MessageBox.Show("Wallet created successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to create wallet. A wallet of the same type may already exist.");
                }
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
    }
   
}
