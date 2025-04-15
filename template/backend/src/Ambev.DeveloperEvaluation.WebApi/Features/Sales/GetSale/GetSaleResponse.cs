namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemResponse> Items { get; set; } = new List<SaleItemResponse>();
    }

    public class SaleItemResponse
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}