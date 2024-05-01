using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet_Manager.Classes;

namespace Wallet_Manager
{
    internal interface IDataAccessLayer
    {
        bool CreateUser(User user);
        User GetUserByEmail(string email);


        bool CreateWallet(Wallet newWallet);
        Wallet GetWalletByUserIDAndType(int userID, string walletType, string walletName);
        bool WalletExists(Wallet walletToCheck);




    }
}
