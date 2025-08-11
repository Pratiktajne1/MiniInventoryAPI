using System.ComponentModel.DataAnnotations;

namespace MiniInventoryAPI.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Description { get; set; }

        // Navigation
        public ICollection<Product> Products { get; set; }
    }
}
