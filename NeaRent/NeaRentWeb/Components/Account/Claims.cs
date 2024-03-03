using static System.Net.Mime.MediaTypeNames;

namespace NeaRentWeb.Components.Account
{
    public class Claims
    {
        public string? IDP
        {
            get; set;
        }

        public Guid OID
        {
            get; set;
        }

        public Guid? SUB
        {
            get; set;
        }

        public string? GivenName
        {
            get; set;
        }

        public string? FamilyName
        {
            get; set;
        }

        public string? Name
        {
            get; set;
        }

        public bool? NewUser
        {
            get; set;
        }


        public string Email
        {
            get; set;
        }
        public string? TFP
        {
            get; set;
        }

        public Guid? AZP
        {
            get; set;
        }

        public string? Ver
        {
            get; set;
        }

        public int? IAT
        {
            get; set;
        }
        public Guid? AUD
        {
            get; set;
        }

        public int? EXP
        {
            get; set;
        }

        public string? ISS
        {
            get; set;
        }

        public int? NBF
        {
            get; set;
        }
    }
}
