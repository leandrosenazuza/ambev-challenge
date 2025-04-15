using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{

    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
                   src.Items.Select(item => new SaleItem(item.ProductId, item.Quantity, item.UnitPrice))));
        }
    }
}

