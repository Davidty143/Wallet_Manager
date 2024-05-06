using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet_Manager.Classes
{
    internal class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        
        public User()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                return false;
            return true;
        }   
    }

}
