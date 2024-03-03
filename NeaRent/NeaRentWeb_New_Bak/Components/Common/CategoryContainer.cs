using Products.Models;

namespace NeaRentWeb.Components.Common
{
    public class CategoryContainer
    {
        public List<Category> AllCategories
        {
            get; set;
        } = new List<Category>();

        public List<Category> AllFeaturedCategories
        {
            get; set;
        } = new List<Category>();
    }
}
