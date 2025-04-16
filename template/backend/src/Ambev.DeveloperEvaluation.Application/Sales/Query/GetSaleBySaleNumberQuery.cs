using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Query
{
    public class GetSaleBySaleNumberQuery : IRequest<SaleDTO>
    {
        public Guid SaleNumber { get; set; }

        public GetSaleBySaleNumberQuery(Guid SaleNumber)
        {
            SaleNumber = SaleNumber;
        }
    }
}
