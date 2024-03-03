using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using NeaRentWeb.Components.Common;
using System.Data;

namespace NeaRentWeb.Components.Account
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal CurrentUser
        {
            get; set;
        }

        public AuthStateProvider()
        {
            this.CurrentUser = this.GetAnonymous();
        }

        private ClaimsPrincipal GetUser(string userName, string id, string role)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Sid, id),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role)
            }, "Authentication type");
            return new ClaimsPrincipal(identity);
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

        public async Task<string?> Login(string userName, string password, Endpoints endpoints, AzureSettings azureSettings, UserTokenProvider userTokenProvider)
        {
            string? result = "";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpRequestMessage requestMessage = new HttpRequestMessage(
                            HttpMethod.Post, 
                            endpoints.AzureIdentityProvider + 
                            azureSettings.TenantId + 
                            "/oauth2/v2.0/token"))
                    {
                        DateTime date = DateTime.Now;

                        var values = new List<KeyValuePair<string, string>>();
                        values.Add(new KeyValuePair<string, string>("client_id", azureSettings.ClientId));
                        values.Add(new KeyValuePair<string, string>("grant_type", "password"));
                      //  values.Add(new KeyValuePair<string, string>("scope", azureSettings.DefaultScopes));
                        values.Add(new KeyValuePair<string, string>("username", userName));
                        values.Add(new KeyValuePair<string, string>("password", password));
                        var content = new FormUrlEncodedContent(values);

                        requestMessage.Content = content;

                        var response = await httpClient.SendAsync(requestMessage);
                        response.EnsureSuccessStatusCode();

                        Dictionary<string, string> tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

                        userTokenProvider.AccessToken = tokenDetails.GetValueOrDefault("access_token");
                        userTokenProvider.AccessTokenExpiration = date.AddSeconds(int.Parse(tokenDetails.GetValueOrDefault("expires_in")));
                        userTokenProvider.RefreshToken = tokenDetails.GetValueOrDefault("refresh_token");
                        userTokenProvider.RefreshTokenExpiration = date.AddSeconds(int.Parse(tokenDetails.GetValueOrDefault("refresh_expires_in")));
                        result = null;

                        this.CurrentUser = this.GetUser("Piet", "000", "test");
                        var task = this.GetAuthenticationStateAsync();
                        this.NotifyAuthenticationStateChanged(task);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public Task<AuthenticationState> Logout()
        {
            this.CurrentUser = this.GetAnonymous();
            var task = this.GetAuthenticationStateAsync();
            this.NotifyAuthenticationStateChanged(task);
            return task;
        }
    }
}
