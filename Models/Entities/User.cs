using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macone.Models.Entities
{
    public class User
    {
        public int MaUser { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string ViTri { get; set; }
    }
}
