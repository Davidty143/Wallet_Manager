using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Wallet_Manager.Classes
{
    internal class SqlDataAccessLayer : IDataAccessLayer
    {
        private string _connectionString;   

        public SqlDataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
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

        public Wallet GetWalletByUserIDAndType(int userID, string walletType)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM wallet WHERE UserID = @UserID AND WalletType = @WalletType", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@WalletType", walletType);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Wallet wallet = new Wallet
                            {
                                UserID = reader.GetInt32("UserID"),
                                WalletType = reader.GetString("WalletType"),
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

                // Updated SQL command to use CategoryID instead of Category
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Transaction (UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description) VALUES (@UserID, @WalletID, @WalletCategory, @TransactionType, @CategoryID, @Amount, @Date, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", transaction.UserID);
                    command.Parameters.AddWithValue("@WalletID", transaction.WalletID);
                    command.Parameters.AddWithValue("@WalletCategory", transaction.WalletCategory);
                    command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                    // Updated to use CategoryID
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
                        command.Parameters.AddWithValue("@UserID", 1); // Ideally, replace 1 with a variable if needed

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
                // Log or handle the exception as needed
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return wallets;
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
                                // Add other fields as necessary
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

        public DataTable GetIncomeCategories()
        {
            using (var conn = new MySqlConnection(_connectionString)) // Ensure you use MySqlConnection
            {
                conn.Open();
                // Adjust the query to match your database schema
                var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Income' ORDER BY Name", conn);
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
                var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Expense' ORDER BY Name", conn);
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
                var cmd = new MySqlCommand("SELECT CategoryId, Name FROM Category WHERE CategoryType = 'Transfer' ORDER BY Name", conn);
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

                    // Begin a database transaction to ensure data integrity
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Insert the main budget details into the Budget table
                        string insertBudgetQuery = @"
                        INSERT INTO Budget (UserID, BudgetName, TotalAmount, PeriodType, StartDate, EndDate) 
                        VALUES (@UserID, @BudgetName, @TotalAmount, @PeriodType, @StartDate, @EndDate)";

                        MySqlCommand cmd = new MySqlCommand(insertBudgetQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", budget.UserID);
                        cmd.Parameters.AddWithValue("@BudgetName", budget.BudgetName);
                        cmd.Parameters.AddWithValue("@TotalAmount", budget.TotalAmount);
                        cmd.Parameters.AddWithValue("@PeriodType", budget.PeriodType);
                        cmd.Parameters.AddWithValue("@StartDate", budget.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", budget.EndDate);
                        cmd.ExecuteNonQuery();

                        // Retrieve the ID of the newly inserted budget
                        long budgetId = cmd.LastInsertedId;

                        // Insert category associations in BudgetCategories table
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

                        // Commit the transaction to finalize the changes
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


        public void AddGoal(Goal goal)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Goal (UserID, GoalName, TargetAmount, CurrentAmount, Deadline, WalletId) 
                             VALUES (@UserID, @GoalName, @TargetAmount, @CurrentAmount, @Deadline, @WalletId)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", goal.UserID);
                command.Parameters.AddWithValue("@GoalName", goal.GoalName);
                command.Parameters.AddWithValue("@TargetAmount", goal.TargetAmount);
                command.Parameters.AddWithValue("@CurrentAmount", goal.CurrentAmount);
                command.Parameters.AddWithValue("@Deadline", goal.Deadline);
                command.Parameters.AddWithValue("@WalletId", goal.WalletID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT TransactionID, UserID, WalletID, WalletCategory, TransactionType, CategoryID, Amount, Date, Description FROM Transaction", connection))
                {
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
                var query = "SELECT WalletName FROM Wallet WHERE WalletID = @WalletID";
                using (var command = new MySqlCommand(query, connection))
                {
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
                var query = "DELETE FROM transaction WHERE TransactionID = @TransactionID";

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





    }


}
