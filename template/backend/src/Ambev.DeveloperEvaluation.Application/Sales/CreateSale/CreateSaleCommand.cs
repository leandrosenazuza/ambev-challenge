using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<SaleDTO>
    {
        public Guid SaleNumber { get; set; } = Guid.NewGuid();
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemDTO> Items { get; set; } = [];
        public bool IsCancelled { get; set; } = false;
    }
}