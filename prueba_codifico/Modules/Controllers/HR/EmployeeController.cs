using Microsoft.AspNetCore.Mvc;
using prueba_codifico.DTO.Interfaces.HR;

namespace prueba_codifico.Modules.Controllers.HR
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet("getemployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employee.GetEmployeesAsync();
            return Ok(employees);
        }
    }
}
