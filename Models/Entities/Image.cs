using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Macone.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImageFileName { get; set; }
        public bool IsMain { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}