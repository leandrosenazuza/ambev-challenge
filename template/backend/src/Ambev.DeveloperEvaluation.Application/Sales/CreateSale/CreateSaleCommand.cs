using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Guid SaleNumber { get; set; } = Guid.NewGuid();
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;

        public List<SaleItemResult> Items { get; set; } = [];

        public bool IsCancelled { get; set; } = false;
    }

    public class SaleItemResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}