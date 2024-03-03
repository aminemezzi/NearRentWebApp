using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using NeaRentWeb.Components.Common;
using System.Data;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.JSInterop;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace NeaRentWeb.Components.Account
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private Claims _claims
        {
            get; set;
        }

        private User _user
        {
            get; set;
        }

        private ClaimsPrincipal CurrentUser
        {
            get; set;
        }

        public AuthStateProvider()
        {
            this.CurrentUser = this.GetAnonymous();
        }

        private ClaimsPrincipal GetAnonymous()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Sid, ""),
            new Claim(ClaimTypes.Name, "Anonymous"),
            new Claim(ClaimTypes.Role, "Anonymous")
            }, null);
            return new ClaimsPrincipal(identity);
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var task = await Task.FromResult(new AuthenticationState(this.CurrentUser));
            return task;
        }

        public async Task<string?> LoginAzure(string userName, string password, Endpoints endpoints, AzureSettings azureSettings, UserTokenProvider userTokenProvider)
        {
            string? result = "";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("client_id", azureSettings.ClientId));
                    values.Add(new KeyValuePair<string, string>("grant_type", "password"));
                    values.Add(new KeyValuePair<string, string>("scope", azureSettings.Scope));
                    values.Add(new KeyValuePair<string, string>("username", userName));
                    values.Add(new KeyValuePair<string, string>("password", password));
                    values.Add(new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"));

                    using var requestMessage = new HttpRequestMessage(
                            HttpMethod.Post,
                            endpoints.AzureIdentityProvider +
                            azureSettings.SignUpSignInPolicyId +
                            "/oauth2/v2.0/token")
                    {
                        Content = new FormUrlEncodedContent(values)
                    };


                    using var response = await httpClient.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    Dictionary<string, string> tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

                    userTokenProvider.AccessToken = tokenDetails.GetValueOrDefault("access_token");
                    userTokenProvider.RefreshToken = tokenDetails.GetValueOrDefault("refresh_token");

                    DateTime date = DateTime.Now;
                    userTokenProvider.AccessTokenExpiration = date.AddSeconds(int.Parse(tokenDetails.GetValueOrDefault("expires_in")));
                    // userTokenProvider.RefreshTokenExpiration = date.AddSeconds(int.Parse(tokenDetails.GetValueOrDefault("refresh_expires_in")));
                    result = null;

                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(userTokenProvider.AccessToken);

                    Claims userClaims = new Claims();

                    if (jwt.Claims != null && jwt.Claims.Count() > 0)
                    {
                        foreach (Claim claim in jwt.Claims)
                        {
                            switch (claim.Type.ToString())
                            {
                                case "idp":
                                    userClaims.IDP = claim.Value;
                                    break;
                                case "oid":
                                    userClaims.OID = Guid.Parse(claim.Value);
                                    break;
                                case "sub":
                                    userClaims.SUB = Guid.Parse(claim.Value);
                                    break;
                                case "given_name":
                                    userClaims.GivenName = claim.Value;
                                    break;
                                case "family_name":
                                    userClaims.FamilyName = claim.Value;
                                    break;
                                case "name":
                                    userClaims.Name = claim.Value;
                                    break;
                                case "newUser":
                                    userClaims.NewUser = Convert.ToBoolean(claim.Value);
                                    break;
                                case "emails":
                                    userClaims.Email = claim.Value;
                                    break;
                                case "tfp":
                                    userClaims.TFP = claim.Value;
                                    break;
                                case "azp":
                                    userClaims.AZP = Guid.Parse(claim.Value);
                                    break;
                                case "ver":
                                    userClaims.Ver = claim.Value;
                                    break;
                                case "iat":
                                    userClaims.IAT = Convert.ToInt32(claim.Value);
                                    break;
                                case "aud":
                                    userClaims.AUD = Guid.Parse(claim.Value);
                                    break;
                                case "exp":
                                    userClaims.EXP = Convert.ToInt32(claim.Value);
                                    break;
                                case "iss":
                                    userClaims.ISS = claim.Value;
                                    break;
                                case "nbf":
                                    userClaims.NBF = Convert.ToInt32(claim.Value);
                                    break;
                                default:
                                    break;
                            }
                        }

                        _claims = userClaims;

                        ClaimsIdentity identity = new ClaimsIdentity(jwt.Claims, "Azure");
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        CurrentUser = principal;

                        _user = new User();
                        _user.ID = _claims.OID;
                        _user.FirstName = _claims.GivenName;
                        _user.Email = _claims.Email;
                    }
                    else
                    {
                        result = "noclaims";
                    }

                    var task = this.GetAuthenticationStateAsync();
                    this.NotifyAuthenticationStateChanged(task);
                }
            }
            catch (HttpRequestException ex)
            {
                if(ex.HttpRequestError == HttpRequestError.ConnectionError || ex.HttpRequestError == HttpRequestError.NameResolutionError)
                {
                    result = "cannotconnect";
                }
                else
                {
                    result = "authentication";
                }
            }
            catch (Exception ex)
            {
                result = "cannotconnect";
            }

            return result;
        }

        public async Task LoginGoogle(NavigationManager navigation, GoogleSettings googleSettings, UserTokenProvider userTokenProvider)
        {
            navigation.NavigateTo("https://accounts.google.com/o/oauth2/v2/auth?client_id=767818549829-22e50k4ta7jveupserhg21g447bt15oa.apps.googleusercontent.com&response_type=code&scope=openid email&redirect_uri=http://localhost:65060");
        }

        public Task<AuthenticationState> Logout()
        {
            this.CurrentUser = this.GetAnonymous();
            var task = this.GetAuthenticationStateAsync();
            this.NotifyAuthenticationStateChanged(task);
            return task;
        }

        public User GetUser()
        {
            return _user;
        }

        public Claims GetUserClaims()
        {
            return _claims;
        }
    }
}
