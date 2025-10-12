using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Category
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public ICollection<Product> SanPhams { get; set; }
    }
}
