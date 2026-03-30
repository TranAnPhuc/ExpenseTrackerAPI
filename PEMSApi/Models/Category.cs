using System.ComponentModel.DataAnnotations;

namespace PEMSApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // 0 (false) = Thu, 1 (true) = Chi
        public bool Type { get; set; }
    }
}
