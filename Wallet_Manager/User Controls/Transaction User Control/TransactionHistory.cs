using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallet_Manager.Classes;

namespace Wallet_Manager.Forms
{
    public partial class TransactionHistory : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

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
        private int transactionsPerPage = 6;
        private int totalTransactions = 0;



        public TransactionHistory()
        {
            InitializeComponent();
            SetDoubleBuffering(this, true);
            InitializeControlArrays();
            LoadCategoryImages();
            LoadTransactions();




            GlobalEvents.TransactionUpdated += LoadTransactions;

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

        public static void SetDoubleBuffering(Control control, bool value)
        {
            Type controlType = control.GetType();

            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

            pi?.SetValue(control, value, null);

            foreach (Control childControl in control.Controls)
            {
                SetDoubleBuffering(childControl, value);
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
                label.Click += editLabel_Click;
            }

            foreach (var label in deleteLabels)
            {
                label.Click += deleteLabel_Click;
            }

            labelNext.Click += new EventHandler(labelNext_Click);
            labelPrev.Click += new EventHandler(labelPrev_Click);

        }
        private void LoadCategoryImages()
        {
            categoryImages = new Image[19];
            for (int i = 0; i < categoryImages.Length; i++)
            {
                string imageName = (i + 1).ToString();
                categoryImages[i] = (Image)Properties.Resources.ResourceManager.GetObject(imageName);

                if (categoryImages[i] == null)
                {
                    Console.WriteLine($"Image '{imageName}.png' not found for category ID: {i + 1}, using default image.");
                    categoryImages[i] = Properties.Resources.button_budget_active; 
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
                    amountLabels[index].Text = $"{transaction.Amount:C}".Insert(1, " ");
                    string walletName = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    walletNameLabels[index].Text = walletName;
                    walletTypeLabels[index].Text = transaction.WalletCategory;
                    dateLabels[index].Text = transaction.Date.ToString("d");
                    editLabels[index].Tag = transaction.TransactionID;
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
                    amountLabels[i].Text = $"{transaction.Amount:C}".Insert(1, " ");
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    walletTypeLabels[i].Text = transaction.WalletCategory;
                    dateLabels[i].Text = transaction.Date.ToString("d");
                    editLabels[i].Tag = transaction.TransactionID;
                    deleteLabels[i].Tag = transaction.TransactionID;

                    if (categoryPictureBoxes[i] == null)
                    {
                        Console.WriteLine("PictureBox at index " + i + " is null.");
                        continue;
                    }

                    int imageIndex = transaction.CategoryID - 1;
                    if (imageIndex < 0 || categoryImages[imageIndex] == null)
                    {
                        Console.WriteLine("Invalid or missing image for Category ID: " + transaction.CategoryID);
                        categoryPictureBoxes[i].Image = Properties.Resources.button_budget_active;
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
                UpdatePaginationLabel();
            }
        }


        private void editLabel_Click(object sender, EventArgs e)
        {
            Label editLabel = sender as Label;
            if (editLabel != null && editLabel.Tag is int transactionId)
            {
                EditTransaction editForm = new EditTransaction(transactionId);
                editForm.ShowDialog();
                LoadTransactions();
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
                    //LoadTransactions();
                    GlobalEvents.OnTransactionUpdated();

                }
            }
        }

        public void ApplyFilters(string transactionType, string category, string wallet, string walletCategory, DateTime startDate, DateTime endDate)
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            string filterTransactionType = transactionType == "All" ? null : transactionType;
            string filterCategory = (category == "0" || category == null) ? null : category;
            string filterWallet = (wallet == "0" || wallet == null) ? null : wallet;
            string filterWalletCategory = walletCategory == "All" ? null : walletCategory;

            transactions = _dataAccessLayer.GetAllFilteredTransactions(filterTransactionType, filterCategory, filterWallet, filterWalletCategory, startDate, endDate);
            totalTransactions = transactions.Count;
            currentPage = 0;
            UpdateTransactionDisplay();
        }

        private void UpdatePaginationLabel()
        {
            int totalPages = totalTransactions > 0 ? (totalTransactions + transactionsPerPage - 1) / transactionsPerPage : 0;

            if (totalPages == 0)
            {
                currentPage = 0;
                paginationLabel.Text = "Page 0 of 0";
            }
            else
            {
                if (currentPage >= totalPages)
                {
                    currentPage = totalPages - 1;
                }
                paginationLabel.Text = $"Page {currentPage + 1} of {totalPages}";
            }
        }




        private void UpdateFilteredTransactionDisplay(List<Transaction> filteredTransactions)
        {
            int start = 0;
            int end = Math.Min(transactionsPerPage, filteredTransactions.Count);
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            for (int i = 0; i < transactionPanels.Length; i++)
            {
                if (i < end)
                {
                    var transaction = filteredTransactions[i + start];
                    transactionPanels[i].Visible = true;
                    descriptionLabels[i].Text = transaction.Description;
                    categoryLabels[i].Text = _dataAccessLayer.GetCategoryNameById(transaction.CategoryID);
                    transactionTypeLabels[i].Text = transaction.TransactionType;
                    amountLabels[i].Text = $"{transaction.Amount:C}".Insert(1, " ");
                    walletNameLabels[i].Text = _dataAccessLayer.GetWalletNameById(transaction.WalletID);
                    walletTypeLabels[i].Text = transaction.WalletCategory;
                    dateLabels[i].Text = transaction.Date.ToString("d");
                    editLabels[i].Tag = transaction.TransactionID;
                    deleteLabels[i].Tag = transaction.TransactionID;

                    if (categoryPictureBoxes[i] == null)
                    {
                        Console.WriteLine("PictureBox at index " + i + " is null.");
                        continue;
                    }

                    int imageIndex = transaction.CategoryID - 1;
                    if (imageIndex < 0 || categoryImages[imageIndex] == null)
                    {
                        Console.WriteLine("Invalid or missing image for Category ID: " + transaction.CategoryID);
                        categoryPictureBoxes[i].Image = Properties.Resources.button_budget_active;
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
                UpdatePaginationLabel();

            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

            SearchFilter searchForm = SearchFilter.GetInstance(this);
            if (searchForm == null)
            {
                searchForm = new SearchFilter(this);
                searchForm.ShowDialog();
            }

            searchForm.ShowDialog();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;


            if (dashboardParent.transactionForm == null || dashboardParent.transactionForm.IsDisposed)
            {
                dashboardParent.transactionForm = new AddTransaction();
            }
            dashboardParent.transactionForm.Show();
            dashboardParent.transactionForm.BringToFront();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            SearchFilter searchForm = SearchFilter.GetInstance(this);
            if (searchForm == null)
            {
                searchForm = new SearchFilter(this);
                searchForm.ShowDialog();
            }
            searchForm.ShowDialog();
        }

        public void RemoveFilters()
        {
            string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
            SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);

            transactions = _dataAccessLayer.GetAllTransactions();
            totalTransactions = transactions.Count;
            currentPage = 0;
            UpdateTransactionDisplay();
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            Dashboard dashboardParent = this.FindForm() as Dashboard;


            if (dashboardParent.transactionForm == null || dashboardParent.transactionForm.IsDisposed)
            {
                dashboardParent.transactionForm = new AddTransaction();
            }
            dashboardParent.transactionForm.Show();
            dashboardParent.transactionForm.BringToFront();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            RemoveFilters();
        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {

        }

        private void deleteLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
