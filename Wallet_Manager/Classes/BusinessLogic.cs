using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Wallet_Manager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Wallet_Manager.Classes
{
    internal class BusinessLogic
    {
        private IDataAccessLayer _dataAccessLayer;


        public BusinessLogic(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public bool CreateAccount(string firstName, string lastName, string email, string password)
        {
            // Check if a user with the same email already exists
            User existingUser = _dataAccessLayer.GetUserByEmail(email);
            if (existingUser != null)
            {
                // A user with the same email already exists
                return false;
            }

            // Validate input, etc.

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = hashedPassword
            };

            return _dataAccessLayer.CreateUser(newUser);
        }

        public bool AuthenticateUser(string email, string password)
        {
            User user = _dataAccessLayer.GetUserByEmail(email);

            if (user != null)
            {
                // Compare the hashed password with the provided password
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
                // Get the user ID
                int userID = GetUserID(email);

                // Check if the user ID is valid
                if (userID != -1)
                {
                    // Set the global user ID
                    GlobalData.UserID = userID;
                    return true;
                }
            }

            return false;
        }



        public int GetUserID(string email)
        {
            User user = _dataAccessLayer.GetUserByEmail(email);

            if (user != null)
            {
                return user.UserID;
            }

            return -1; // or throw an exception
        }


        public bool CreateWallet(Wallet newWallet)
        {
            // Check if a wallet of the same type already exists for the user
            Wallet existingWallet = _dataAccessLayer.GetWalletByUserIDAndType(newWallet.UserID, newWallet.WalletType);
            if (existingWallet != null)
            {
                // A wallet of the same type already exists for the user
                return false;
            }

            // If no such wallet exists, create the new wallet
            return _dataAccessLayer.CreateWallet(newWallet);
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

            // Check for sufficient funds in the source wallet
            if ((sourceCategory == "Spending" && sourceWallet.SpendingMoney < amount) ||
                (sourceCategory == "Savings" && sourceWallet.SavingsMoney < amount))
            {
                throw new Exception("Not enough balance in source wallet's " + sourceCategory + ".");
            }

            // Deduct from the source category
            if (sourceCategory == "Spending")
            {
                sourceWallet.SpendingMoney -= amount;
            }
            else if (sourceCategory == "Savings")
            {
                sourceWallet.SavingsMoney -= amount;
            }

            // Add to the target category
            if (targetCategory == "Spending")
            {
                targetWallet.SpendingMoney += amount;
            }
            else if (targetCategory == "Savings")
            {
                targetWallet.SavingsMoney += amount;
            }

            // Create and record the transactions
            Transaction withdrawal = new Transaction
            {
                UserID = userId,
                WalletID = sourceWalletId,
                WalletCategory = sourceCategory,
                TransactionType = "Transfer",
                CategoryID = 18, // Assuming CategoryID is predefined
                Amount = -amount, // Negative for withdrawal
                Date = DateTime.Now,
                Description = description
            };

            Transaction deposit = new Transaction
            {
                UserID = userId,
                WalletID = targetWalletId,
                WalletCategory = targetCategory,
                TransactionType = "Transfer",
                CategoryID = 19, // Assuming CategoryID is predefined
                Amount = amount,
                Date = DateTime.Now,
                Description = description
            };

            // Only proceed with adding transactions if all checks pass
            bool isWithdrawalAdded = _dataAccessLayer.AddTransaction(withdrawal);
            bool isDepositAdded = _dataAccessLayer.AddTransaction(deposit);

            if (!isWithdrawalAdded || !isDepositAdded)
            {
                throw new Exception("Failed to add one or both transactions to the database.");
            }

            // Persist the updated wallet information to the database
            _dataAccessLayer.UpdateWallet(sourceWallet);
            if (sourceWalletId != targetWalletId)
            {
                _dataAccessLayer.UpdateWallet(targetWallet);
            }

            return true;
        }






    }
}

