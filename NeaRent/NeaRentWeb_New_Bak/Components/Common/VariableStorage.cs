using System.Globalization;

namespace NeaRentWeb.Components.Common
{
    public class VariableStorage
    {
        public CultureInfo Culture
        {
            get; set;
        } = CultureInfo.InvariantCulture;

        public List<BreadcrumbItem> Breadcrumbs
        {
            get; set;
        } = new List<BreadcrumbItem>();
    }
}
