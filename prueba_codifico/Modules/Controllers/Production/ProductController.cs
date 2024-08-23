using Microsoft.AspNetCore.Mvc;
using prueba_codifico.DTO.Interfaces.HR;
using prueba_codifico.DTO.Interfaces.Production;
using prueba_codifico.DTO.Models.HR;

namespace prueba_codifico.Modules.Controllers.Production
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _product;
        public ProductController(IProducts product)
        {
            _product = product;
        }


        [HttpGet("getproducts")]
        public async Task<IActionResult> GetEmployees()
        {
            var products = await _product.GetAllProductsAsync();
            return Ok(products);
        }

    }
}
