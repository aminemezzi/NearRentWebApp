namespace NeaRentWeb.Components.Models
{
    public class CurrentUser
    {
        public bool IsAuthenticated
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }

        public Guid UserID
        {
            get; set;
        }

        public Dictionary<string, string> Claims
        {
            get; set;
        }
    }
}
