using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
// TODO TMP, for showing that the database sends back the right data for our image...
using System.Drawing;

namespace DataLayer
{
    public class CustomerData : ICustomerData
    {
        private readonly string _connectionString;
        private readonly IDataLogger _logger;

        public CustomerData(IDataLogger logger)
        {
            this._connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["SoccerCitEDB"];
            this._logger = logger;
        }

        public async Task<Customer> PostCustomer(Customer newCustomer)
        {

            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string cmdText = "INSERT INTO Customer (email, username, password)" +
                             "VALUES (@email, @username, @password);" +
                             "SELECT customerId, email, username, password, profilePic FROM Customer WHERE email = @customerEmail;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@email", newCustomer.Email);
            cmd.Parameters.AddWithValue("@username", newCustomer.Username);
            cmd.Parameters.AddWithValue("@password", newCustomer.Password);
            cmd.Parameters.AddWithValue("@customerEmail", newCustomer.Email);
            /* 
            TODO If we want to have the user register with a profile picture every time, we will have to give a profile picture in the Logic Layer. 
            Weirdness when passing nulls.
            > Use default picture
            */
            // if(newCustomer.ImageData is null) {
            //     cmd.Parameters.AddWithValue("@image", DBNull.Value);    
            // } else cmd.Parameters.AddWithValue("@image", newCustomer.ImageData);
            // cmd.Parameters.AddWithValue("@image", newCustomer.ImageData);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();
            int id = reader.GetInt32(0);
            string email = reader.GetString(1);
            string username = reader.GetString(2);
            string password = reader.GetString(3);
            if (!reader.IsDBNull(4))
            {
                byte[] imageData = (byte[])reader[4];
            }
            //long returnedBytes = /*(byte[])*/ reader.GetBytes(4,0,imageData, 0, 65535);
            //if(imageData.Length == 0) its null

            await connection.CloseAsync();

            Customer registeredCustomer = new Customer(email, username, password); //imageData
            _logger.LogRegistration(registeredCustomer);

            // TODO TMP, These 3 lines save the image that was used in registering the user... testing if we can get our picture back from database
            // MemoryStream ms1 = new MemoryStream(registeredCustomer.ImageData);
            // Image resultImage = Image.FromStream(ms1);
            // resultImage.Save($"../../SoccerCitEConsole/{Guid.NewGuid()}.png");

            return registeredCustomer;
        }

        public async Task<Guid> LoginCustomer(Customer c)
        {
            int customerId = await GetUserIDFromDB(c);
            Guid sessionId = Guid.Empty;
            if (customerId > 0)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    //Insert new login session, then retrieve Guid
                    string commandText = "INSERT INTO LoginSession (CustomerId) VALUES (@customerId);" +
                                        "SELECT SessionId FROM LoginSession WHERE CustomerId = @customerId;";
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {

                        await connection.OpenAsync();
                        command.Parameters.AddWithValue("@customerId", customerId);
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            await reader.ReadAsync();
                            sessionId = reader.GetGuid(0);
                        }
                        catch (Exception e)
                        {

                        }
                        finally
                        {
                            await connection.CloseAsync();
                        }
                    }
                }
            }
            return sessionId;
        }

        public async Task LogoutCustomer(Guid sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM LoginSession WHERE SessionId = @sessionId", connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@sessionId", sessionId);
                    try
                    {
                        //Returns number of rows affected if we want to keep track of sucess
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception e)
                    {
                        _logger.ErrorLog(e);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }

        private async Task<int> GetUserIDFromDB(Customer c)
        {
            int customerId = -1; //If Return is -1, Email and/or PW incorrect

            //Validating that the user login information is correct, returns CustomerId if it is
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT CustomerId FROM Customer WHERE Email = @email AND Password = @password", connection))
                {
                    try
                    {
                        await connection.OpenAsync();
                        command.Parameters.AddWithValue("@email", c.Email);
                        command.Parameters.AddWithValue("@password", c.Password);
                        SqlDataReader reader = command.ExecuteReader();
                        //If we get a return, store customerId for insert in Session Table, otherwise return empty Guid
                        if (await reader.ReadAsync())
                        {
                            customerId = reader.GetInt32(0);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.ErrorLog(e);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
            return customerId;
        }
    }
}