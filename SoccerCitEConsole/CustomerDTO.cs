using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCitEConsole
{
    public class CustomerDTO
    {
        public string ?Email {get; set;}
        public string ?Username {get; set;}
        public string ?Password {get; set;}
        // Add property for profile picture... byte array?!
        public byte[] ?ImageData {get; set;}

        public CustomerDTO() {}
        public CustomerDTO(string email, string username, string password, byte[] imageData) { //, byte[] imageData
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.ImageData = imageData;
        }
    }
}