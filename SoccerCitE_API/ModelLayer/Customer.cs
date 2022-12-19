using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Customer
    {
        public string ?Email {get; set;}
        public string ?Username {get; set;}
        public string ?Password {get; set;}
        //public byte[] ?ImageData {get; set;}
        //option 1, base64 string?
        // Different model for user profile
        // Model that will encompass everything in the user profile...

        public Customer() {}
        public Customer(string email, string username, string password) { //, byte[] imageData
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}