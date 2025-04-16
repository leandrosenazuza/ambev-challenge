using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate == default ? DateTime.UtcNow : src.SaleDate))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(itemDto =>
                    new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice, itemDto.Discount)).ToList()));

            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalSaleAmount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, SaleItemDTO>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ReverseMap()
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ConstructUsing(src => new SaleItem(src.ProductId, src.Quantity, src.UnitPrice, src.Discount));
        }
    }
}