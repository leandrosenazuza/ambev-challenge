using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductProfile : Profile
{

    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<GetProductResult, Product >()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

    }
}
