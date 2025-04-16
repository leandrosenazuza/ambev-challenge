using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {


        public Guid SaleNumber { get; set; }

        public DateTime SaleDate { get; set; }


        [Required]
        [MaxLength(255)]
        public string Customer { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Branch { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "At least one sale item is required.")]
        public List<CreateSaleItemRequest> Items { get; set; } = [];

        public bool IsCancelled { get; set; } = false;
    }

    public class CreateSaleItemRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Quantity must be between 1 and 20.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Unit price must be greater than or equal to 0.")]
        public decimal UnitPrice { get; set; }
    }
}

