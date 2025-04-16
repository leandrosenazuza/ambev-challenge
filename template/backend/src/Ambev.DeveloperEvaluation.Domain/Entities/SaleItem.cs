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


        public SaleItem(int productId, int quantity, decimal unitPrice, decimal discount)
        {
            ValidateProductId(productId);
            ValidateQuantity(quantity);
            ValidateUnitPrice(unitPrice);
            ValidateDiscount(discount);

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
        }

        private void ValidateProductId(int productId)
        {
            if (productId <= 0)
                throw new ArgumentOutOfRangeException(nameof(productId), "ProductId must be greater than zero.");
        }

        private void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        private void ValidateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero.");
        }

        private void ValidateDiscount(decimal discount)
        {
            if (discount < 0 || discount > 1)
                throw new ArgumentOutOfRangeException(nameof(discount), "Discount must be between 0 and 1.");
        }

        public SaleItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CalculateTotalAmount();
            ApplyDiscount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = Quantity * UnitPrice - Discount;
        }

        private void ApplyDiscount()
        {
            if (Quantity >= 4 && Quantity <= 20)
            {
                if (Quantity >= 10)
                {
                    Discount = TotalAmount * 0.20m;
                }
                else
                {
                    Discount = TotalAmount * 0.10m;
                }
            }
            else
            {
                Discount = 0;
            }
            CalculateTotalAmount();
        }
    }
}
