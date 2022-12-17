using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DataLayer
{
    public class CustomerData : ICustomerData
    {
        private readonly string _connectionString;
        private readonly IDataLogger _logger;

        public CustomerData(IDataLogger logger) {
            this._connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["SoccerCitEDB"];
            this._logger = logger;
        }

        public async Task<Customer> PostCustomer(Customer newCustomer)
        {

            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string cmdText = "INSERT INTO Customer (email, username, password, profilePic)" +
                             "VALUES (@email, @username, @password, NULL);" +
                             "SELECT customerId, email, username, password, profilePic FROM Customer WHERE email = @customerEmail;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@email", newCustomer.Email);
            cmd.Parameters.AddWithValue("@username", newCustomer.Username);
            cmd.Parameters.AddWithValue("@password", newCustomer.Password);
            cmd.Parameters.AddWithValue("@customerEmail", newCustomer.Email);
            //cmd.Parameters.AddWithValue("@profilePic", newCustomer.ProfilePic);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();
            int id = reader.GetInt32(0);
            string email = reader.GetString(1);
            string username = reader.GetString(2);
            string password = reader.GetString(3);

            await connection.CloseAsync();

            Customer registeredCustomer = new Customer(email, username, password);
            _logger.LogRegistration(registeredCustomer);
            return registeredCustomer;
        }
    }
}