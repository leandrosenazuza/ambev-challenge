using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using MediatR;
namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDTO>
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public ProductRatingCommand Rating { get; set; }

        public CreateProductCommand(int id, string title, decimal price, string? description,
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

    public class ProductRatingCommand
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }

        public ProductRatingCommand(decimal rate, int count)
        {
            Rate = rate;
            Count = count;
        }
    }
}