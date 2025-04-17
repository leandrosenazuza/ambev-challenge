using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DTO;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO
                {
                    Rate = src.RatingRate,
                    Count = src.RatingCount
                }));
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.RatingRate, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating.Count));
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.RatingRate, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating.Count))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.RatingRate, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating.Count))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            
        }
    }
}