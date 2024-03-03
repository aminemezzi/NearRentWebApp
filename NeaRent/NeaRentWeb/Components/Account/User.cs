using System.IdentityModel.Tokens.Jwt;

namespace NeaRentWeb.Components.Account
{
    public class User
    {
        public Guid ID
        {
            get; set;
        }

        public string FirstName
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
    }
}
