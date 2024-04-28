using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet_Manager.Forms;

namespace Wallet_Manager.Classes
{
    public static class GlobalData
    {
       public static int UserID { get; set; }
        
        public static AddTransaction AddTransactionForm { get; set; }

        public static void SetAddTransactionForm(AddTransaction addTransactionForm)
        {
            AddTransactionForm = addTransactionForm;
        }

        public static AddTransaction GetAddTransactionForm()
        {
            return AddTransactionForm;
        }

        public static void SetUserID(int userID)
        {

            UserID = userID;
        }

        public static int GetUserID()
        {
            UserID = 1;
            return UserID;
        }



    }

}
