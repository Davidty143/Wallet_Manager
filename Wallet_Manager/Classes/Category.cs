using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    internal class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        // Constructor
        public Category(int id, string name)
        {
            CategoryID = id;
            Name = name;
        }
    }
}
