using Products.Models;
using System.ComponentModel;

namespace NeaRentWeb.Components.Common
{
    public static class ProductContainer
    {
        public static List<Product> AllProducts
        {
            get; set;
        } = new List<Product>();
    }
}
