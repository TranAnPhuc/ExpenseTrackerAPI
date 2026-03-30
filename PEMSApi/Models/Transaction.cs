using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEMSApi.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
