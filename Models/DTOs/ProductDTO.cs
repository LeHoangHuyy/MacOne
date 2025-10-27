namespace Macone.Models.DTOs
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public long? Price { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public int? Stock { get; set; }
        public double? Weight { get; set; }
        public string? Size { get; set; }
        public int? CategoryId { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }
    }
}
