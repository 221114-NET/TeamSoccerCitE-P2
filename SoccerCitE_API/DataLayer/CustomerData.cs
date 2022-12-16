using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class CustomerData : ICustomerData
    {
        private readonly string _connectionString;

        public CustomerData() {
            this._connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["SoccerCitEDB"];
        }

        public async Task<Customer> PostCustomer(Customer c) {
            Console.WriteLine(_connectionString);
            return null!;
        }
    }
}