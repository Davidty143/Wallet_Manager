using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    internal class Goal
    {
        public int UserID { get; set; } // Unique identifier for the goal (optional based on your design)
        public string GoalName { get; set; } // Name of the goal
        public float TargetAmount { get; set; } // The financial target for the goal
        public float CurrentAmount { get; set; } // Current saved amount towards the goal
        public DateTime? Deadline { get; set; } // Optional deadline for the goal
        public int WalletID { get; set; } // ID of the wallet associated with the goal

        // Constructor without parameters

    }

}
