using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public long? Price { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        //public int? Stock { get; set; }
        //public double? Weight { get; set; }
        //public string? Size { get; set; }
        public string? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}