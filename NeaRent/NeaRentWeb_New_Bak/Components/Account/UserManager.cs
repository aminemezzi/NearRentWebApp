using Azure.Core;
using NeaRentWeb.Components.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NeaRentWeb.Components.Account
{
    public class UserManager
    {
        private Endpoints endpoints
        {
            get; set;
        }

        private AzureSettings azureSettings
        {
            get; set;
        }

        private UserTokenProvider userTokenProvider
        {
            get; set;
        }

        public UserManager(Endpoints endpoints, AzureSettings azureSettings, UserTokenProvider userTokenProvider)
        {
            this.endpoints = endpoints;
            this.azureSettings = azureSettings;
            this.userTokenProvider = userTokenProvider;
        }

        public async Task CreateNewUser(string firstName, string surname, string username, string password)
        {
            string? accessToken = null;

            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                //    using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoints.KeyCloakAdmin + "/protocol/openid-connect/token"))
                //    {
                //        var values = new List<KeyValuePair<string, string>>();
                //        values.Add(new KeyValuePair<string, string>("grant_type", keycloakConfiguration.AdminGrantType));
                //        values.Add(new KeyValuePair<string, string>("client_id", keycloakConfiguration.AdminClientID));
                //        values.Add(new KeyValuePair<string, string>("client_secret", keycloakConfiguration.AdminClientSecret));
                //        var content = new FormUrlEncodedContent(values);
                //        requestMessage.Content = content;

                //        var response = await httpClient.SendAsync(requestMessage);
                //        response.EnsureSuccessStatusCode();

                //        Dictionary<string, string> tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

                //        accessToken = tokenDetails.GetValueOrDefault("access_token");
                //    }
                //}

                //if (accessToken != null)
                //{
                //    using (HttpClient httpClient = new HttpClient())
                //    {
                //        using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/admin/realms/NeaRent/users"))
                //        {
                //            requestMessage.Headers.Clear();
                //            requestMessage.Headers.Add("Authorization", "Bearer " + accessToken);

                //            User user = new User();
                //            user.firstName = firstName;
                //            user.lastName = surname;
                //            user.email = username;
                //            user.username = username;
                //            user.credentials.Add(new User.Credentials(){ value = password });

                //            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(user),
                //                    Encoding.UTF8,
                //                    "application/json");

                //            var response = await httpClient.SendAsync(requestMessage);
                //            response.EnsureSuccessStatusCode();

                //            //Dictionary<string, string> tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

                //            //accessToken = tokenDetails.GetValueOrDefault("access_token");
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
        }

        public class User
        {
            public string firstName
            {
                get; set;
            }
            public string lastName
            {
                get; set;
            }
            public string email
            {
                get; set;
            }

            public bool emailVerified
            {
                get; set;
            } = true;

            public bool enabled
            {
                get; set;
            } = true;

         public string username
            {
                get; set;
            }

            public List<Credentials> credentials
            {
                get; set;
            } = new List<Credentials>();


            public class Credentials
            {
                public string type
                {
                    get; set;
                } = "password";

                public string value
                {

                    get; set;
                }

                public bool temporary
                {
                    get; set;
                } = false;
            }
        }

    }

    
}
