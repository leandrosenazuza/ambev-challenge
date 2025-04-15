namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid SaleNumber { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
