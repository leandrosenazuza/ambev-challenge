using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Query
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}