using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class SaleItem
    {
        [Column("product_id")]
        [Required]
        [Key]
        public int ProductId { get; set; }

        [Column("quantity")]
        [Required]
        [Range(1, 20)]
        public int Quantity { get; set; }

        [Column("unit_price")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; } = 0;

        [Column("total_amount")]
        [Required]
        public decimal TotalAmount { get; set; }

        public SaleItem() { }

        public SaleItem(int productId, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot sell more than 20 items!!!");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;

            ApplyDiscountRules();

        }

        private void ApplyDiscountRules()
        {
            var baseAmount = Quantity * UnitPrice;

            if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = baseAmount * 0.20m;
            }
            else if (Quantity >= 4)
            {
                Discount = baseAmount * 0.10m;
            }
            else
            {
                Discount = 0m;
            }
            TotalAmount = baseAmount - Discount;
        }
    }

}
