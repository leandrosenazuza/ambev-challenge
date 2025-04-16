﻿namespace Ambev.DeveloperEvaluation.Application.Sales.DTO
{
    public class SaleItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
