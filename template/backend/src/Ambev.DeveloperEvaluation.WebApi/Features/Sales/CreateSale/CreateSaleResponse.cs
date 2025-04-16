namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid SaleNumber { get; set; }

        public DateTime SaleDate { get; set; }

        public string Customer { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public decimal TotalSaleAmount { get; set; }

        public List<CreateSaleItemResponse> Items { get; set; } = [];

        public bool IsCancelled { get; set; }
    }

    public class CreateSaleItemResponse
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
