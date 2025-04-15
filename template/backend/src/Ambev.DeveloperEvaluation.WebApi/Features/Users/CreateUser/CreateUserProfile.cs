using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
           .ForMember(dest => dest.Rating,
               opt => opt.MapFrom(src => src.Rating));

        CreateMap<ProductRatingRequest, ProductRatingCommand>();

        CreateMap<Domain.Entities.Product, CreateProductResponse>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => new ProductRatingResponse
                {
                    Rate = src.RatingRate,
                    Count = src.RatingCount
                }));
    }
}
