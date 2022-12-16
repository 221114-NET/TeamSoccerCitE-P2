using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Customer
    {
        public string Email {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        // Add property for profile picture... byte array?!

        public Customer() {}
        public Customer(string email, string username, string password) {
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}