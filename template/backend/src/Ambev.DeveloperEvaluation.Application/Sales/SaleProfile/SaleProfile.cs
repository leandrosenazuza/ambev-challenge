using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleProfile
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {

            CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.SaleNumber, opt => opt.Ignore())
            .ForMember(dest => dest.SaleDate, opt =>
            opt.MapFrom(src => src.SaleDate == default ? DateTime.UtcNow : src.SaleDate))
            .ForMember(dest => dest.TotalSaleAmount, opt =>
            opt.MapFrom(src => src.Items
            .Select(itemDto => new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice))
            .Sum(si => si.TotalAmount)))
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
            src.Items.Select(itemDto =>
            new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice))
            .ToList()));

            CreateMap<UpdateSaleCommand, Sale>()

            .ForMember(dest => dest.SaleDate, opt =>
                opt.MapFrom(src => src.SaleDate == default ? DateTime.UtcNow : src.SaleDate))
            .ForMember(dest => dest.TotalSaleAmount, opt =>
                opt.MapFrom(src => src.Items
                    .Select(itemDto => new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice))
                    .Sum(si => si.TotalAmount)))
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalSaleAmount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<SaleItem, SaleItemDTO>()
                   .ReverseMap()
                   .ConstructUsing(src => new SaleItem(src.ProductId, src.Quantity, src.UnitPrice));

            CreateMap<DeleteSaleCommand, Boolean>();
        }


    }
}