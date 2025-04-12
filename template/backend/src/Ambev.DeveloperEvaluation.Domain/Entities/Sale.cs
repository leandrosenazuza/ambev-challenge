using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class Sale
    {
        [Key]
        public Guid SaleNumber { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public String Customer { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSaleAmount { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<Product> Product { get; set; }

        [Required]
        public int Qtd { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public Branch Branch { get; set; }

        public bool IsCancelled { get; set; }

    }
}



