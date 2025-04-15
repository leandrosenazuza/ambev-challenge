namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid SaleNumber { get; set; } = Guid.NewGuid();
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }

    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}