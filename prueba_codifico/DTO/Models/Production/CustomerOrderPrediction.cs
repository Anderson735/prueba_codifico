namespace prueba_codifico.DTO.Models.Production
{
    public class CustomerOrderPrediction
    {
        public string? CustomerName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
