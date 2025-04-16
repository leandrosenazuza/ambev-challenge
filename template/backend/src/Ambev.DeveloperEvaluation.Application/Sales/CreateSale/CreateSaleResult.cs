namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
   public class CreateSaleResult
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public decimal TotalSaleAmount { get; set; }
    public List<CreateSaleItemResult> Items { get; set; }
    public bool IsCancelled { get; set; }
}

    public class CreateSaleItemResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
