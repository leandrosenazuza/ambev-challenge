using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities; // Adjust namespace as needed
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public ISaleRepository MockSaleRepository { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ISaleRepository));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                MockSaleRepository = Substitute.For<ISaleRepository>();

                services.AddSingleton(MockSaleRepository);
            });
        }
    }

    public class SaleControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly ISaleRepository _mockSaleRepository;
        private readonly Faker _faker;

        public SaleControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mockSaleRepository = factory.MockSaleRepository;
            _faker = new Faker();
        }

        [Fact]
        public async Task CreateSale_ReturnsCreatedSale_WhenDataIsValid()
        {

            var fakeSaleEntity = GenerateFakeSale();
            var fakeSaleDto = MapSaleToDto(fakeSaleEntity);


            _mockSaleRepository
                .CreateAsync(Arg.Any<Sale>())
                .Returns(fakeSaleEntity);

            var response = await _client.PostAsJsonAsync("/api/sale", fakeSaleDto);
            response.EnsureSuccessStatusCode();

            var createdSaleDto = await response.Content.ReadFromJsonAsync<SaleDTO>();

            Assert.NotNull(createdSaleDto);
            AssertSaleDtoEquality(fakeSaleEntity, createdSaleDto);
        }

        private Sale GenerateFakeSale()
        {

            var quantity = 2;
            var unitPrice = 20.0;

            var itemTotal = quantity * unitPrice;

            var fakeItem = new SaleItem
            {
                ProductId = _faker.Random.Int(1, 1000),
                Quantity = quantity,
                UnitPrice = (decimal)unitPrice,
                TotalAmount = (decimal)(quantity * unitPrice)
            };


            var totalSaleAmount = (decimal)itemTotal;

            return new Sale
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = _faker.Date.Past(),
                Customer = _faker.Name.FullName(),
                Branch = _faker.Company.CompanyName(),
                Items = [fakeItem],
                TotalSaleAmount = totalSaleAmount,
                IsCancelled = false
            };
        }

        private SaleDTO MapSaleToDto(Sale entity)
        {
            if (entity == null)
                return null;

            return new SaleDTO
            {
                SaleNumber = entity.SaleNumber,
                SaleDate = entity.SaleDate,
                Customer = entity.Customer,
                TotalSaleAmount = entity.TotalSaleAmount,
                Branch = entity.Branch,
                IsCancelled = entity.IsCancelled,
                Items = entity.Items.Select(item => new SaleItemDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalAmount = item.TotalAmount
                }).ToList()
            };
        }

        private void AssertSaleDtoEquality(Sale expectedEntity, SaleDTO actualDto)
        {
            Assert.Equal(expectedEntity.Customer, actualDto.Customer);
            Assert.Equal(expectedEntity.TotalSaleAmount, actualDto.TotalSaleAmount);
            Assert.Equal(expectedEntity.Branch, actualDto.Branch);
            Assert.Equal(expectedEntity.IsCancelled, actualDto.IsCancelled);


            Assert.Equal(expectedEntity.Items.Count, actualDto.Items.Count);
            for (int i = 0; i < expectedEntity.Items.Count; i++)
            {
                Assert.Equal(expectedEntity.Items[i].ProductId, actualDto.Items[i].ProductId);
                Assert.Equal(expectedEntity.Items[i].Quantity, actualDto.Items[i].Quantity);
                Assert.Equal(expectedEntity.Items[i].UnitPrice, actualDto.Items[i].UnitPrice);
                Assert.Equal(expectedEntity.Items[i].Discount, actualDto.Items[i].Discount);
                Assert.Equal(expectedEntity.Items[i].TotalAmount, actualDto.Items[i].TotalAmount);
            }
        }

        [Fact]
        public async Task GetSaleBySaleNumber_ReturnsSale_WhenSaleExists()
        {
            var existingSaleNumber = Guid.NewGuid();
            var fakeSaleEntity = GenerateFakeSale(existingSaleNumber);

            _mockSaleRepository
                .GetBySaleNumberAsync(existingSaleNumber)
                .Returns(fakeSaleEntity);


            var response = await _client.GetAsync($"/api/sale/{existingSaleNumber}");


            response.EnsureSuccessStatusCode();


            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var returnedSaleDto = await response.Content.ReadFromJsonAsync<SaleDTO>();


            Assert.NotNull(returnedSaleDto);
            AssertSaleDtoEquality(fakeSaleEntity, returnedSaleDto);
        }

        /*
                [Fact]
                public async Task UpdateSale_ReturnsUpdatedSale_WhenDataIsValid()
                {
                    // Arrange
                    var saleNumber = Guid.NewGuid();
                    var originalSale = GenerateFakeSaleDTO(saleNumber);
                    var updatedSale = GenerateFakeSaleDTO(saleNumber);

                    // Setup mock repository to return the updated sale
                    _mockSaleRepository
                        .UpdateSaleAsync(Arg.Any<SaleDTO>())
                        .Returns(updatedSale);

                    // Act
                    var response = await _client.PutAsJsonAsync($"/api/sale/{saleNumber}", updatedSale);
                    response.EnsureSuccessStatusCode();

                    // Assert
                    var returnedSale = await response.Content.ReadFromJsonAsync<SaleDTO>();
                    AssertSaleDtoEquality(updatedSale, returnedSale);
                }

                [Fact]
                public async Task DeleteSale_ReturnsSuccess_WhenSaleExists()
                {
                    // Arrange
                    var saleNumber = Guid.NewGuid();

                    // Setup mock repository to indicate successful deletion
                    _mockSaleRepository
                        .DeleteSaleAsync(saleNumber)
                        .Returns(true);

                    // Act
                    var response = await _client.DeleteAsync($"/api/sale/{saleNumber}");
                    response.EnsureSuccessStatusCode();

                    // Assert
                    // You might want to check the response content or status code
                    Assert.True(response.IsSuccessStatusCode);
                }*/

        // Helper method to generate fake SaleDTO
        private Sale GenerateFakeSale(Guid? saleNumber = null)
        {
            return new Sale
            {
                SaleNumber = saleNumber ?? Guid.NewGuid(),
                SaleDate = _faker.Date.Past(),
                Customer = _faker.Name.FullName(),
                TotalSaleAmount = _faker.Finance.Amount(),
                Branch = _faker.Company.CompanyName(),
                Items =
                [
                    new SaleItem
                    {
                        ProductId = _faker.Random.Int(1, 1000),
                        Quantity = _faker.Random.Int(1, 10),
                        UnitPrice = _faker.Finance.Amount(10, 100),
                    }
                ],
                IsCancelled = _faker.Random.Bool()
            };
        }
    }
}