using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductHandler> _logger;

        public GetSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {

            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException();

            var sale = await _saleRepository.GetByIdAsync(request.SaleNumber);
            if (sale == null) throw new InvalidOperationException($"Sale with Sale Number {request.SaleNumber} does not exists"); ;

            var result = _mapper.Map<GetSaleResult>(sale);

            _logger.LogInformation($"Sale returned successfully: {sale.SaleNumber}");

            return result;
        }
    }
}