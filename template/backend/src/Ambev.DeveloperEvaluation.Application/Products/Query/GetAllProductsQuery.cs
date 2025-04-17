using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Query;

public class GetAllProductsQuery : IRequest<PaginatedResult<ProductDTO>>
{
    public PaginationParameters Parameters { get; set; }

    public GetAllProductsQuery(PaginationParameters parameters)
    {
        Parameters = parameters;
    }

    public GetAllProductsQuery()
    {
    }
}