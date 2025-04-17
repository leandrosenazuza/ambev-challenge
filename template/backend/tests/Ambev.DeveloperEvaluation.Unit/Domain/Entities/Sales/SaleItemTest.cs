using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sale.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sale
{

    public class SaleItemTests
    {
        [Theory]
        [MemberData(nameof(SaleTestData.SaleItemNoDiscountData), MemberType = typeof(SaleTestData))]
        public void SaleItem_BelowFourItems_NoDiscount(int productId, int quantity, decimal unitPrice)
        {
            var saleItem = new SaleItem(productId, quantity, unitPrice);

            var expectedBaseAmount = quantity * unitPrice;
            var expectedDiscount = 0m;
            var expectedTotal = expectedBaseAmount;

            // Assert
            Assert.Equal(expectedDiscount, saleItem.Discount);
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }

        [Theory]
        [MemberData(nameof(SaleTestData.SaleItemTenPercentDiscountData), MemberType = typeof(SaleTestData))]
        public void SaleItem_FourToNineItems_TenPercentDiscount(int productId, int quantity, decimal unitPrice)
        {
            var saleItem = new SaleItem(productId, quantity, unitPrice);

            var baseAmount = quantity * unitPrice;
            var expectedDiscount = baseAmount * 0.10m;
            var expectedTotal = baseAmount - expectedDiscount;

            Assert.Equal(expectedDiscount, saleItem.Discount);
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }

        [Theory]
        [MemberData(nameof(SaleTestData.SaleItemTwentyPercentDiscountData), MemberType = typeof(SaleTestData))]
        public void SaleItem_TenToTwentyItems_TwentyPercentDiscount(int productId, int quantity, decimal unitPrice)
        {
            var saleItem = new SaleItem(productId, quantity, unitPrice);

            var baseAmount = quantity * unitPrice;
            var expectedDiscount = baseAmount * 0.20m;
            var expectedTotal = baseAmount - expectedDiscount;

            Assert.Equal(expectedDiscount, saleItem.Discount);
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }

        [Theory]
        [MemberData(nameof(SaleTestData.SaleItemAboveLimitData), MemberType = typeof(SaleTestData))]
        public void SaleItem_QuantityGreaterThanTwenty_ThrowsArgumentOutOfRangeException(int productId, int quantity, decimal unitPrice)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SaleItem(productId, quantity, unitPrice));
        }
    }

}
