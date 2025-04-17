using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Domain
{
    public static class ProductTestData
    {
        public static Product GetValidProduct()
        {
            return new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 99.99m,
                Description = "This is a test product description",
                Category = "Test Category",
                Image = "test1.jpg",
                RatingRate = 4.5m,
                RatingCount = 100,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static List<Product> GetValidProducts()
        {
            return
            [
                new Product
                {
                    Id = 1,
                    Title = "First Product",
                    Price = 29.99m,
                    Description = "First test product description",
                    Category = "Electronics",
                    Image = "teste2.jpg",
                    RatingRate = 4.2m,
                    RatingCount = 50
                },
                new Product
                {
                    Id = 2,
                    Title = "Second Product",
                    Price = 49.99m,
                    Description = "Second test product description",
                    Category = "Clothing",
                    Image = "test3.jpg",
                    RatingRate = 3.8m,
                    RatingCount = 75
                },
                new Product
                {
                    Id = 3,
                    Title = "Third Product",
                    Price = 99.99m,
                    Description = "Third test product description",
                    Category = "Home",
                    Image = "test4.jpg",
                    RatingRate = 4.7m,
                    RatingCount = 120
                }
            ];
        }

        public static Product GetProductWithMinimumRequiredFields()
        {
            return new Product
            {
                Title = "Minimum Product",
                Price = 10.00m
            };
        }

        public static Product GetProductWithMaximumValues()
        {
            return new Product
            {
                Id = int.MaxValue,
                Title = new string('A', 255),
                Price = decimal.MaxValue,
                Description = new string('A', 1000),
                Category = new string('A', 100),
                Image = new string('A', 500),
                RatingRate = 5m,
                RatingCount = int.MaxValue
            };
        }
    }
}