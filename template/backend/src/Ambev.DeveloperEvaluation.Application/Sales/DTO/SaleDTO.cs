namespace Ambev.DeveloperEvaluation.Application.Sales.DTO
{
    public class SaleDTO
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemDTO> Items { get; set; } = [];
        public bool IsCancelled { get; set; }

        public void RecalculateSaleTotal()
        {
            TotalSaleAmount = Items.Sum(i => i.TotalAmount);
        }
        public void AddItem(SaleItemDTO item)
        {
            Items.Add(item);
            RecalculateSaleTotal();
        }
    }
}
