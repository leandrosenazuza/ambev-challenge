using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;



    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public ProductRatingCommand? Rating { get; set; }
    }

    public class ProductRatingCommand
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }