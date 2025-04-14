
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
public class CreateProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public ProductRatingResponse Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductRatingResponse
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}