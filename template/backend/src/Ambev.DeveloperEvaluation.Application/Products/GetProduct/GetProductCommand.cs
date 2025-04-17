using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommand : IRequest<ProductDTO>
    {
        public int Id { get; set; }

        public GetProductCommand(int id)
        {
            Id = id;
        }
    }
}