namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sale.TestData
{


    public static class SaleTestData
    {
        public static IEnumerable<object[]> SaleItemNoDiscountData =>
        [
    new object[] { 1, 1, 100m },
new object[] { 2, 3, 50m }
        ];


        public static IEnumerable<object[]> SaleItemTenPercentDiscountData =>
            [
            new object[] { 1, 4, 100m },
        new object[] { 2, 7, 50m }
            ];

        public static IEnumerable<object[]> SaleItemTwentyPercentDiscountData =>
            [
            new object[] { 1, 10, 20m },
        new object[] { 2, 20, 15m }
            ];

        public static IEnumerable<object[]> SaleItemAboveLimitData =>
            [
            new object[] { 1, 21, 10m }
            ];
    }

}



