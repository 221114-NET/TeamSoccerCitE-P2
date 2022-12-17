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

        public CustomerData() {
            this._connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["SoccerCitEDB"];
        }

        public async Task<Customer> PostCustomer(Customer newCustomer)
        {
            //List<Customer> list = new List<Customer>();

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

            /*while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string email = reader.GetString(1);
                string username = reader.GetString(2);
                string password = reader.GetString(3);
                //int profilePic = reader.GetInt32(4);

                Customer tmp = new Customer(email, username, password); //, profilePic
                //list.Add(tmp);
            }
            */
            await reader.ReadAsync();
            int id = reader.GetInt32(0);
            string email = reader.GetString(1);
            string username = reader.GetString(2);
            string password = reader.GetString(3);

            await connection.CloseAsync();
            //logger.LogInformation("Executed AddCustomer");

            //return list;
            //Customer registeredCustomer = new Customer(email, username, password);
            //return registeredCustomer;
            
            //This does the same thing
            return new Customer(email, username, password);
        }
    }
}