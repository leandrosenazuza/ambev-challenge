using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        public Guid SaleNumber { get; set; }

        public GetSaleCommand(Guid saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}