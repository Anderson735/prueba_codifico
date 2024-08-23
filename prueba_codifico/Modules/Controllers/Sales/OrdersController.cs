using Microsoft.AspNetCore.Mvc;
using prueba_codifico.DTO.Interfaces.Sales;

namespace prueba_codifico.Modules.Controllers.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrders _orders;

        public OrdersController(IOrders orders)
        {
            _orders = orders;
        }

        [HttpGet("getnextpredictorders")]
        public async Task<IActionResult> GetNextPredictorOrders()
        {
            var list = await _orders.GetNextPredictedOrdersAsync();
            return Ok(list);
        }

        [HttpGet("getclientorders/{customerId}")]
        public async Task<IActionResult> GetClientOrders(int customerId)
        {
            var orders = await _orders.GetClientOrdersAsync(customerId);
            return Ok(orders);
        }
    }
}
