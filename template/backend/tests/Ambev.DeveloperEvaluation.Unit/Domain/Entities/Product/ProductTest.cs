using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Domain
{
    public class ProductTest
    {
        [Fact]
        public void Product_Should_Create_Valid_Instance()
        {
            var product = ProductTestData.GetValidProduct();

            product.Should().NotBeNull();
            product.Title.Should().NotBeNullOrEmpty();
            product.Price.Should().BeGreaterThanOrEqualTo(0);
            product.RatingRate.Should().BeInRange(0, 5);
            product.RatingCount.Should().BeGreaterThanOrEqualTo(0);
            product.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_Should_Not_Accept_Invalid_Title(string invalidTitle)
        {
            var product = new Product { Title = invalidTitle };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);
            action.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Product_Should_Not_Accept_Negative_Price(decimal invalidPrice)
        {

            // The price have to be greater than 0!!!
            var product = new Product { Price = invalidPrice };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);

            action.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Product_Should_Not_Accept_Invalid_Rating_Rate(decimal invalidRate)
        {
            // The rate have to be between 0 and 5!!!
            var product = new Product { RatingRate = invalidRate };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);

            action.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(-1)]
        public void Product_Should_Not_Accept_Negative_Rating_Count(int invalidCount)
        {
            // The rate have to be between 0 and 5!!!
            var product = new Product { RatingCount = invalidCount };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);

            action.Should().Throw<ValidationException>();
        }
    }
}