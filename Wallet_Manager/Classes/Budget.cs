using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    internal class Budget
    {
        public int BudgetID { get; set; } // Primary key, auto-incremented
        public int UserID { get; set; } // Foreign key linking to the Users table
        public string BudgetName { get; set; }
        public float TotalAmount { get; set; }
        public string PeriodType { get; set; } // e.g., Monthly, Annually
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> CategoryIds { get; set; } // List of category IDs
        public Budget()
        {
            CategoryIds = new List<int>(); // Initialize the list to prevent null reference issues
        }
    }
}
