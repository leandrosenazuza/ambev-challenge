using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class DeleteSaleCommand : IRequest<Boolean>
    {
        public Guid SaleNumber { get; set; }

        public DeleteSaleCommand(Guid saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}