using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales
{
    public class CreateSaleRequestProfile : Profile
    {
        public CreateSaleRequestProfile()
        {
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}
