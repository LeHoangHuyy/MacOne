using Macone.Models.Entities;

namespace Macone.Areas.User.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product SanPham { get; set; }
        public List<Image> AnhSanPhams { get; set; }
        public ProductDetailViewModel(Product SanPham, List<Image> AnhSanPhams)
        {
            this.SanPham = SanPham;
            this.AnhSanPhams = AnhSanPhams;
        }
    }
}
