using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
public class CreateProductRequest
{
    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string Title { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Category { get; set; }

    [Url]
    [StringLength(500)]
    public string? Image { get; set; }

    public ProductRatingRequest? Rating { get; set; }
}

public class ProductRatingRequest
{
    [Range(0, 5)]
    public decimal Rate { get; set; }

    [Range(0, int.MaxValue)]
    public int Count { get; set; }
}