﻿using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleDTO>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;


        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(command);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            //sale.PublishEvent("SaleCreated"); TODO
            var response = _mapper.Map<SaleDTO>(sale);
            return response;
        }
    }
}
