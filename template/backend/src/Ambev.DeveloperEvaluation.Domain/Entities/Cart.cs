using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart
    {
        [Column("id")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("date")]
        [Required]
        public DateTime Date { get; set; }

        //[NotMapped] 
        public List<CartProduct> Products { get; set; } = [];

        public Cart()
        {
        }
    }
}