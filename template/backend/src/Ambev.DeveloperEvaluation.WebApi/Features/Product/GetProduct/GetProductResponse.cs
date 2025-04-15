namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The product's title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The product's price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The product's detailed description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The product's category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the product's image
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The product's rating information
    /// </summary>
    public ProductRating Rating { get; set; } = new ProductRating();
}

/// <summary>
/// Product rating information
/// </summary>
public class ProductRating
{
    /// <summary>
    /// The average rating score
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// The total number of ratings
    /// </summary>
    public int Count { get; set; }
}