using prueba_codifico.DTO.Models.Production;
using prueba_codifico.DTO.Models.Sales;

namespace prueba_codifico.DTO.Interfaces.Sales
{
    public interface IOrders
    {
        Task<List<CustomerOrderPrediction>> GetNextPredictedOrdersAsync();
        Task<List<Order>> GetClientOrdersAsync(int customerId);
    }
}
