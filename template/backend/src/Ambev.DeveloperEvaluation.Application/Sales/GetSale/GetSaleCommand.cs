using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommand : IRequest<SaleDTO>
    {
        public Guid SaleNumber { get; set; }

        public GetSaleCommand(Guid saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}