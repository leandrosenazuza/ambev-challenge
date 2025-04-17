using Ambev.DeveloperEvaluation.Application.Products.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.Helper;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.ProductController
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IProductRepository MockProductRepository { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IProductRepository));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                MockProductRepository = Substitute.For<IProductRepository>();
                services.AddSingleton(MockProductRepository);
            });
        }
    }

    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IProductRepository _mockProductRepository;
        private readonly Faker _faker;
        private readonly string _token;

        public ProductControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mockProductRepository = factory.MockProductRepository;
            _faker = new Faker();
            _token = TestAuthenticationHelper.GenerateJwtToken();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public async Task GetAllProducts_ReturnsPaginatedResult_WhenParametersAreValid(int page, int pageSize)
        {
            var fakeProducts = Enumerable.Range(1, 10)
                .Select(_ => GenerateFakeProduct())
                .ToList();

            var expectedPaginatedResult = new PaginatedResult<Product>
            {
                Items = fakeProducts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalItems = fakeProducts.Count,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(fakeProducts.Count / (double)pageSize)
            };

            _mockProductRepository
                .GetAllAsync(Arg.Any<PaginationParameters>(), Arg.Any<CancellationToken>())
                .Returns(expectedPaginatedResult);

            var response = await _client.GetAsync($"/api/products/all?Page={page}&PageSize={pageSize}");

            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponseHelper<PaginatedResult<ProductDTO>>>();
            var returnedResult = apiResponse.Data;

            Assert.NotNull(returnedResult);
            Assert.NotNull(returnedResult.Items);
            Assert.Equal(expectedPaginatedResult.CurrentPage, returnedResult.CurrentPage);
            Assert.Equal(expectedPaginatedResult.TotalPages, returnedResult.TotalPages);
            Assert.Equal(expectedPaginatedResult.TotalItems, returnedResult.TotalItems);
        }

      
        private Product GenerateFakeProduct(int? id = null)
        {
         
            return new Product
            {
                Title = _faker.Commerce.ProductName(),
                Price = decimal.Parse(_faker.Commerce.Price()),
                Description = _faker.Commerce.ProductDescription(),
                Image = _faker.Image.PicsumUrl(),
                RatingRate = _faker.Random.Decimal(1, 5),
                RatingCount = _faker.Random.Int(1, 1000)

            };
        }



        private void AssertProductDtoEquality(Product expectedEntity, ProductDTO actualDto)
        {
            Assert.Equal(expectedEntity.Id, actualDto.Id);
            Assert.Equal(expectedEntity.Title, actualDto.Title);
            Assert.Equal(expectedEntity.Price, actualDto.Price);
            Assert.Equal(expectedEntity.Description, actualDto.Description);
            Assert.Equal(expectedEntity.Category, actualDto.Category);
            Assert.Equal(expectedEntity.Image, actualDto.Image);
            Assert.Equal(expectedEntity.RatingRate, actualDto.Rating.Rate);
            Assert.Equal(expectedEntity.RatingRate, actualDto.Rating.Count);
        }
    }
}