using Microsoft.Data.SqlClient;
using prueba_codifico.DTO.Interfaces.Sales;
using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DataAccess.Repository.DML.Sales
{
    public class OrderDetailsEntityRepository : IOrderDetails
    {
        private readonly string _connectionString;

        public OrderDetailsEntityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task AddOrderAsync(OrderDTO orderDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert Order
                        var orderQuery = @"
                        INSERT INTO Sales.Orders (Empid, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry)
                        VALUES (@EmpId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @OrderDate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry);
                        SELECT SCOPE_IDENTITY();";

                        int orderId;
                        using (var command = new SqlCommand(orderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@EmpId", orderDto.EmpId);
                            command.Parameters.AddWithValue("@ShipperId", orderDto.ShipperId);
                            command.Parameters.AddWithValue("@ShipName", orderDto.ShipName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ShipAddress", orderDto.ShipAddress ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ShipCity", orderDto.ShipCity ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@OrderDate", orderDto.OrderDate);
                            command.Parameters.AddWithValue("@RequiredDate", orderDto.RequiredDate);
                            command.Parameters.AddWithValue("@ShippedDate", orderDto.ShippedDate ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Freight", orderDto.Freight);
                            command.Parameters.AddWithValue("@ShipCountry", orderDto.ShipCountry ?? (object)DBNull.Value);

                            orderId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        }

                        // Insert OrderDetails
                        var detailsQuery = @"
                        INSERT INTO Sales.OrderDetails (Orderid, Productid, Unitprice, Qty, Discount)
                        VALUES (@OrderId, @ProductId, @UnitPrice, @Qty, @Discount);";

                        using (var command = new SqlCommand(detailsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OrderId", orderId);
                            command.Parameters.AddWithValue("@ProductId", orderDto.ProductId);
                            command.Parameters.AddWithValue("@UnitPrice", orderDto.UnitPrice);
                            command.Parameters.AddWithValue("@Qty", orderDto.Qty);
                            command.Parameters.AddWithValue("@Discount", orderDto.Discount);

                            await command.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
