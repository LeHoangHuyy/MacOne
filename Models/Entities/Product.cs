using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}