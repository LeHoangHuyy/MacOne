using Macone.Models.Entities;

namespace Macone.Areas.User.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Image> Images { get; set; }
        public ProductDetailViewModel(Product Product, List<Image> Images)
        {
            this.Product = Product;
            this.Images = Images;
        }
    }
}
