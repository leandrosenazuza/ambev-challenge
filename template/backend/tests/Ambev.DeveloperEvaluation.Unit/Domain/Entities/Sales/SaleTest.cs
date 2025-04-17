using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;


namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales
{
    public class SaleTests
    {

        [Fact]
        public void Sale_AddItems_RecalculateSaleTotalCorrectly()
        {
            var sale = new DeveloperEvaluation.Domain.Entities.Sale();

            var saleItem1 = new SaleItem(productId: 1, quantity: 3, unitPrice: 10m);
            var saleItem2 = new SaleItem(productId: 2, quantity: 4, unitPrice: 20m);

            sale.AddItem(saleItem1);
            sale.AddItem(saleItem2);
            var expectedSaleAmount = saleItem1.TotalAmount + saleItem2.TotalAmount;

            Assert.Equal(expectedSaleAmount, sale.TotalSaleAmount);
        }
    }
}