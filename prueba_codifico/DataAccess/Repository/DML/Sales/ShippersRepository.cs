using Microsoft.Data.SqlClient;
using prueba_codifico.DTO.Interfaces.Sales;
using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DataAccess.Repository.DML.Sales
{
    public class ShippersRepository : IShipper
    {
        private readonly string _connectionString;

        public ShippersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<Shipper>> GetAllShippersAsync()
        {
            var shippers = new List<Shipper>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    Shipperid, 
                    Companyname
                FROM 
                    Sales.Shippers";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var shipper = new Shipper
                            {
                                Shipperid = reader.GetInt32(reader.GetOrdinal("Shipperid")),
                                Companyname = reader.GetString(reader.GetOrdinal("Companyname"))
                            };

                            shippers.Add(shipper);
                        }
                    }
                }
            }

            return shippers;
        }
    }
}
