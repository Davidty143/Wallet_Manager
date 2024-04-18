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

                using (MySqlCommand command = new MySqlCommand("INSERT INTO Transaction (UserID, WalletID, WalletCategory, TransactionType, Category, Amount, Date, Description) VALUES (@UserID, @WalletID, @WalletCategory, @TransactionType, @Category, @Amount, @Date, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", transaction.UserID);
                    command.Parameters.AddWithValue("@WalletID", transaction.WalletID);
                    command.Parameters.AddWithValue("@WalletCategory", transaction.WalletCategory);
                    command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                    command.Parameters.AddWithValue("@Category", transaction.Category);
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

    }
}
