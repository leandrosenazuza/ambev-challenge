using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class DeleteSaleCommand : IRequest<Guid>
    {
        public Guid SaleNumber { get; set; }
    }
}