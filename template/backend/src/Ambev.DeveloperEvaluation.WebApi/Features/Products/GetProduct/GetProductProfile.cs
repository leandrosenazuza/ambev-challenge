using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{

    public class GetProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetUser feature
        /// </summary>
        public GetProductProfile()
        {
            CreateMap<int, GetProductCommand>()
                .ConstructUsing(id => new GetProductCommand(id));
            CreateMap<GetProductResult, GetProductResponse>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<int, GetProductCommand>()
.ConstructUsing(id => new GetProductCommand(id));

            CreateMap<GetProductResult, GetProductResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));

            CreateMap<ProductRatingResult, ProductRating>()
                .ForMember(dest => dest.Rate,   // replace with actual property names
                           opt => opt.MapFrom(src => src.Rate));
        }
    }

}
