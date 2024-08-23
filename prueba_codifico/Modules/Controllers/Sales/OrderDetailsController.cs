using Microsoft.AspNetCore.Mvc;
using prueba_codifico.DTO.Interfaces.Sales;
using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.Modules.Controllers.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetails _orders;

        public OrderDetailsController(IOrderDetails orders)
        {
            _orders = orders;
        }


        [HttpPost("addorder")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDto)
        {
            try
            {
                await _orders.AddOrderAsync(orderDto);
                return Ok(new { Message = "Order added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}

