using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
