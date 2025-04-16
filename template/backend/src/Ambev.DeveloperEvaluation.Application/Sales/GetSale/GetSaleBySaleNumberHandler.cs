using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Query;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleBySaleNumberHandler : IRequestHandler<GetSaleBySaleNumberQuery, SaleDTO>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleBySaleNumberHandler> _logger;

        public GetSaleBySaleNumberHandler(ISaleRepository repository, IMapper mapper, ILogger<GetSaleBySaleNumberHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleDTO> Handle(GetSaleBySaleNumberQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetSaleBySaleNumberQuery for SaleNumber: {SaleNumber}", request.SaleNumber);

                var sale = await _repository.GetBySaleNumberAsync(request.SaleNumber);
                if (sale == null)
                {
                    _logger.LogWarning("Sale not found for SaleNumber: {SaleNumber}", request.SaleNumber);
                    return null; // Or throw an exception if preferred
                }

                var result = _mapper.Map<SaleDTO>(sale);
                _logger.LogInformation("Successfully retrieved sale for SaleNumber: {SaleNumber}", request.SaleNumber);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetSaleBySaleNumberQuery for SaleNumber: {SaleNumber}", request.SaleNumber);
                throw;
            }
        }
    }
}