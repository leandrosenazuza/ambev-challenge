using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
