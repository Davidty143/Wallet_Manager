using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    public class Wallet
    {
        public int WalletID { get; set; }
        public int UserID { get; set; }

        public string WalletName{ get; set; }
        public string WalletType { get; set; }
        public float SpendingMoney { get; set; }
        public float SavingsMoney { get; set; }

        public int getWalletID()
        {
            return this.WalletID; 
        }
    }
}
    