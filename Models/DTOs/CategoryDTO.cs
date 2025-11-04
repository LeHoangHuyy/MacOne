using Macone.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Macone.Models.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category names cannot exceed 100 characters")]
        public string? Name { get; set; }
    }
}
