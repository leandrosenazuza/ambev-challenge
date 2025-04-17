using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Query;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetAllSalesHandler : IRequestHandler<GetAllSaleQuery, PaginatedResult<SaleDTO>>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSalesHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SaleDTO>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.Parameters, cancellationToken);

            return new PaginatedResult<SaleDTO>
            {
                Items = _mapper.Map<List<SaleDTO>>(result.Items),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
        }
    }
}

