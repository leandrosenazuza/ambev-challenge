using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    // It is assumed that CreateSaleCommand implements IRequest
    // and contains properties for SaleDate, Customer, Branch, IsCancelled, and a list of Item DTOs.
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;


        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"User with Sale Number {command.SaleNumber} already exists");

            var sale = _mapper.Map<Sale>(command);
            sale.CalculateTotal();

            await _saleRepository.CreateAsync(sale, cancellationToken);

            //sale.PublishEvent("SaleCreated"); TODO
            var response = _mapper.Map<CreateSaleResult>(sale);
            return response;
        }
    }
}