using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using AutoMapper;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }


        public async Task<Boolean> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);
            if (existingSale == null)
                throw new InvalidOperationException($"Sale with Sale Number {request.SaleNumber} not exists");
            return await _saleRepository.DeleteAsync(request.SaleNumber, cancellationToken);
        }
    }
}
