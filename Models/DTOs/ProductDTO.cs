using Macone.Models.Entities;

namespace Macone.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public long? Price { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? Stock { get; set; }
        public double? Weight { get; set; }
        public string? Size { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
