using Microsoft.AspNetCore.Mvc;
using prueba_codifico.DTO.Interfaces.Sales;

namespace prueba_codifico.Modules.Controllers.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipperController : ControllerBase
    {
        private readonly IShipper _shipper;

        public ShipperController(IShipper shipper)
        {
            _shipper = shipper;
        }

        [HttpGet("getallshippers")]
        public async Task<IActionResult> GetAllShippers()
        {
            var shippers = await _shipper.GetAllShippersAsync();
            return Ok(shippers);
        }
    }
}
