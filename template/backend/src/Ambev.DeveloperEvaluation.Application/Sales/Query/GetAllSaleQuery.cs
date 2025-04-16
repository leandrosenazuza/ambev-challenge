using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Query;

public class GetAllSaleQuery : IRequest<PaginatedResult<SaleDTO>>
{
    public PaginationParameters Parameters { get; set; }

    public GetAllSaleQuery(PaginationParameters parameters)
    {
        Parameters = parameters;
    }

    public GetAllSaleQuery()
    {
    }
}
