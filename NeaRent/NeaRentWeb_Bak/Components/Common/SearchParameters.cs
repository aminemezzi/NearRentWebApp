namespace NeaRentWeb.Components.Common
{
    public static class SearchParameters
    {
        private static Dictionary<string, string>? productSearch = null;

        public static Dictionary<string, string>? ProductSearch
        {
            get
            {
                return productSearch;
            }
            set
            {
                productSearch = value;
                ProductSearchChanged?.Invoke(null, EventArgs.Empty);
            }
        } 

        public static event EventHandler ProductSearchChanged;

    }
}
