using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class Product
    {
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public long Gia { get; set; }
        public string AnhDaiDien { get; set; }
        public string MoTa { get; set; }
        public string ThongTin { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public string MaLoai { get; set; }
        public Category Loai { get; set; }
        public ICollection<Image> Anhs { get; set; }
    }
}