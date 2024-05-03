using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    public static class GlobalEvents
    {
        // Define a delegate and an event for transaction updates
        public delegate void TransactionUpdateHandler();
        public static event TransactionUpdateHandler TransactionUpdated;
        public delegate void ProfileInformationHandler();
        public static event ProfileInformationHandler ProfileInformationUpdated;

        // Method to call to trigger the event
        public static void OnTransactionUpdated()
        {
            TransactionUpdated?.Invoke();
        }

        public static void OnProfileInformationUpdated()
        {
            ProfileInformationUpdated?.Invoke();
        }

    }
}
