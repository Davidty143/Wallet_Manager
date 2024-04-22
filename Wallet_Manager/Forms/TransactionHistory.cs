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

        private Image[] categoryImages;


        private int currentPage = 0;
        private int transactionsPerPage = 6; // Adjust based on your UI setup
        private int totalTransactions = 0;

        public TransactionHistory()
        {
            InitializeComponent();
            InitializeControlArrays();
            LoadCategoryImages();
            LoadTransactions();

            //ConnectEventHandlers();
        }

        private void labelPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateTransactionDisplay();
            }
        }

        private void labelNext_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) * transactionsPerPage < totalTransactions)
            {
                currentPage++;
                UpdateTransactionDisplay();
            }
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

            foreach (var label in deleteLabels)
            {
                label.Click += deleteLabel_Click; // Attach the click event handler
            }

            labelNext.Click += new EventHandler(labelNext_Click);
            labelPrev.Click += new EventHandler(labelPrev_Click);

        }
        private void LoadCategoryImages()
        {
            categoryImages = new Image[19]; // Create an array to hold 19 images
            for (int i = 0; i < categoryImages.Length; i++)
            {
                string imageName = (i + 1).ToString(); // This will generate "1", "2", ..., "19"
                categoryImages[i] = (Image)Properties.Resources.ResourceManager.GetObject(imageName);

                if (categoryImages[i] == null)
                {
                    Console.WriteLine($"Image '{imageName}.png' not found for category ID: {i + 1}, using default image.");
                    categoryImages[i] = Properties.Resources.button_budget_active; // Fallback to a default image
                    if (categoryImages[i] == null)
                    {
                        Console.WriteLine("Default image is also not found. Check resource file.");
                    }
                }
            }
        }


        private void LoadTransactions()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
            transactions = _dataAccessLayer.GetAllTransactions();
            totalTransactions = transactions.Count;
            UpdateTransactionDisplay();

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
                    deleteLabels[index].Tag = transaction.TransactionID;
                    index++;
                }
            }

            for (int i = index; i < transactionPanels.Length; i++)
            {
                transactionPanels[i].Visible = false;
            }
        }

        private void UpdateTransactionDisplay()
        {
            int start = currentPage * transactionsPerPage;
            int end = Math.Min(start + transactionsPerPage, transactions.Count);
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            for (int i = 0; i < transactionPanels.Length; i++)
            {
                if (i + start < end)
                {
                    var transaction = transactions[i + start];
                    transactionPanels[i].Visible = true;
                    descriptionLabels[i].Text = transaction.Description;
                    categoryLabels[i].Text = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    transactionTypeLabels[i].Text = transaction.TransactionType;
                    amountLabels[i].Text = $"₱ {transaction.Amount}";
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    walletTypeLabels[i].Text = transaction.WalletCategory;
                    dateLabels[i].Text = transaction.Date.ToString("d");
                    editLabels[i].Tag = transaction.TransactionID;
                    deleteLabels[i].Tag = transaction.TransactionID;

                    // Check for null PictureBox and Image
                    if (categoryPictureBoxes[i] == null)
                    {
                        Console.WriteLine("PictureBox at index " + i + " is null.");
                        continue; // Skip this iteration
                    }

                    int imageIndex = transaction.CategoryID - 1; // Calculate the index
                    if (imageIndex < 0 || categoryImages[imageIndex] == null)
                    {
                        Console.WriteLine("Invalid or missing image for Category ID: " + transaction.CategoryID);
                        categoryPictureBoxes[i].Image = Properties.Resources.button_budget_active; // Use default image
                    }
                    else
                    {
                        categoryPictureBoxes[i].Image = categoryImages[imageIndex];
                    }
                }
                else
                {
                    transactionPanels[i].Visible = false;
                }
            }
        }


        private void editLabel_Click(object sender, EventArgs e)
        {
            Label editLabel = sender as Label;
            if (editLabel != null && editLabel.Tag is int transactionId)
            {
                EditTransaction editForm = new EditTransaction(transactionId);
                editForm.ShowDialog(); // Show the form as a modal dialog
                LoadTransactions(); // Reload transactions to reflect the change
            }
        }

        private void deleteLabel_Click(object sender, EventArgs e)
        {
            Label deleteLabel = sender as Label;
            if (deleteLabel != null && deleteLabel.Tag is int transactionId)
            {
                if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                    SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                    _dataAccessLayer.DeleteTransaction(transactionId);
                    LoadTransactions(); // Reload transactions to reflect the change
                }
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

        private void categoryPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Assuming SearchFilter is correctly implementing a Singleton pattern
            // and has a public static property called Instance to access it.
            SearchFilter searchForm = SearchFilter.Instance;
            if (searchForm == null)
            {
                searchForm = new SearchFilter();
                searchForm.ShowDialog(); // Set the instance if it's part of the Singleton pattern
            }

            searchForm.ShowDialog(); // Show the form as a modal dialog
            //LoadTransactions(); // Reload transactions to reflect the changes
        }
    }
}
