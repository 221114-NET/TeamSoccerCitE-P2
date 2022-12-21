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
                using (SqlCommand command = new SqlCommand("SELECT ProductId, Name, Description, Price, Quantity, CategoryId, ProductImage FROM Product", connection))
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

        // TODO This is a temporary solution for adding products with images to the database
        public async Task<String> PostProduct(Product p)
        {   
            string feedback = "Post Product was unsuccessful";
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                string commandText = "INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES (@name, @description, @price, @quantity, @categoryId, @productImage);";
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
                        
                        // Execute
                        await command.ExecuteNonQueryAsync();
                        feedback = "Post Product was successful";
                    } catch (Exception ex) {
                        _logger.ErrorLog(ex);
                    }
                    return feedback;
                }
            }
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