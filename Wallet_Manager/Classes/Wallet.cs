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

        public Wallet()
        {
            WalletName = "";
            WalletType = "";
            SpendingMoney = 0;
            SavingsMoney = 0;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(WalletName) || string.IsNullOrEmpty(WalletType))
                return false;
            if (SpendingMoney < 0 || SavingsMoney < 0)
                return false;
            return true;
        }

    }
}
    