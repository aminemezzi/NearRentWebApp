namespace NeaRentWeb.Components.Account
{
    /// <summary>
    /// Keeps track of different access tokens and their lifetimes
    /// </summary>
    public class UserTokenProvider
    {
        public string? AccessToken
        {
            get; set;
        } = null;

        public DateTime? AccessTokenExpiration
        {
            get; set;
        } = null;

        public string? RefreshToken
        {
            get; set;
        } = null;

        public DateTime? RefreshTokenExpiration
        {
            get; set;
        } = null;
    }
}
