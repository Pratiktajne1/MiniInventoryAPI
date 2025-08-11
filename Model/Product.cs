using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryAPI.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sku { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Optimistic Concurrency

        [Timestamp]
        [BindNever]
        public byte[] RowVersion { get; set; }
    }
}
