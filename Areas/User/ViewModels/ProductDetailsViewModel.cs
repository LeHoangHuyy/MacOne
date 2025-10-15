using Macone.Models.Entities;

namespace Macone.Areas.User.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public List<Image> Images { get; set; }
        public ProductDetailsViewModel(Product Product, List<Image> Images)
        {
            this.Product = Product;
            this.Images = Images;
        }
    }
}
