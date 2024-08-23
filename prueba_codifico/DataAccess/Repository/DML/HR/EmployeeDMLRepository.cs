using Microsoft.Data.SqlClient;
using prueba_codifico.DTO.Interfaces.HR;
using prueba_codifico.DTO.Models.HR;

namespace prueba_codifico.DataAccess.Repository.DML.HR
{
    public class EmployeeDMLRepository : IEmployee
    {
        private readonly string _connectionString;

        public EmployeeDMLRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
            SELECT 
                Empid,
                (firstname + ' ' + lastname) AS FullName
            FROM 
                HR.Employees;
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var employee = new Employee
                            {
                                Empid = reader.GetInt32(reader.GetOrdinal("Empid")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName"))
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
    }
}
