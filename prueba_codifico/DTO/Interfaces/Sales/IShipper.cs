using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DTO.Interfaces.Sales
{
    public interface IShipper
    {
        Task<List<Shipper>> GetAllShippersAsync();
    }
}
