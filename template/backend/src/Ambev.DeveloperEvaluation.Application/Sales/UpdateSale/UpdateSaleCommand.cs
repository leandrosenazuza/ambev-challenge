using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<SaleDTO>
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; }
        public List<SaleItemDTO> Items { get; set; }
        public bool IsCancelled { get; set; }
    }
}