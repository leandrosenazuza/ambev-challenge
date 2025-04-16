
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, Sale>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleCommand, CreateSaleRequest>();

            CreateMap<CreateSaleCommand, Sale>()
                          .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
                              src.Items.Select(item => new SaleItem(item.ProductId, item.Quantity, item.UnitPrice))));
            CreateMap<SaleItem, CreateSaleItemResult>();
            CreateMap<Sale, CreateSaleResult>();
            CreateMap<CreateSaleItemRequest, SaleItemResult>();
        }
    }
}
