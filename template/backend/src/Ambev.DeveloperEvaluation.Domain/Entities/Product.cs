using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("title")]
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column("price")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("category")]
        [MaxLength(100)]
        public string? Category { get; set; }

        [Column("image")]
        [MaxLength(500)]
        public string? Image { get; set; }

        [Column("rating_rate")]
        [Range(0, 5)]
        public decimal RatingRate { get; set; } = 0m;

        [Column("rating_count")]
        [Range(0, int.MaxValue)]
        public int RatingCount { get; set; } = 0;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}