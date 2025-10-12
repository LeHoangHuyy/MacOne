using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Image
    {
        public int MaAnh { get; set; }
        public string MaSp { get; set; }
        public string TenFileAnh { get; set; }
        public Product SanPham { get; set; }
    }
}