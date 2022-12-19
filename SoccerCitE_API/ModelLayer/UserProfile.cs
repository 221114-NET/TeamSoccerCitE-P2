using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserProfile
    {
        // 2 fields 
        public byte[] dbImageData {get; set;}
        public string apiImageData {get; set;} // Use this when user wants to set their image from the browser
        
        public UserProfile() {}
        public UserProfile(byte[] imageBytes, string base64Info) {
            this.dbImageData = imageBytes;
            this.apiImageData = base64Info;
        }
    }
}