    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MySql.Data.MySqlClient;

    namespace Wallet_Manager.Classes
    {
        internal class SqlDataAccessLayer
        {
            private string _connectionString;



            public SqlDataAccessLayer(string connectionString)  
            {
                _connectionString = connectionString;
                GlobalEvents.BudgetUpdated += UpdateBudgetStatuses;
            }

        public Transaction Transaction
        {
            get => default;
            set
            {
            }
        }

        internal Category Category
        {
            get => default;
            set
            {
            }
        }

        public Wallet Wallet
        {
            get => default;
            set
            {
            }
        }

        internal User User
        {
            get => default;
            set
            {
            }
        }

        public Budget Budget
        {
            get => default;
            set
            {
            }
        }

        public bool CreateAccount(string firstName, string lastName, string email, string password)
            {
                User existingUser = GetUserByEmail(email);
                if (existingUser != null)
                {
                    return false;
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                User newUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = hashedPassword
                };

                return CreateUser(newUser);
            }

            public bool AuthenticateUser(string email, string password)
            {
                User user = GetUserByEmail(email);

                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool LoginUser(string email, string password)
            {
                if (AuthenticateUser(email, password))
                {
                    int userID = GetUserID(email);

                    if (userID != -1)
                    {
                        GlobalData.UserID = userID;
                        return true;
                    }
                }

                return false;
            }

            public bool CreateUser(User user)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = "INSERT INTO user (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }

            public int GetUserID(string email)
            {
                User user = GetUserByEmail(email);

                if (user != null)
                {
                    return user.UserID;
                }

                return -1; 
            }

            public bool CreateWalletValidate(Wallet newWallet)
            {
                Wallet existingWallet = GetWalletByUserIDAndType(newWallet.UserID, newWallet.WalletType, newWallet.WalletName);
                if (existingWallet != null)
                {
                    return false;
                }

                return CreateWallet(newWallet);
            }



            public bool Transfer(int sourceWalletId, string sourceCategory, int targetWalletId, string targetCategory, float amount, int userId, string description)
            {
                string _connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SqlDataAccessLayer _dataAccessLayer = new SqlDataAccessLayer(_connectionString);
                Wallet sourceWallet = _dataAccessLayer.GetWallet(sourceWalletId);
                Wallet targetWallet = (sourceWalletId == targetWalletId) ? sourceWallet : _dataAccessLayer.GetWallet(targetWalletId);

                if (sourceWallet == null || targetWallet == null)
                {
                    throw new Exception("Source or target wallet does not exist.");
                }

                if (sourceWallet.WalletID == targetWallet.WalletID && sourceCategory == targetCategory)
                {
                    throw new Exception("Cannot transfer within the same wallet and category.");
                }

                if ((sourceCategory == "Spending" && sourceWallet.SpendingMoney < amount) ||
                    (sourceCategory == "Savings" && sourceWallet.SavingsMoney < amount))
                {
                    throw new Exception("Not enough balance in source wallet's " + sourceCategory + ".");
                }

                if (sourceCategory == "Spending")
                {
                    sourceWallet.SpendingMoney -= amount;
                }
                else if (sourceCategory == "Savings")
                {
                    sourceWallet.SavingsMoney -= amount;
                }

                if (targetCategory == "Spending")
                {
                    targetWallet.SpendingMoney += amount;
                }
                else if (targetCategory == "Savings")
                {
                    targetWallet.SavingsMoney += amount;
                }

                Transaction withdrawal = new Transaction
                {
                    UserID = userId,
                    WalletID = sourceWalletId,
                    WalletCategory = sourceCategory,
                    TransactionType = "Transfer",
                    CategoryID = 18, 
                    Amount = -amount, 
                    Date = DateTime.Now,
                    Description = description
                };

                Transaction deposit = new Transaction
                {
                    UserID = userId,
                    WalletID = targetWalletId,
                    WalletCategory = targetCategory,
                    TransactionType = "Transfer",
                    CategoryID = 19,
                    Amount = amount,
                    Date = DateTime.Now,
                    Description = description
                };

                bool isWithdrawalAdded = _dataAccessLayer.AddTransaction(withdrawal);
                bool isDepositAdded = _dataAccessLayer.AddTransaction(deposit);

                if (!isWithdrawalAdded || !isDepositAdded)
                {
                    throw new Exception("Failed to add one or both transactions to the database.");
                }

                _dataAccessLayer.UpdateWallet(sourceWallet);
                if (sourceWalletId != targetWalletId)
                {
                    _dataAccessLayer.UpdateWallet(targetWallet);
                }

                return true;
            }

            public bool ValidateCurrentPassword(int userId, string currentPassword)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Password FROM User WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            return BCrypt.Net.BCrypt.Verify(currentPassword, storedPassword);
                        }
                    }
                }
                return false;
            }

            public bool UpdatePassword(int userId, string newPassword)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE User SET Password = @NewPassword WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NewPassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }

            public void SaveProfilePicture(int userId, byte[] imageBytes)
            {
                string query = "UPDATE User SET ProfilePicture = @ProfilePicture WHERE UserID = @UserID";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", imageBytes);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void UpdateUserProfile(int userId, string firstName, string lastName, string email, byte[] profilePicture)
            {
                string query = "UPDATE User SET FirstName = @FirstName, LastName = @LastName, Email = @Email, ProfilePicture = @ProfilePicture WHERE UserID = @UserID";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@ProfilePicture", profilePicture);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public byte[] ImageToByteArray(string imagePath)
            {
                Image image = Image.FromFile(imagePath);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }

            public Image GetProfilePicture(int userId)
            {
                string query = "SELECT ProfilePicture FROM User WHERE UserID = @UserID";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        byte[] imageBytes = cmd.ExecuteScalar() as byte[];
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                return Image.FromStream(ms);
                            }
                        }
                    }
                }
                return null;
            }

            public string GetDisplayNameById()
            {
                string displayName = "";

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "SELECT FirstName, LastName FROM user WHERE UserID = @Id;";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", GlobalData.GetUserID());

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    displayName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                                }
                                else
                                {
                                    Console.WriteLine("No user found with the ID: " + GlobalData.GetUserID());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                return displayName;
            }


            public User GetUserByEmail(string email)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM user WHERE Email = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Password = reader.GetString(reader.GetOrdinal("Password"))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }




            public Wallet GetWalletByUserIDAndType(int userID, string walletType, string walletName)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM wallet WHERE UserID = @UserID AND WalletType = @WalletType AND WalletName = @WalletName", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@WalletType", walletType);
                        cmd.Parameters.AddWithValue("@WalletName", walletName);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Wallet wallet = new Wallet
                                {
                                    UserID = reader.GetInt32("UserID"),
                                    WalletType = reader.GetString("WalletType"),
                                    WalletName = reader.GetString("WalletName"),
                                    SpendingMoney = reader.GetFloat("SpendingMoney"),
                                    SavingsMoney = reader.GetFloat("SavingsMoney")
                                };

                                return wallet;
                            }
                        }
                    }
                }

                return null;
            }


            public bool CreateWallet(Wallet newWallet)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Wallet(UserID, WalletName, WalletType, SpendingMoney, SavingsMoney) VALUES (@UserID, @WalletName, @WalletType, @SpendingMoney, @SavingsMoney)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", newWallet.UserID);
                        cmd.Parameters.AddWithValue("@WalletName", newWallet.WalletName);
                        cmd.Parameters.AddWithValue("@WalletType", newWallet.WalletType);
                        cmd.Parameters.AddWithValue("@SpendingMoney", newWallet.SpendingMoney);
                        cmd.Parameters.AddWithValue("@SavingsMoney", newWallet.SavingsMoney);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            public bool WalletExists(Wallet walletToCheck)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Wallets WHERE UserID = @UserID AND WalletType = @WalletType", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", walletToCheck.UserID);
                        cmd.Parameters.AddWithValue("@WalletType", walletToCheck.WalletType);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            public bool AddTransaction(Transaction transaction)
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Transaction (UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description) VALUES (@UserID, @WalletID, @WalletCategory, @TransactionType, @CategoryID, @Amount, @Date, @Description)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", transaction.UserID);
                        command.Parameters.AddWithValue("@WalletID", transaction.WalletID);
                        command.Parameters.AddWithValue("@WalletCategory", transaction.WalletCategory);
                        command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                        command.Parameters.AddWithValue("@CategoryID", transaction.CategoryID);
                        command.Parameters.AddWithValue("@Amount", transaction.Amount);
                        command.Parameters.AddWithValue("@Date", transaction.Date);
                        command.Parameters.AddWithValue("@Description", transaction.Description);

                        var rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }


            public List<Wallet> GetWallets()
            {
                List<Wallet> wallets = new List<Wallet>();
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("SELECT * FROM wallet WHERE UserID = @UserID", connection))
                        {
                            command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID()); 

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Wallet wallet = new Wallet
                                    {
                                        WalletID = reader.GetInt32("WalletID"),
                                        WalletName = reader.GetString("WalletName"),
                                        WalletType = reader.GetString("WalletType"),
                                        SpendingMoney = reader.GetFloat("SpendingMoney"),
                                        SavingsMoney = reader.GetFloat("SavingsMoney")
                                    };
                                    wallets.Add(wallet);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                return wallets;
            }

            public int GetWalletIdByUserIdAndName(int userId, string walletName)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT WalletID FROM Wallet WHERE UserID = @UserID AND WalletName = @WalletName";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@WalletName", walletName);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }



            public bool UpdateWallet(Wallet wallet)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = "UPDATE wallet SET SpendingMoney = @SpendingMoney, SavingsMoney = @SavingsMoney WHERE WalletID = @WalletID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SpendingMoney", wallet.SpendingMoney);
                        cmd.Parameters.AddWithValue("@SavingsMoney", wallet.SavingsMoney);
                        cmd.Parameters.AddWithValue("@WalletID", wallet.WalletID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }

            public bool CheckAndUpdateWallet(Wallet wallet)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string checkQuery = "SELECT COUNT(1) FROM wallet WHERE WalletName = @WalletName AND WalletID != @WalletID";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@WalletName", wallet.WalletName);
                        checkCmd.Parameters.AddWithValue("@WalletID", wallet.WalletID);
                        checkCmd.Parameters.AddWithValue("@WalletType", wallet.WalletType);


                        conn.Open();
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        conn.Close();

                        if (count > 0)
                        {
                            return false;
                        }
                    }

                    string updateQuery = @"
                UPDATE wallet 
                SET 
                    WalletName = @WalletName, 
                    WalletType = @WalletType, 
                    SpendingMoney = @SpendingMoney, 
                    SavingsMoney = @SavingsMoney 
                WHERE WalletID = @WalletID";

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@WalletName", wallet.WalletName);
                        updateCmd.Parameters.AddWithValue("@WalletType", wallet.WalletType);
                        updateCmd.Parameters.AddWithValue("@SpendingMoney", wallet.SpendingMoney);
                        updateCmd.Parameters.AddWithValue("@SavingsMoney", wallet.SavingsMoney);
                        updateCmd.Parameters.AddWithValue("@WalletID", wallet.WalletID);

                        conn.Open();
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }



            public Wallet GetWallet(int walletId)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM wallet WHERE WalletID = @WalletID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@WalletID", walletId);

                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Wallet
                                {
                                    WalletID = reader.GetInt32(reader.GetOrdinal("WalletID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    WalletName = reader.GetString(reader.GetOrdinal("WalletName")),
                                    SpendingMoney = reader.GetFloat(reader.GetOrdinal("SpendingMoney")),
                                    SavingsMoney = reader.GetFloat(reader.GetOrdinal("SavingsMoney")),
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            public DataTable GetAllCategories()
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category", conn);
                    var dataTable = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }

            public DataTable GetIncomeCategories()
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Income' ", conn);
                    var dataTable = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }

            public DataTable GetExpenseCategories()
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Expense' ", conn);
                    var dataTable = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
            public DataTable GetTransferCategories()
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Transfer' ", conn);
                    var dataTable = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }

            public void SaveBudget(Budget budget)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();

                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                            string insertBudgetQuery = @"
                    INSERT INTO Budget (UserID, BudgetName, TotalAmount, IsActive, PeriodType, StartDate, EndDate) 
                    VALUES (@UserID, @BudgetName, @TotalAmount, @IsActive, @PeriodType, @StartDate, @EndDate)";

                            MySqlCommand cmd = new MySqlCommand(insertBudgetQuery, conn, transaction);
                            cmd.Parameters.AddWithValue("@UserID", budget.UserID);
                            cmd.Parameters.AddWithValue("@BudgetName", budget.BudgetName);
                            cmd.Parameters.AddWithValue("@TotalAmount", budget.TotalAmount);
                            cmd.Parameters.AddWithValue("@IsActive", budget.IsActive); 
                            cmd.Parameters.AddWithValue("@PeriodType", budget.PeriodType);
                            cmd.Parameters.AddWithValue("@StartDate", budget.StartDate);
                            cmd.Parameters.AddWithValue("@EndDate", budget.EndDate);
                            cmd.ExecuteNonQuery();

                            long budgetId = cmd.LastInsertedId;

                            foreach (int categoryId in budget.CategoryIds)
                            {
                                string insertCategoryQuery = @"
                        INSERT INTO BudgetCategory (BudgetID, CategoryID) 
                        VALUES (@BudgetID, @CategoryID)";

                                MySqlCommand categoryCmd = new MySqlCommand(insertCategoryQuery, conn, transaction);
                                categoryCmd.Parameters.AddWithValue("@BudgetID", budgetId);
                                categoryCmd.Parameters.AddWithValue("@CategoryID", categoryId);
                                categoryCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Database error: " + ex.Message);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General error: " + ex.Message);
                        throw;
                    }
                }
            }

            public void DeleteBudget(int budgetId)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();

                  
                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                        
                            string deleteCategoriesQuery = "DELETE FROM BudgetCategory WHERE BudgetID = @BudgetID";
                            MySqlCommand categoryCmd = new MySqlCommand(deleteCategoriesQuery, conn, transaction);
                            categoryCmd.Parameters.AddWithValue("@BudgetID", budgetId);
                            categoryCmd.ExecuteNonQuery();

                            string deleteBudgetQuery = "DELETE FROM Budget WHERE BudgetID = @BudgetID";
                            MySqlCommand cmd = new MySqlCommand(deleteBudgetQuery, conn, transaction);
                            cmd.Parameters.AddWithValue("@BudgetID", budgetId);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Database error: " + ex.Message);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General error: " + ex.Message);
                        throw;
                    }
                }
            }


            public Wallet GetWalletById(int walletId)
            {
                Wallet wallet = null;
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT WalletID, UserID, WalletName, WalletType, SpendingMoney, SavingsMoney FROM Wallet WHERE WalletID = @WalletID", conn);
                    cmd.Parameters.AddWithValue("@WalletID", walletId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            wallet = new Wallet
                            {
                                WalletID = reader.GetInt32("WalletID"),
                                UserID = reader.GetInt32("UserID"),
                                WalletName = reader.IsDBNull(reader.GetOrdinal("WalletName")) ? null : reader.GetString("WalletName"),
                                WalletType = reader.IsDBNull(reader.GetOrdinal("WalletType")) ? null : reader.GetString("WalletType"),
                                SpendingMoney = reader.IsDBNull(reader.GetOrdinal("SpendingMoney")) ? 0 : reader.GetFloat("SpendingMoney"),
                                SavingsMoney = reader.IsDBNull(reader.GetOrdinal("SavingsMoney")) ? 0 : reader.GetFloat("SavingsMoney")
                            };
                        }
                    }
                }
                return wallet;
            }

            public Wallet GetTotalBalancesForAllWallets()
            {
                Wallet totalWallet = new Wallet
                {
                    WalletID = 0,
                    UserID = 0,
                    WalletName = "All Wallets",
                    WalletType = "",
                    SpendingMoney = 0,
                    SavingsMoney = 0
                };

                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT SUM(SpendingMoney) AS TotalSpending, SUM(SavingsMoney) AS TotalSavings FROM Wallet WHERE UserID  = @UserID", conn);
                    cmd.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                    using (var reader = cmd.ExecuteReader())
                    {
                    

                        if (reader.Read())
                        {
                            totalWallet.SpendingMoney = reader.IsDBNull(reader.GetOrdinal("TotalSpending")) ? 0 : reader.GetFloat("TotalSpending");
                            totalWallet.SavingsMoney = reader.IsDBNull(reader.GetOrdinal("TotalSavings")) ? 0 : reader.GetFloat("TotalSavings");
                        }
                    }
                }
                return totalWallet;
            }



            public List<Transaction> GetAllTransactions()
            {
                List<Transaction> transactions = new List<Transaction>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT TransactionID, UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description FROM Transaction WHERE UserID =  @UserID ORDER BY Date DESC, TransactionID DESC", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    UserID = reader.GetInt32("UserID"),
                                    WalletID = reader.GetInt32("WalletID"),
                                    WalletCategory = reader.GetString("WalletCategory"),
                                    TransactionType = reader.GetString("TransactionType"),
                                    CategoryID = reader.GetInt32("CategoryID"),
                                    Amount = reader.GetFloat("Amount"),
                                    Date = reader.GetDateTime("Date"),
                                    Description = reader.GetString("Description")
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
                return transactions;
            }

            public List<Transaction> GetLastTransactionsForAllWallets(int count)
            {
                List<Transaction> transactions = new List<Transaction>();
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand($"SELECT * FROM Transaction WHERE UserID = @UserID ORDER BY Date DESC, TransactionID DESC LIMIT {count}", conn);
                    cmd.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new Transaction
                            {
                                TransactionID = reader.GetInt32("TransactionID"),
                                WalletID = reader.GetInt32("WalletID"),
                                CategoryID = reader.GetInt32("CategoryID"),
                                WalletCategory = reader.GetString("WalletCategory"),
                                Amount = reader.GetFloat("Amount"),
                                Description = reader.GetString("Description"),
                                TransactionType = reader.GetString("TransactionType"),
                                Date = reader.GetDateTime("Date")
                            });
                        }
                    }
                }
                return transactions;
            }


            public List<Transaction> GetTransactionsByWalletId(int walletId, int count)
            {
                List<Transaction> transactions = new List<Transaction>();
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand($"SELECT * FROM Transaction WHERE WalletID = @WalletID ORDER BY Date DESC, TransactionID DESC LIMIT {count}", conn);
                    cmd.Parameters.AddWithValue("@WalletID", walletId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new Transaction
                            {
                                TransactionID = reader.GetInt32("TransactionID"),
                                WalletID = reader.GetInt32("WalletID"),
                                CategoryID = reader.GetInt32("CategoryID"),
                                Amount = reader.GetFloat("Amount"),
                                Description = reader.GetString("Description"),
                                TransactionType = reader.GetString("TransactionType"),
                                Date = reader.GetDateTime("Date")
                            });
                        }
                    }
                }
                return transactions;
            }






            public List<Transaction> GetTransactionsByCategoryAndDate(List<int> categoryIds, DateTime startDate, DateTime endDate)
            {
                List<Transaction> transactions = new List<Transaction>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string categories = string.Join(",", categoryIds);
                    if (categories == "") 
                    {
                        return transactions;
                    }

                    string query = $"SELECT * FROM `Transaction` WHERE UserID = @UserID AND CategoryID IN ({categories}) AND `Date` >= @StartDate AND `Date` <= @EndDate";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    UserID = reader.GetInt32("UserID"),
                                    WalletID = reader.GetInt32("WalletID"),
                                    WalletCategory = reader.GetString("WalletCategory"),
                                    TransactionType = reader.GetString("TransactionType"),
                                    CategoryID = reader.GetInt32("CategoryID"),
                                    Amount = reader.GetFloat("Amount"),
                                    Date = reader.GetDateTime("Date"),
                                    Description = reader.GetString("Description")
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
                return transactions;
            }

            public List<int> GetCategoryIdsByBudgetId(int budgetId)
            {
                List<int> categoryIds = new List<int>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT CategoryID FROM BudgetCategory WHERE BudgetID = @BudgetID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BudgetID", budgetId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int categoryId = reader.GetInt32("CategoryID");
                                categoryIds.Add(categoryId);
                            }
                        }
                    }
                }
                return categoryIds;
            }


            public Dictionary<int, float> GetCategoryExpensesByDate(List<int> categoryIds, DateTime startDate, DateTime endDate)
            {
                Dictionary<int, float> categoryTotals = new Dictionary<int, float>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string categories = string.Join(",", categoryIds);
                    if (categories == "") 
                    {
                        return categoryTotals; 
                    }

                    string query = "SELECT CategoryID, SUM(Amount) AS TotalAmount FROM `Transaction` WHERE UserID = @UserID AND CategoryID IN (" + categories + ") AND `Date` >= @StartDate AND `Date` <= @EndDate GROUP BY CategoryID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int categoryId = reader.GetInt32("CategoryID");
                                float totalAmount = reader.GetFloat("TotalAmount");
                                categoryTotals[categoryId] = totalAmount;
                            }
                        }
                    }
                }
                return categoryTotals;
            }







            public List<Transaction> GetLatestThreeTransactions()
            {
                List<Transaction> transactions = new List<Transaction>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT TransactionID, UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description FROM Transaction WHERE UserID = @UserID ORDER BY Date DESC, TransactionID DESC LIMIT 3", connection))
                    {

                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    UserID = reader.GetInt32("UserID"),
                                    WalletID = reader.GetInt32("WalletID"),
                                    WalletCategory = reader.GetString("WalletCategory"),
                                    TransactionType = reader.GetString("TransactionType"),
                                    CategoryID = reader.GetInt32("CategoryID"),
                                    Amount = reader.GetFloat("Amount"),
                                    Date = reader.GetDateTime("Date"),
                                    Description = reader.GetString("Description")
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
                return transactions;
            }


            public List<Transaction> GetLatestThreeWalletTransactions(int walletId)
            {
                List<Transaction> transactions = new List<Transaction>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT TransactionID, UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description FROM Transaction WHERE WalletID = @WalletID ORDER BY Date DESC, TransactionID DESC LIMIT 3", connection))
                    {
                        command.Parameters.AddWithValue("@WalletID", walletId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    UserID = reader.GetInt32("UserID"),
                                    WalletID = reader.GetInt32("WalletID"),
                                    WalletCategory = reader.GetString("WalletCategory"),
                                    TransactionType = reader.GetString("TransactionType"),
                                    CategoryID = reader.GetInt32("CategoryID"),
                                    Amount = reader.GetFloat("Amount"),
                                    Date = reader.GetDateTime("Date"),
                                    Description = reader.GetString("Description")
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
                return transactions;
            }

        

            public List<Transaction> GetAllFilteredTransactions(string transactionType = null, string category = null, string wallet = null, string walletCategory = null, DateTime? startDate = null, DateTime? endDate = null)
            {
                List<Transaction> transactions = new List<Transaction>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    StringBuilder query = new StringBuilder("SELECT TransactionID, UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description FROM Transaction WHERE 1=1");

                    if (!string.IsNullOrEmpty(transactionType))
                        query.Append(" AND TransactionType = @TransactionType");
                    if (!string.IsNullOrEmpty(category))
                        query.Append(" AND CategoryID = @CategoryID");
                    if (!string.IsNullOrEmpty(wallet))
                        query.Append(" AND WalletID = @WalletID");
                    if (!string.IsNullOrEmpty(walletCategory))
                        query.Append(" AND WalletCategory = @WalletCategory");
                    if (startDate.HasValue)
                        query.Append(" AND Date >= @StartDate");
                    if (endDate.HasValue)
                        query.Append(" AND Date <= @EndDate");

                    query.Append(" ORDER BY Date DESC, TransactionID");

                

                    using (MySqlCommand command = new MySqlCommand(query.ToString(), connection))
                    {
                        if (!string.IsNullOrEmpty(transactionType))
                            command.Parameters.AddWithValue("@TransactionType", transactionType);
                        if (!string.IsNullOrEmpty(category))
                            command.Parameters.AddWithValue("@CategoryID", category);
                        if (!string.IsNullOrEmpty(wallet))
                            command.Parameters.AddWithValue("@WalletID", wallet);
                        if (!string.IsNullOrEmpty(walletCategory))
                            command.Parameters.AddWithValue("@WalletCategory", walletCategory);
                        if (startDate.HasValue)
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                        if (endDate.HasValue)
                            command.Parameters.AddWithValue("@EndDate", endDate.Value);

                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    UserID = reader.GetInt32("UserID"),
                                    WalletID = reader.GetInt32("WalletID"),
                                    WalletCategory = reader.GetString("WalletCategory"),
                                    TransactionType = reader.GetString("TransactionType"),
                                    CategoryID = reader.GetInt32("CategoryID"),
                                    Amount = reader.GetFloat("Amount"),
                                    Date = reader.GetDateTime("Date"),
                                    Description = reader.GetString("Description")
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
                return transactions;
            }




            public string GetWalletNameById(int walletId)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT WalletName FROM Wallet WHERE UserID = @UserID AND WalletID = @WalletID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        command.Parameters.AddWithValue("@WalletID", walletId);
                        var result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "Unknown Wallet";
                    }
                }
            }


            public Transaction GetTransactionById(int transactionId)
            {
                Transaction transaction = null;
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT TransactionID, UserID, WalletID, CategoryID, WalletCategory, TransactionType, Amount, Date, Description FROM Transaction WHERE TransactionID = @TransactionID", conn);
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transaction = new Transaction
                            {
                                TransactionID = reader.GetInt32("TransactionID"),
                                UserID = reader.GetInt32("UserID"),
                                WalletID = reader.GetInt32("WalletID"),
                                CategoryID = reader.GetInt32("CategoryID"),
                                WalletCategory = reader.GetString("WalletCategory"),
                                TransactionType = reader.GetString("TransactionType"),
                                Amount = reader.GetFloat("Amount"),
                                Date = reader.GetDateTime("Date"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description")
                            };
                        }
                    }
                }
                return transaction;
            }

            public Dictionary<DateTime, float> GetBudgetDailyExpenses(int budgetId, DateTime startDate, DateTime endDate)
            {
                var dailyExpenses = new Dictionary<DateTime, float>();
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                SELECT DATE(date) AS ExpenseDate, SUM(Amount) AS TotalSpent
                FROM Transaction
                WHERE TransactionType = 'expense' 
                    AND CategoryId IN (SELECT CategoryId FROM BudgetCategory WHERE BudgetId = @BudgetId)
                    AND Date BETWEEN @StartDate AND @EndDate
                GROUP BY DATE(Date)
                ORDER BY ExpenseDate;
            ";
                    sql += " AND UserID = @UserID";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        cmd.Parameters.AddWithValue("@BudgetId", budgetId);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime(reader.GetOrdinal("ExpenseDate"));
                                float totalSpent = reader.GetFloat(reader.GetOrdinal("TotalSpent"));
                                dailyExpenses.Add(date, totalSpent);
                            }
                        }
                    }
                }
                return dailyExpenses;
            }


            public string GetCategoryNameById(int categoryId)
            {
                string categoryName = "";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT Name FROM Category WHERE CategoryID = @CategoryID", conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoryName = reader.GetString("Name");
                        }
                    }
                }
                return categoryName;
            }

            public void UpdateTransaction(Transaction transaction)
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("UPDATE transaction SET UserID=@UserID, WalletID=@WalletID, CategoryID=@CategoryID, WalletCategory=@WalletCategory, TransactionType=@TransactionType, Amount=@Amount, Date=@Date, Description=@Description WHERE TransactionID=@TransactionID", conn);

                    cmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                    cmd.Parameters.AddWithValue("@UserID", transaction.UserID);
                    cmd.Parameters.AddWithValue("@WalletID", transaction.WalletID);
                    cmd.Parameters.AddWithValue("@CategoryID", transaction.CategoryID);
                    cmd.Parameters.AddWithValue("@WalletCategory", transaction.WalletCategory);
                    cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@Date", transaction.Date);
                    cmd.Parameters.AddWithValue("@Description", transaction.Description);

                    cmd.ExecuteNonQuery();
                }
            }

            public void DeleteTransaction(int transactionId)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM transaction     WHERE TransactionID = @TransactionID";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", transactionId);

                        try
                        {
                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                Console.WriteLine("Transaction successfully deleted.");
                            }
                            else
                            {
                                Console.WriteLine("No transaction found with the specified ID.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                    }
                }
            }

            public float CalculateTotalSavingsForToday()
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                float totalSavings = 0;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT GREATEST(0,
                    (SELECT IFNULL(SUM(Amount), 0) FROM Transaction
                        WHERE TransactionType = 'Income' AND WalletCategory = 'Savings' AND DATE(Date) = CURDATE() AND UserID = @UserID) +
                    (SELECT IFNULL(SUM(Amount), 0) FROM Transaction
                        WHERE TransactionType = 'Transfer' AND CategoryID = 19 AND WalletCategory = 'Savings' AND DATE(Date) = CURDATE() AND UserID = @UserID) +
                    (SELECT IFNULL(SUM(Amount), 0) FROM Transaction
                        WHERE TransactionType = 'Transfer' AND CategoryID = 18 AND  WalletCategory = 'Savings' AND DATE(Date) = CURDATE() AND UserID = @UserID) -
                    (SELECT IFNULL(SUM(Amount), 0) FROM Transaction
                        WHERE TransactionType = 'Expense' AND WalletCategory = 'Savings' AND DATE(Date) = CURDATE() AND UserID = @UserID)
                ) AS TotalSavingsToday";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalSavings = Convert.ToSingle(result);
                        }
                    }
                }

                return totalSavings;
            }


            public float GetTotalExpensesForToday()
            {
                float totalExpenses = 0;

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT IFNULL(SUM(Amount), 0) FROM Transaction
                    WHERE TransactionType = 'Expense' AND Date = CURDATE() AND UserID = @UserID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalExpenses = Convert.ToSingle(result);
                        }
                    }
                }

                return totalExpenses;
            }


            public Wallet GetMostUsedWallet()
            {
                Wallet mostUsedWallet = null;

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string queryFindMostUsedWalletId = @"
                    SELECT WalletID
                    FROM Transaction
                    WHERE UserID = @UserID
                    GROUP BY WalletID
                    ORDER BY COUNT(*) DESC
                    LIMIT 1";

                    int mostUsedWalletId = 0;
                    using (MySqlCommand command = new MySqlCommand(queryFindMostUsedWalletId, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            mostUsedWalletId = Convert.ToInt32(result);
                        }
                    }

                    if (mostUsedWalletId != 0)
                    {
                        string queryGetWalletDetails = $@"
                        SELECT WalletID, UserID, WalletName, WalletType, SpendingMoney, SavingsMoney
                        FROM Wallet
                        WHERE WalletID = {mostUsedWalletId}";

                        using (MySqlCommand command = new MySqlCommand(queryGetWalletDetails, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    mostUsedWallet = new Wallet
                                    {
                                        WalletID = reader.GetInt32("WalletID"),
                                        UserID = reader.GetInt32("UserID"),
                                        WalletName = reader.GetString("WalletName"),
                                        WalletType = reader.GetString("WalletType"),
                                        SpendingMoney = reader.GetFloat("SpendingMoney"),
                                        SavingsMoney = reader.GetFloat("SavingsMoney")
                                    };
                                }
                            }
                        }
                    }
                }

                return mostUsedWallet;
            }


            public bool DeleteWallet(int walletId)
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {


                            var deleteTransactionsQuery = "DELETE FROM transaction WHERE WalletID = @WalletID";
                            using (var cmd = new MySqlCommand(deleteTransactionsQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@WalletID", walletId);
                                cmd.ExecuteNonQuery();
                            }

                            var deleteWalletQuery = "DELETE FROM wallet WHERE WalletID = @WalletID";
                            using (var cmd = new MySqlCommand(deleteWalletQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@WalletID", walletId);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }


            public List<Budget> GetAllBudgets()
            {
                var budgets = new List<Classes.Budget>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT BudgetID, UserID, IsActive, BudgetName, TotalAmount, PeriodType, StartDate, EndDate FROM budget WHERE UserID = @UserID";



                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var budget = new Budget
                                {
                                    BudgetID = reader.GetInt32("BudgetID"),
                                    UserID = reader.GetInt32("UserID"),
                                    IsActive = reader.GetBoolean("IsActive"),
                                    BudgetName = reader.GetString("BudgetName"),
                                    TotalAmount = reader.GetFloat("TotalAmount"),
                                    PeriodType = reader.GetString("PeriodType"),
                                    StartDate = reader.GetDateTime("StartDate"),
                                    EndDate = reader.GetDateTime("EndDate")
                                };
                                budgets.Add(budget);
                            }
                        }
                    }
                }

                return budgets;
            }

            private void UpdateBudgetInDatabase(Budget budget)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "UPDATE budget SET IsActive = @IsActive WHERE BudgetID = @BudgetID";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IsActive", budget.IsActive);
                        cmd.Parameters.AddWithValue("@BudgetID", budget.BudgetID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void UpdateBudget(Budget budget)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE budget SET BudgetName = @BudgetName, TotalAmount = @TotalAmount, PeriodType = @PeriodType, StartDate = @StartDate, EndDate = @EndDate WHERE BudgetID = @BudgetID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BudgetName", budget.BudgetName);
                        command.Parameters.AddWithValue("@TotalAmount", budget.TotalAmount);
                        command.Parameters.AddWithValue("@PeriodType", budget.PeriodType);
                        command.Parameters.AddWithValue("@StartDate", budget.StartDate);
                        command.Parameters.AddWithValue("@EndDate", budget.EndDate);
                        command.Parameters.AddWithValue("@BudgetID", budget.BudgetID);
                        command.ExecuteNonQuery();
                    }
                }
            }


            public void UpdateBudgetStatuses()
            {
                var budgets = GetAllBudgets(); 

                foreach (var budget in budgets)
                {
                    if (DateTime.Today < budget.StartDate || DateTime.Today > budget.EndDate)
                    {
                        if (budget.IsActive)
                        {
                            budget.IsActive = false; 
                            UpdateBudgetInDatabase(budget); 
                        }
                    }
                }
            }



            public Dictionary<DateTime, float> GetDailyExpensesUnderBudget(Budget budget)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                Dictionary<DateTime, float> dailyExpenses = new Dictionary<DateTime, float>();

                DateTime startDate = budget.StartDate;
                DateTime endDate = DateTime.Today > budget.EndDate ? budget.EndDate : DateTime.Today;

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    dailyExpenses[date] = 0;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    if (budget.CategoryIds == null || budget.CategoryIds.Count == 0)
                    {
                        throw new ArgumentException("Budget must include at least one category ID.");
                    }

                    string query = $@"
            SELECT Date, IFNULL(SUM(Amount), 0) AS TotalAmount
            FROM Transaction
            WHERE UserID = @UserID AND TransactionType = 'Expense' 
                AND Date BETWEEN @StartDate AND @EndDate
                AND CategoryID IN ({string.Join(",", budget.CategoryIds)})
            GROUP BY Date
            ORDER BY Date ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("Date");
                                float amount = reader.GetFloat("TotalAmount");
                                if (dailyExpenses.ContainsKey(date))
                                {
                                    dailyExpenses[date] = amount;
                                }
                            }
                        }
                    }
                }

                return dailyExpenses;
            }





            public SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> CalculateFinancialSummaryForLast7Days(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> summary = new SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
                SELECT 
                    DATE(Date) AS TransactionDate,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalIncome,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalExpenses,
                    GREATEST(0, SUM(CASE WHEN TransactionType = 'Transfer' AND CategoryID = 19 THEN Amount ELSE 0 END) +
                             SUM(CASE WHEN TransactionType = 'Income' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END) -
                             SUM(CASE WHEN TransactionType = 'Expense' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END)) AS TotalSavings
                FROM Transaction
                WHERE Date >= CURDATE() - INTERVAL 6 DAY
            ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";   
                    }
                    baseQuery += " AND UserID = @UserID";
                    baseQuery += " GROUP BY DATE(Date) ORDER BY DATE(Date) ASC;";

                

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }

                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("TransactionDate");
                                float totalIncome = reader.GetFloat("TotalIncome");
                                float totalExpenses = reader.GetFloat("TotalExpenses");
                                float totalSavings = reader.GetFloat("TotalSavings");

                                summary[date] = (totalSavings, totalExpenses, totalIncome);
                            }
                        }
                    }
                }

                for (int i = 6; i >= 0; i--)
                {
                    DateTime date = DateTime.Today.AddDays(-i);
                    if (!summary.ContainsKey(date))
                    {
                        summary[date] = (0, 0, 0); 
                    }
                }

                return summary;
            }



            public SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> CalculateFinancialSummaryForLastMonth(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> summary = new SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
                SELECT 
                    DATE(Date) AS TransactionDate,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalIncome,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalExpenses,
                    GREATEST(0, SUM(CASE WHEN TransactionType = 'Transfer' AND CategoryID = 19 THEN Amount ELSE 0 END) +
                             SUM(CASE WHEN TransactionType = 'Income' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END) -
                             SUM(CASE WHEN TransactionType = 'Expense' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END)) AS TotalSavings
                FROM Transaction
                WHERE Date >= CURDATE() - INTERVAL 1 MONTH
            ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";
                    }

                    baseQuery += " AND UserID = @UserID";

                    baseQuery += " GROUP BY DATE(Date) ORDER BY DATE(Date) ASC;";

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("TransactionDate");
                                float totalIncome = reader.GetFloat("TotalIncome");
                                float totalExpenses = reader.GetFloat("TotalExpenses");
                                float totalSavings = reader.GetFloat("TotalSavings");

                                summary[date] = (totalSavings, totalExpenses, totalIncome);
                            }
                        }
                    }
                }

                DateTime startDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                for (int i = 0; i < daysInMonth; i++)
                {
                    DateTime date = startDate.AddDays(i);
                    if (!summary.ContainsKey(date))
                    {
                        summary[date] = (0, 0, 0); 
                    }
                }

                return summary;
            }

            public SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> CalculateFinancialSummaryForLastYear(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)> summary =
                    new SortedDictionary<DateTime, (float totalSavings, float totalExpenses, float totalIncome)>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
                SELECT 
                    DATE_FORMAT(Date, '%Y-%m-01') AS MonthStart,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalIncome,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalExpenses,
                    GREATEST(0, SUM(CASE WHEN TransactionType = 'Transfer' AND CategoryID = 19 THEN Amount ELSE 0 END) +
                             SUM(CASE WHEN TransactionType = 'Income' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END) -
                             SUM(CASE WHEN TransactionType = 'Expense' AND WalletCategory = 'Savings' THEN Amount ELSE 0 END)) AS TotalSavings
                FROM Transaction
                WHERE Date >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR)
            ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";
                    }

                    baseQuery += " AND UserID = @UserID";

                    baseQuery += " GROUP BY MonthStart ORDER BY MonthStart;";

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }

                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime monthStart = reader.GetDateTime("MonthStart");
                                float totalIncome = reader.GetFloat("TotalIncome");
                                float totalExpenses = reader.GetFloat("TotalExpenses");
                                float totalSavings = reader.GetFloat("TotalSavings");

                                summary[monthStart] = (totalSavings, totalExpenses, totalIncome);
                            }
                        }
                    }
                }

                DateTime startDate = DateTime.Today.AddMonths(-11).AddDays(-DateTime.Today.Day + 1);
                for (int i = 0; i < 12; i++)
                {
                    DateTime monthStart = startDate.AddMonths(i);
                    if (!summary.ContainsKey(monthStart))
                    {
                        summary[monthStart] = (0, 0, 0);
                    }
                }

                return summary;
            }

            public SortedDictionary<string, float> GetExpenseCategoriesLast7Days(int walletId)
            {
                var expenses = new SortedDictionary<string, float>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
            SELECT c.Name AS CategoryName, SUM(t.Amount) AS TotalAmount
            FROM Transaction t
            JOIN Category c ON t.CategoryID = c.CategoryId
            WHERE t.TransactionType = 'Expense' AND t.Date >= CURDATE() - INTERVAL 6 DAY
        ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND t.WalletID = @WalletID";
                    }
                    baseQuery += " AND UserID = @UserID";
                    baseQuery += " GROUP BY c.Name;";

                    using (var command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["CategoryName"].ToString();
                                float totalAmount = float.Parse(reader["TotalAmount"].ToString());
                                expenses[category] = totalAmount;
                            }
                        }
                    }
                }

                return expenses;
            }



            public SortedDictionary<string, float> GetExpenseCategoriesLast30Days(int walletId)
            {
                var expenses = new SortedDictionary<string, float>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
            SELECT c.Name AS CategoryName, SUM(t.Amount) AS TotalAmount
            FROM Transaction t
            JOIN Category c ON t.CategoryID = c.CategoryId
            WHERE t.TransactionType = 'Expense' AND t.Date >= CURDATE() - INTERVAL 29 DAY
        ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND t.WalletID = @WalletID";
                    }

                    baseQuery += " AND UserID = @UserID";
                    baseQuery += " GROUP BY c.Name;";

                    using (var command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }

                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["CategoryName"].ToString();
                                float totalAmount = float.Parse(reader["TotalAmount"].ToString());
                                expenses[category] = totalAmount;
                            }
                        }
                    }
                }

                return expenses;
            }



            public SortedDictionary<string, float> GetExpenseCategoriesLastYear(int walletId)
            {
                var expenses = new SortedDictionary<string, float>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
            SELECT c.Name AS CategoryName, SUM(t.Amount) AS TotalAmount
            FROM Transaction t
            JOIN Category c ON t.CategoryID = c.CategoryId
            WHERE t.TransactionType = 'Expense' AND t.Date >= CURDATE() - INTERVAL 365 DAY
        ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND t.WalletID = @WalletID";
                    }
                    baseQuery += " AND UserID = @UserID";
                    baseQuery += " GROUP BY c.Name;";

                    using (var command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["CategoryName"].ToString();
                                float totalAmount = float.Parse(reader["TotalAmount"].ToString());
                                expenses[category] = totalAmount;
                            }
                        }
                    }
                }

                return expenses;
            }


            public SortedDictionary<DateTime, float> CalculateNetWorthOver7Days()
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, float> netWorthSummary = new SortedDictionary<DateTime, float>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
            SELECT 
                TransactionDate,
                SUM(TotalSavings) OVER (ORDER BY TransactionDate) AS CumulativeSavings,
                SUM(TotalSpending) OVER (ORDER BY TransactionDate) AS CumulativeSpending
            FROM (
                SELECT 
                    DATE(Date) AS TransactionDate,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalSavings,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalSpending
                FROM Transaction
                WHERE Date >= CURDATE() - INTERVAL 6 DAY
                GROUP BY DATE(Date)
            ) AS DailyTotals
            ORDER BY TransactionDate ASC
            ";
                    query += " AND UserID = @UserID;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("TransactionDate");
                                float cumulativeSavings = reader.GetFloat("CumulativeSavings");
                                float cumulativeSpending = reader.GetFloat("CumulativeSpending");

                                float dailyNetWorth = cumulativeSavings - cumulativeSpending;
                                netWorthSummary[date] = dailyNetWorth;
                            }
                        }
                    }
                }

                for (int i = 6; i >= 0; i--)
                {
                    DateTime date = DateTime.Today.AddDays(-i);
                    if (!netWorthSummary.ContainsKey(date))
                    {
                        float previousNetWorth = i < 6 ? netWorthSummary[DateTime.Today.AddDays(-(i + 1))] : 0f;
                        netWorthSummary[date] = previousNetWorth; 
                    }
                }

                return netWorthSummary;
            }



            public SortedDictionary<DateTime, float> CalculateNetWorthOver7Days(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, float> netWorthSummary = new SortedDictionary<DateTime, float>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
            WITH CumulativeData AS (
                SELECT 
                    DATE(Date) AS TransactionDate,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalSavings,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalSpending
                FROM Transaction
                WHERE UserID = @UserID
                ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";
                    }

                    baseQuery += @"
                GROUP BY DATE(Date)
            ),
            CumulativeSums AS (
                SELECT
                    TransactionDate,
                    SUM(TotalSavings) OVER (ORDER BY TransactionDate) AS CumulativeSavings,
                    SUM(TotalSpending) OVER (ORDER BY TransactionDate) AS CumulativeSpending
                FROM CumulativeData
            )
            SELECT 
                TransactionDate,
                CumulativeSavings,
                CumulativeSpending
            FROM CumulativeSums
            WHERE TransactionDate >= CURDATE() - INTERVAL 7 DAY
            ORDER BY TransactionDate ASC;
            ";

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("TransactionDate");
                                float cumulativeSavings = reader.GetFloat("CumulativeSavings");
                                float cumulativeSpending = reader.GetFloat("CumulativeSpending");

                                float dailyNetWorth = cumulativeSavings - cumulativeSpending;
                                netWorthSummary[date] = dailyNetWorth;
                            }
                        }
                    }
                }

                DateTime startDate = DateTime.Today.AddDays(-7);
                for (int i = 0; i <= 7; i++)
                {
                    DateTime date = startDate.AddDays(i);
                    if (!netWorthSummary.ContainsKey(date))
                    {
                        float previousNetWorth = i > 0 ? netWorthSummary[date.AddDays(-1)] : 0f;
                        netWorthSummary[date] = previousNetWorth; 
                    }
                }

                return netWorthSummary;
            }



            public SortedDictionary<DateTime, float> CalculateNetWorthOver1Month(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, float> netWorthSummary = new SortedDictionary<DateTime, float>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
                SELECT 
                    TransactionDate,
                    SUM(TotalSavings) OVER (ORDER BY TransactionDate) AS CumulativeSavings,
                    SUM(TotalSpending) OVER (ORDER BY TransactionDate) AS CumulativeSpending
                FROM (
                    SELECT 
                        DATE(Date) AS TransactionDate,
                        SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalSavings,
                        SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalSpending
                    FROM Transaction
                    WHERE Date >= CURDATE() - INTERVAL 30 DAY
            ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";
                    }

                    baseQuery += " AND UserID = @UserID";
                

                    baseQuery += " GROUP BY DATE(Date)) AS DailyTotals ORDER BY TransactionDate ASC;";

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("TransactionDate");
                                float cumulativeSavings = reader.GetFloat("CumulativeSavings");
                                float cumulativeSpending = reader.GetFloat("CumulativeSpending");

                                float dailyNetWorth = cumulativeSavings - cumulativeSpending;
                                netWorthSummary[date] = dailyNetWorth;
                            }
                        }
                    }
                }

                DateTime startDate = DateTime.Today.AddDays(-30);
                for (int i = 0; i <= 30; i++)
                {
                    DateTime date = startDate.AddDays(i);
                    if (!netWorthSummary.ContainsKey(date))
                    {
                        float previousNetWorth = i > 0 ? netWorthSummary[date.AddDays(-1)] : 0f;
                        netWorthSummary[date] = previousNetWorth; 
                    }
                }

                return netWorthSummary;
            }
            public SortedDictionary<DateTime, float> CalculateNetWorthOver12Months(int walletId)
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=123Database;database=wallet_manager";
                SortedDictionary<DateTime, float> netWorthSummary = new SortedDictionary<DateTime, float>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string baseQuery = @"
                SELECT 
                    YEAR(Date) AS Year,
                    MONTH(Date) AS Month,
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalSavings,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalSpending
                FROM Transaction
                WHERE Date >= CURDATE() - INTERVAL 1 YEAR
            ";

                    if (walletId != 0)
                    {
                        baseQuery += " AND WalletID = @WalletID";
                    }

                    baseQuery += " AND UserID = @UserID GROUP BY YEAR(Date), MONTH(Date) ORDER BY Year, Month ASC;";

                    using (MySqlCommand command = new MySqlCommand(baseQuery, connection))
                    {
                        if (walletId != 0)
                        {
                            command.Parameters.AddWithValue("@WalletID", walletId);
                        }
                        command.Parameters.AddWithValue("@UserID", GlobalData.GetUserID());

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int year = reader.GetInt32("Year");
                                int month = reader.GetInt32("Month");
                                float cumulativeSavings = reader.GetFloat("TotalSavings");
                                float cumulativeSpending = reader.GetFloat("TotalSpending");

                                DateTime firstDayOfMonth = new DateTime(year, month, 1);
                                float monthlyNetWorth = cumulativeSavings - cumulativeSpending;
                                netWorthSummary[firstDayOfMonth] = monthlyNetWorth;
                            }
                        }
                    }
                }

                DateTime startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-12);
                for (int i = 0; i <= 12; i++)
                {
                    DateTime month = startMonth.AddMonths(i);   
                    if (!netWorthSummary.ContainsKey(month))
                    {
                        float previousNetWorth = i > 0 ? netWorthSummary[month.AddMonths(-1)] : 0f;
                        netWorthSummary[month] = previousNetWorth; 
                    }
                }

                return netWorthSummary;
            }



        }


    }

