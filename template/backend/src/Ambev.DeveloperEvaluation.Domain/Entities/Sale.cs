﻿using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    [Table("Sales")]
    public class Sale : BaseEntity
    {
        [Column("sale_number")]
        [Key]
        public Guid SaleNumber { get; set; } = Guid.NewGuid();

        [Column("sale_date")]
        [Required]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Column("customer")]
        [Required]
        [MaxLength(255)]
        public string Customer { get; set; } = string.Empty;

        [Column("total_sale_amount")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalSaleAmount { get; set; }

        [Column("branch")]
        [Required]
        [MaxLength(255)]
        public string Branch { get; set; } = string.Empty;

        [Column("items")]
        public List<SaleItem> Items { get; set; } = [];

        [Column("is_cancelled")]
        [Required]
        public bool IsCancelled { get; set; } = false;

        public void RecalculateSaleTotal()
        {
            TotalSaleAmount = Items.Sum(i => i.TotalAmount);
        }
        public void AddItem(SaleItem item)
        {
            Items.Add(item);
            RecalculateSaleTotal();
        }
    }
}

