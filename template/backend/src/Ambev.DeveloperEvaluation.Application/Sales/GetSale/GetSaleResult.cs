namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemResult> Items { get; set; } = new List<SaleItemResult>();

        public GetSaleResult() { }
    }

    public class SaleItemResult
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}