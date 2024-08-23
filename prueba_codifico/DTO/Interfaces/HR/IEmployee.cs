using prueba_codifico.DTO.Models.HR;

namespace prueba_codifico.DTO.Interfaces.HR
{
    public interface IEmployee
    {
        Task<List<Employee>> GetEmployeesAsync();
    }
}
