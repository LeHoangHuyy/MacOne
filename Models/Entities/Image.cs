using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImageFileName { get; set; }
        public string? ProductId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public Product? SanPham { get; set; }
    }
}