using prueba_codifico.DTO.Models.Production;

namespace prueba_codifico.DTO.Interfaces.Production
{
    public interface IProducts
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
