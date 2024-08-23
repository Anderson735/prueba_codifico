using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DTO.Interfaces.Sales
{
    public interface IOrderDetails
    {
        Task AddOrderAsync(OrderDTO orderDto);
    }
}
