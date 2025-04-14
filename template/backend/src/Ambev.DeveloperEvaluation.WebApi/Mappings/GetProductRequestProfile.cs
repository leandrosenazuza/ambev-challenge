using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductRequestProfile : Profile
    {
        public GetProductRequestProfile() {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, CreateProductResult>();

        }
    }
}
