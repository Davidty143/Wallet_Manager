﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallet_Manager.Classes
{
    public class Budget
    {
        public int BudgetID { get; set; } 
        public int UserID { get; set; } 
        public string BudgetName { get; set; }
        public float TotalAmount { get; set; }
        public string PeriodType { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<int> CategoryIds { get; set; } 

        public Budget()
        {
            CategoryIds = new List<int>(); 
        }
        public bool Validate()
        {
            if (string.IsNullOrEmpty(BudgetName) || TotalAmount < 0 || string.IsNullOrEmpty(PeriodType) || StartDate == null || EndDate == null)
                return false;
            return true;
        }

    }
}
