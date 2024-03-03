namespace NeaRentWeb.Components.Common
{
    public class AzureSettings
    {
        public string TenantId
        {
            get; set;
        }
        public string ClientId
        {
            get; set;
        }

        public string[] DefaultScopes
        {
            get; set;
        }
    }
}
