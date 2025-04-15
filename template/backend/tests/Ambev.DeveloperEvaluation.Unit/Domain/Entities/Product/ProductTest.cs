using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Tests.Domain
{
    public class ProductTest
    {
        [Fact]
        public void Product_Should_Create_Valid_Instance()
        {
            // Arrange
            var product = ProductTestData.GetValidProduct();

            // Assert
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
            // The Title can not be "" or null
            var product = new Product { Title = invalidTitle };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);

            //Here we are using FluentAssertions to check if the exception is thrown
            //Could be any exception, since it be an ValidationException
            action.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Product_Should_Not_Accept_Negative_Price(decimal invalidPrice)
        {

            // The price have to be greater than 0!
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
            // The rate have to be between 0 and 5!
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
            // Arrange & Act
            // The rate have to be between 0 and 5!
            var product = new Product { RatingCount = invalidCount };

            Action action = () => Validator.ValidateObject(product,
                new ValidationContext(product),
                validateAllProperties: true);

            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Product_Should_Have_CreatedAt_When_Instantiated()
        {
            // Arrange
            var product = new Product();

            // Assert
            product.CreatedAt.Should().NotBe(default(DateTime));
            product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}