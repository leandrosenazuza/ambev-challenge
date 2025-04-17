using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleDTO>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;


        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"User with Sale Number {command.SaleNumber} already exists");

            var sale = _mapper.Map<Sale>(command);

            await _saleRepository.CreateAsync(sale, cancellationToken);

            //sale.PublishEvent("SaleCreated"); TODO
            var response = _mapper.Map<SaleDTO>(sale);
            return response;
        }
    }
}

