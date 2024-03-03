using Products.Models;

namespace NeaRentWeb.Components.Common
{
    public static class Categories
    {
        public static List<Category> AllCategories
        {
            get; set;
        } = new List<Category>();

        public static List<Category> AllFeaturedCategories
        {
            get; set;
        } = new List<Category>();
    }
}
