using Org.BouncyCastle.Crypto.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class Wallet_uc : UserControl
    {
        private int currentWalletID;
        PictureBox editPictureBox = new System.Windows.Forms.PictureBox();
        PictureBox deletePictureBox = new System.Windows.Forms.PictureBox();
        public Wallet_uc()
        {
            InitializeComponent();
            PopulateWalletsComboBox();
            UpdateWalletDisplay();

            editPictureBox.Click += editWalletPictureBox_Click;
            deletePictureBox.Click += deleteWalletPictureBox_Click;


        }

        private void UpdateWalletDisplay(Wallet wallet)
        {
            if (wallet != null)
            {
                walletNameLabel.Text = wallet.WalletName ?? "N/A";
                walletTypeLabel.Text = wallet.WalletType ?? "N/A";
                spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C"); // Assuming currency format
                savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C"); // Assuming currency format
            }
        }

        private void PopulateWalletsComboBox()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            List<Wallet> wallets = _dataAccessLayer.GetWallets();

            // Convert wallets to a binding-friendly format
            var walletBindingList = wallets.Select(wallet => new
            {
                Text = $"{wallet.WalletName} (ID: {wallet.WalletID})",
                Value = wallet.WalletID
            }).ToList();

            selectWalletComboBox.DisplayMember = "Text";
            selectWalletComboBox.ValueMember = "Value";
            selectWalletComboBox.DataSource = walletBindingList;
        }

        private void UpdateWalletDisplay()
        {
            int walletId = 0;
            if (int.TryParse(selectWalletComboBox.SelectedValue?.ToString(), out walletId))
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Wallet wallet = _dataAccessLayer.GetWalletById(walletId);
                if (wallet != null)
                {
                    currentWalletID = wallet.WalletID;
                    walletNameLabel.Text = wallet.WalletName ?? "N/A";
                    walletTypeLabel.Text = wallet.WalletType ?? "N/A";
                    spendingBalanceLabel.Text = wallet.SpendingMoney.ToString("C");
                    savingBalanceLabel.Text = wallet.SavingsMoney.ToString("C");
                    totalBalanceLabel.Text = (wallet.SavingsMoney + wallet.SpendingMoney).ToString("C");
                }
            }
            else
            {
                // Handle the case where walletId is not valid or conversion failed
                MessageBox.Show("Selected wallet ID is invalid.");
            }
        }

        private void EditPictureBox_Click(object sender, EventArgs e)
        {

        }
       




        private void Wallet_uc_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {
            
        }

        private void selectWalletComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWalletDisplay();
        }

        private void editWalletPictureBox_Click(object sender, EventArgs e)
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            Wallet wallet = _dataAccessLayer.GetWalletById(currentWalletID);
            if (wallet != null)
            {
                // Open an edit form and pass the wallet object
                EditWallet editForm = new EditWallet(wallet);
                editForm.ShowDialog();
                UpdateWalletDisplay();
                PopulateWalletsComboBox();
            }
            else
            {
                MessageBox.Show("Wallet not found.");
            }
        }

        private void deleteWalletPictureBox_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this wallet?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                bool success = _dataAccessLayer.DeleteWallet(currentWalletID);
                if (success)
                {
                    MessageBox.Show("Wallet deleted successfully.");
                    UpdateWalletDisplay(); // Refresh the display
                    PopulateWalletsComboBox();
                }
                else
                {
                    MessageBox.Show("Failed to delete wallet.");
                }
            }
        }
    }
}
