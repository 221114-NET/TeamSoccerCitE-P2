using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ModelLayer;

namespace DataLayer
{
    public class ProductData : IProductData
    {
        private readonly string _connectionString;
        private readonly IDataLogger _logger;

        public ProductData(IDataLogger logger)
        {
            this._connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["SoccerCitEDB"];
            this._logger = logger;
        }

        public async Task<List<Product>> GetProductList()
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Product", connection))
                {
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader sqlProducts = await command.ExecuteReaderAsync();

                        while (await sqlProducts.ReadAsync())
                        {
                            // Create product object (with image) and add it to the list
                            Product p = new Product(
                                sqlProducts.GetInt32(0),
                                sqlProducts.GetString(1),
                                sqlProducts.GetString(2),
                                sqlProducts.GetDouble(3),
                                sqlProducts.GetInt32(4),
                                (ProductCategory)sqlProducts.GetInt32(5),
                                (byte[]) sqlProducts[6]
                            );
                            productList.Add(p);
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
            return productList;
        }

        public async Task<Product> PostProduct(Product p)
        {   
            // Will we get id data?
            Product newProduct = new Product();
            int productId = p.ProductId;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                string commandText = "INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES (@name, @description, @price, @quantity, @categoryId, @productImage);";
                commandText += "SELECT ProductId, Name, Description, Price, Quantity, CategoryId, ProductImage FROM Product WHERE ProductId = @productId";
                using (SqlCommand command = new SqlCommand(commandText, connection)) {
                    try {
                        await connection.OpenAsync();
                        // Add Parameters
                        command.Parameters.AddWithValue("@name", p.Name);
                        command.Parameters.AddWithValue("@description", p.Description);
                        command.Parameters.AddWithValue("@price", p.Price);
                        command.Parameters.AddWithValue("@quantity", p.Quantity);
                        command.Parameters.AddWithValue("@categoryId", (int) p.CategoryId);
                        command.Parameters.AddWithValue("@productImage", p.ImageData);

                        command.Parameters.AddWithValue("@productId", productId);
                        
                        // Execute
                        using(SqlDataReader reader = await command.ExecuteReaderAsync()) {
                            await reader.ReadAsync();
                            newProduct = new Product(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3),
                                reader.GetInt32(4),
                                (ProductCategory)reader.GetInt32(5),
                                (byte[]) reader[6]
                            ); 
                        }
                        // Do stuff 
                    } catch (Exception ex) {
                        _logger.ErrorLog(ex);
                    }
                    return newProduct;
                }
            }
            /*
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

            await connection.CloseAsync();

            Customer registeredCustomer = new Customer(email, username, password);
            _logger.LogRegistration(registeredCustomer);

            return registeredCustomer;
        */
        }

        public async Task CartCheckout(List<Product> cart)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                string commandText = "UPDATE Product SET Quantity = Quantity - @productQuantity WHERE ProductId = @productId";
                using(SqlCommand command = new SqlCommand(commandText, connection)) {
                    try {
                        await connection.OpenAsync();
                        command.Parameters.Add("@productQuantity", SqlDbType.Int);
                        command.Parameters.Add("@productId", SqlDbType.Int);
                        foreach(Product product in cart) {
                            command.Parameters["@productQuantity"].Value = product.Quantity;
                            command.Parameters["@productId"].Value = product.ProductId;
                            await command.ExecuteNonQueryAsync(); // returns an int, # of affected rows
                        }
                    } catch(Exception ex) {
                        _logger.ErrorLog(ex);
                    }
                }
            }
        }
    }
}