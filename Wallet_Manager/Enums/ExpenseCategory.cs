using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet_Manager.Forms;

namespace Wallet_Manager.Enums
{
    public enum ExpenseCategory
    {

        Food,
        Transportation,

        [DisplayEnum("School Fees")]
        SchoolFees,

        [DisplayEnum("Personal Care")]
        PersonalCare,

        Rent,
        Entertainment,
        Shopping,
        Health,
        Other

    }
}
