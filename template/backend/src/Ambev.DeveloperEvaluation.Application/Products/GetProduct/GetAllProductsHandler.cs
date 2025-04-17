using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Query;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<ProductDTO>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.Parameters, cancellationToken);

            return new PaginatedResult<ProductDTO>
            {
                Items = _mapper.Map<List<ProductDTO>>(result.Items),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
        }
    }
}