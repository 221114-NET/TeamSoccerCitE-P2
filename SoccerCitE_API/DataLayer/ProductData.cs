using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                            //TODO: Not currently reading image column, because product model does not have image member yet
                            productList.Add(new Product(sqlProducts.GetInt32(0), sqlProducts.GetString(1), sqlProducts.GetString(2), sqlProducts.GetDouble(3),
                            sqlProducts.GetInt32(4), (ProductCategory)sqlProducts.GetInt32(5)));
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
    }
}