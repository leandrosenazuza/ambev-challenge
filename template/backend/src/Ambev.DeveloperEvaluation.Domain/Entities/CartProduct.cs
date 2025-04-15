using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class CartProduct
    {
        [Column("id")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Column("product_id")]
        [Required]
        public int ProductId { get; set; }

        [Column("unit_price")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Column("quantity")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column("total_price")]
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}