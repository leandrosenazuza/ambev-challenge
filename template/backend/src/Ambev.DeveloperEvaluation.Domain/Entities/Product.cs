using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{ 
        public class Product : BaseEntity
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(200)]
            public string Title { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal Price { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [StringLength(50)]
            public string Category { get; set; }

            [Required]
            public string Image { get; set; }
            public Rating Rating { get; set; }
        }

        public class Rating
        {
            [Key]
            public int Id { get; set; }

            [Column(TypeName = "decimal(3,1)")]
            public decimal Rate { get; set; }

            public int Count { get; set; }

            public int ProductId { get; set; }

            public Product Product { get; set; }
        }
    }

