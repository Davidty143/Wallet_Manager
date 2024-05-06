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

        public Category(int id, string name)
        {
            CategoryID = id;
            Name = name;
        }
        
        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception("Category name cannot be empty");
        }
    }
}
