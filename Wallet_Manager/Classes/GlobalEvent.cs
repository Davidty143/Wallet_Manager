using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    public static class GlobalEvents
    {
        public delegate void TransactionUpdateHandler();
        public static event TransactionUpdateHandler TransactionUpdated;
        public delegate void ProfileInformationHandler();
        public static event ProfileInformationHandler ProfileInformationUpdated;

        public delegate void BudgetUpdatehandler();
        public static event BudgetUpdatehandler BudgetUpdated;

        public static void OnTransactionUpdated()
        {
            TransactionUpdated?.Invoke();
        }

        public static void OnProfileInformationUpdated()
        {
            ProfileInformationUpdated?.Invoke();
        }

        public static void onBudgetUpdated()
        {
            BudgetUpdated?.Invoke();
        }

    }
}
