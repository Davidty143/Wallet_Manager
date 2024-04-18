using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Forms
{
    internal class DisplayEnum : Attribute
    {
        public string DisplayName { get; }

        public DisplayEnum(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
