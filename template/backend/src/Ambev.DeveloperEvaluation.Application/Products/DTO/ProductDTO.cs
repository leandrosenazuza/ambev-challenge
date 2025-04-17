namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public ProductRatingDTO Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProductRatingDTO
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }

