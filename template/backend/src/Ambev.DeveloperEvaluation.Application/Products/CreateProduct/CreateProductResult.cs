﻿namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public ProductRatingResult? Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductRatingResult
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}