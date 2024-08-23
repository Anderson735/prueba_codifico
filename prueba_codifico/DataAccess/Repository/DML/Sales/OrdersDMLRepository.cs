using Microsoft.Data.SqlClient;
using prueba_codifico.DTO.Interfaces.Sales;
using prueba_codifico.DTO.Models.Production;
using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DataAccess.Repository.DML.Sales
{
    public class OrdersDMLRepository : IOrders
    {
        private readonly string _connectionString;

        public OrdersDMLRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<CustomerOrderPrediction>> GetNextPredictedOrdersAsync()
        {
            var result = new List<CustomerOrderPrediction>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    WITH OrderIntervals AS (
                        SELECT 
                            O.custid,
                            O.orderdate,
                            DATEDIFF(DAY, LAG(O.orderdate) OVER(PARTITION BY O.custid ORDER BY O.orderdate), O.orderdate) AS DaysBetweenOrders
                        FROM 
                            Sales.Orders O
                    )
                    SELECT 
                        C.companyname AS [CustomerName],
                        MAX(O.orderdate) AS [LastOrderDate],
                        DATEADD(DAY, AVG(OI.DaysBetweenOrders), MAX(O.orderdate)) AS [NextPredictedOrder]
                    FROM 
                        Sales.Customers C
                    INNER JOIN 
                        Sales.Orders O ON C.custid = O.custid
                    LEFT JOIN 
                        OrderIntervals OI ON O.custid = OI.custid
                    GROUP BY 
                        C.companyname;
                    ";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var customerOrderPrediction = new CustomerOrderPrediction
                            {
                                CustomerName = reader["CustomerName"].ToString(),
                                LastOrderDate = reader.IsDBNull(reader.GetOrdinal("LastOrderDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("LastOrderDate")),
                                NextPredictedOrder = reader.IsDBNull(reader.GetOrdinal("NextPredictedOrder"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("NextPredictedOrder"))
                            };

                            result.Add(customerOrderPrediction);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<Order>> GetClientOrdersAsync(int customerId)
        {
            var orders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    Orderid,
                    Requireddate,
                    Shippeddate,
                    Shipname,
                    Shipaddress,
                    Shipcity
                FROM 
                    Sales.Orders
                WHERE 
                    custid = @CustomerId;
                ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var order = new Order
                            {
                                Orderid = reader.GetInt32(reader.GetOrdinal("Orderid")),
                                Requireddate = reader.IsDBNull(reader.GetOrdinal("Requireddate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Requireddate")),
                                Shippeddate = reader.IsDBNull(reader.GetOrdinal("Shippeddate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Shippeddate")),
                                Shipname = reader["Shipname"].ToString(),
                                Shipaddress = reader["Shipaddress"].ToString(),
                                Shipcity = reader["Shipcity"].ToString()
                            };

                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }

    }
}


