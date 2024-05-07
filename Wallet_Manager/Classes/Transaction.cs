using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int WalletID { get; set; }
        public int CategoryID { get; set; }
        public string WalletCategory { get; set; }
        public string TransactionType { get; set; }
        
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Transaction()
        {
            UserID = 0;
            WalletID = 0;
            CategoryID = 0;
            WalletCategory = "";
            TransactionType = "";
            Amount = 0;
            Date = DateTime.Now;
            Description = "";
        }
        public bool Validate()
        {
            if (UserID < 0 || WalletID < 0 || CategoryID < 0 || string.IsNullOrEmpty(WalletCategory) || string.IsNullOrEmpty(TransactionType) || Amount < 0 || Date == null)
                return false;
            return true;
        }
        
    }
}
