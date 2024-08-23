using Microsoft.Data.SqlClient;
using prueba_codifico.DTO.Interfaces.Production;
using prueba_codifico.DTO.Models.Production;

namespace prueba_codifico.DataAccess.Repository.DML.Production
{
    public class ProductsEntityRepository : IProducts
    {
        private readonly string _connectionString;

        public ProductsEntityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    Productid, 
                    Productname
                FROM 
                     Production.Products";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var product = new Product
                            {
                                Productid = reader.GetInt32(reader.GetOrdinal("Productid")),
                                Productname = reader.GetString(reader.GetOrdinal("Productname"))
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}

