using Products.Models;

namespace NeaRentWeb.Components.Common
{
    public class ProductContainer
    {
        public List<Product> AllProducts
        {
            get; set;
        } = new List<Product>();

        public List<Product> SearchedProducts
        {
            get; set;
        } = new List<Product>();
    }
}
