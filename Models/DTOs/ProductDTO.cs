using System.ComponentModel.DataAnnotations;

namespace Macone.Models.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Price must requird")]
        [Range(1, 10000000000, ErrorMessage ="Price must be greater than 0")]
        public long? Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(2000, ErrorMessage = "Product information must not exceed 2000 characters")]
        public string? Information { get; set; }

        [Required(ErrorMessage = "Inventory quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Inventory quantity must be greater than or equal to 0")]
        public int? Stock { get; set; }

        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000 kg")]
        public double? Weight { get; set; }

        [StringLength(50, ErrorMessage = "Size must not exceed 50 characters")]
        public string? Size { get; set; }

        [Required(ErrorMessage = "Product category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid product category")]
        public int? CategoryId { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }
    }
}
