using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace NeaRentWeb.Components.Common
{
    public static class Tools
    {
        public static async Task<CultureInfo?> GetCulture(HttpContext context)
        {
            var rcf = context.Features.Get<IRequestCultureFeature>();

            if (rcf != null)
            {
                return rcf.RequestCulture.Culture;
            }
            return null;
        }
    }
}
