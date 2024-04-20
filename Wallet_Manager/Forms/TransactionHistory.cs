using Guna.UI2.WinForms;
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
    public partial class TransactionHistory : UserControl
    {
        private List<Transaction> transactions = new List<Transaction>();
        private Panel[] transactionPanels;
        private PictureBox[] categoryPictureBoxes;
        private Label[] descriptionLabels;
        private Label[] categoryLabels;
        private Label[] transactionTypeLabels;
        private Label[] amountLabels;
        private Label[] walletNameLabels;
        private Label[] walletTypeLabels;
        private Label[] dateLabels;
        private Label[] editLabels;
        private Label[] deleteLabels;

        public TransactionHistory()
        {
            InitializeComponent();
            InitializeControlArrays();
            LoadTransactions();
            //ConnectEventHandlers();
        }


        private void InitializeControlArrays()
        {
            transactionPanels = new Guna2CustomGradientPanel[] { panel1, panel2, panel3, panel4, panel5, panel6 };
            categoryPictureBoxes = new PictureBox[] { categoryPictureBox1, categoryPictureBox2, categoryPictureBox3, categoryPictureBox4, categoryPictureBox5, categoryPictureBox6 };
            descriptionLabels = new Label[] { descriptionLabel1, descriptionLabel2, descriptionLabel3, descriptionLabel4, descriptionLabel5, descriptionLabel6 };
            categoryLabels = new Label[] { categoryLabel1, categoryLabel2, categoryLabel3, categoryLabel4, categoryLabel5, categoryLabel6 };
            transactionTypeLabels = new Label[] { transactionTypeLabel1, transactionTypeLabel2, transactionTypeLabel3, transactionTypeLabel4, transactionTypeLabel5, transactionTypeLabel6 };
            amountLabels = new Label[] { amountLabel1, amountLabel2, amountLabel3, amountLabel4, amountLabel5, amountLabel6 };
            walletNameLabels = new Label[] { walletNameLabel1, walletNameLabel2, walletNameLabel3, walletNameLabel4, walletNameLabel5, walletNameLabel6 };
            walletTypeLabels = new Label[] { walletType1, walletType2, walletType3, walletType4, walletType5, walletType6 };
            dateLabels = new Label[] { dateLabel1, dateLabel2, dateLabel3, dateLabel4, dateLabel5, dateLabel6 };
            editLabels = new Label[] { editLabel1, editLabel2, editLabel3, editLabel4, editLabel5, editLabel6 };
            deleteLabels = new Label[] { deleteLabel1, deleteLabel2, deleteLabel3, deleteLabel4, deleteLabel5, deleteLabel6 };

            foreach (var label in editLabels)
            {
                label.Click += editLabel_Click; // Attach the click event handler
            }

        }


        private void LoadTransactions()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            transactions = _dataAccessLayer.GetAllTransactions();
            int index = 0;

            foreach (var transaction in transactions)
            {
                if (index < transactionPanels.Length)
                {
                    transactionPanels[index].Visible = true;
                    descriptionLabels[index].Text = transaction.Description;
                    string categoryName = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    categoryLabels[index].Text = categoryName;
                    transactionTypeLabels[index].Text = transaction.TransactionType;
                    amountLabels[index].Text = $"₱ {transaction.Amount}";
                    string walletName = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    walletNameLabels[index].Text = walletName;
                    walletTypeLabels[index].Text = transaction.WalletCategory;
                    dateLabels[index].Text = transaction.Date.ToString("d");
                    editLabels[index].Tag = transaction.TransactionID; // Set the Tag to the transaction ID
                    index++;
                }
            }

            for (int i = index; i < transactionPanels.Length; i++)
            {
                transactionPanels[i].Visible = false;
            }
        }

        private void editLabel_Click(object sender, EventArgs e)
        {
            Label editLabel = sender as Label;
            if (editLabel != null && editLabel.Tag is int transactionId)
            {
                EditTransaction editForm = new EditTransaction(transactionId);
                editForm.ShowDialog(); // Show the form as a modal dialog
            }
        }





        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {

        }

        private void walletNameLabel2_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel3_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel4_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel5_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel6_Click(object sender, EventArgs e)
        {

        }

        private void walletNameLabel1_Click(object sender, EventArgs e)
        {

        }

        private void walletType5_Click(object sender, EventArgs e)
        {

        }

        private void editLabel2_Click(object sender, EventArgs e)
        {

        }

        private void deleteLabel2_Click(object sender, EventArgs e)
        {

        }

        private void editLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
