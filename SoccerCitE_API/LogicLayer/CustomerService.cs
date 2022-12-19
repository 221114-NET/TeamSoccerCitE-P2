using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using DataLayer;

namespace LogicLayer
{
    public class CustomerService : ICustomerService
    {
        // Dependency injection for data layer
        private readonly ICustomerData _iCustomerData;
        public CustomerService(ICustomerData iCustomerData) {
            this._iCustomerData = iCustomerData;
        }

        public async Task<Customer> PostCustomer(Customer c) {
            // if(c.ImageData is null) {
            //     FileInfo defaultImage = new FileInfo("../../SoccerCitEConsole/test.png");
            //     byte[] imageData = new byte[defaultImage.Length];

            //     // Put defaultImage data into byte array using filestream
            //     using(FileStream stream = defaultImage.OpenRead()) {
            //         stream.Read(imageData, 0, imageData.Length);
            //     }

            //     c.ImageData = imageData;
            // }
            
            return await _iCustomerData.PostCustomer(c);
        }
    }
}