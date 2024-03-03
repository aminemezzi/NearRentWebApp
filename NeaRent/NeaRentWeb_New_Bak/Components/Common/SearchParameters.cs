namespace NeaRentWeb.Components.Common
{
    public class SearchParameters
    {
        private Dictionary<string, string>? searchParameters = null;

        public Dictionary<string, string>? SearchParameterList
        {
            get
            {
                return searchParameters;
            }
            set
            {
                searchParameters = value;
                SearchParametersChanged?.Invoke(null, EventArgs.Empty);
            }
        } 

        public event EventHandler SearchParametersChanged;

    }
}
