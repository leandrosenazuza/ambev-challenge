using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductProfile : Profile
{

    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
               .ForMember(dest => dest.RatingRate,
                   opt => opt.MapFrom(src => src.Rating != null ? src.Rating.Rate : 0m))
               .ForMember(dest => dest.RatingCount,
                   opt => opt.MapFrom(src => src.Rating != null ? src.Rating.Count : 0));

        CreateMap<Product, CreateProductResult>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => new ProductRatingResult
                {
                    Rate = src.RatingRate,
                    Count = src.RatingCount
                }));
    }
}
