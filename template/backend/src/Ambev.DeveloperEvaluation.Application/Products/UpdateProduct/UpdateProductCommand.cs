using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using MediatR;
namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDTO>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public ProductRatingCommand Rating { get; set; }

        public UpdateProductCommand(int id, string title, decimal price, string? description,
            string? category, string? image, ProductRatingCommand rating)
        {
            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
            Rating = rating;
        }
    }
}