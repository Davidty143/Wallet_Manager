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
    public partial class AddGoal : Form
    {
        public AddGoal()
        {
            InitializeComponent();
            PopulateWalletsComboBox();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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

            txtWallet.DisplayMember = "Text";
            txtWallet.ValueMember = "Value";
            txtWallet.DataSource = walletBindingList;
        }

        private void AddGoal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string goalName = txtGoalName.Text;
                decimal targetAmount = decimal.Parse(txtTargetAmount.Text);
                decimal currentAmount = decimal.Parse(txtCurrentAmount.Text);
                DateTime? deadline = datePickerDeadline.Value;
                int walletId = (int)comboBoxWallet.SelectedValue;

                // Insert into database (simplified)
                InsertGoal(goalName, targetAmount, currentAmount, deadline, walletId);

                MessageBox.Show("Goal added successfully!");
                this.Close(); // Optionally close the form
            }
            else
            {
                MessageBox.Show("Please check your input and try again.");
            }
        }
    }
}
