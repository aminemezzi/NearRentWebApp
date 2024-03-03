using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web.UI;
using Microsoft.Identity.Web;
using System.Reflection;
using System.Security.Claims;
using System.Linq.Dynamic.Core;
using NeaRentWeb.Components;
using NeaRentWeb.Components.Common;
using NeaRentWeb.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth;
using UserManager.Models;
using User = UserManager.Models.User;

namespace NeaRentWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AzureSettings azureSettings = new AzureSettings();
            builder.Configuration.GetSection("AzureAd").Bind(azureSettings);
            builder.Services.AddSingleton<AzureSettings>(azureSettings);

            GoogleSettings googleSettings = new GoogleSettings();
            builder.Configuration.GetSection("Google").Bind(googleSettings);
            builder.Services.AddSingleton<GoogleSettings>(googleSettings);

            GoogleMapSettings googleMapSettings = new GoogleMapSettings();
            builder.Configuration.GetSection("GoogleMaps").Bind(googleMapSettings);
            builder.Services.AddSingleton<GoogleMapSettings>(googleMapSettings);

            UserTokenProvider userTokenProvider = new UserTokenProvider();
            builder.Services.AddSingleton<UserTokenProvider>(userTokenProvider);

            SiteSettings siteSettings = new SiteSettings();
            builder.Configuration.GetSection("SiteSettings").Bind(siteSettings);
            builder.Services.AddSingleton<SiteSettings>(siteSettings);

            User user = new User();
            builder.Services.AddSingleton<User>(user);

            builder.Services.AddScoped<NotifyResumeService>();

            Endpoints endpoints = new Endpoints();
            builder.Configuration.GetSection("Endpoints").Bind(endpoints);
            builder.Services.AddSingleton<Endpoints>(endpoints);

            builder.Services.AddRazorPages(); //.AddRazorPagesOptions(options => {options.RootDirectory = "/Components/Pages"; });

            // Read the connection string from the appsettings.json file
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            // Get HostingEnvironment
            var env = builder.Environment;
            builder.Configuration.AddJsonFile($"appsettings{env.EnvironmentName}.json", optional: true);
            builder.Configuration.AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
            // Add services to the container.
            builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents()
                    .AddMicrosoftIdentityConsentHandler();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<HttpContextAccessor>();

            builder.Services.AddSingleton<AuthenticationStateProvider, AuthStateProvider>();


            //builder.Services.AddHttpClient("https://accounts.google.com/.well-known/openid-configuration", client =>
            //{
            //    client.BaseAddress = new Uri("https://accounts.google.com/.well-known/openid-configuration");
            //});
            //builder.Services
            //    .AddScoped<IGoogleConnectService, GoogleConnectService>()
            //    .AddScoped<GoogleConnectService>()
            //    .AddScoped<AuthenticationStateProvider>(provider =>
            //        provider.GetRequiredService<GoogleConnectService>());

            //// This is where you wire up to events to detect when a user Log in
            //builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = googleSettings.client_id;
            //        options.ClientSecret = googleSettings.client_secret;
            //    })
            //    .AddMicrosoftIdentityWebApp(options =>
            //    {
            //        builder.Configuration.Bind("AzureAd", options);
            //        options.Events = new OpenIdConnectEvents
            //        {
            //            OnRedirectToIdentityProvider = async ctxt =>
            //            {
            //                // Invoked before redirecting to the identity provider to authenticate. 
            //                // This can be used to set ProtocolMessage.State
            //                // that will be persisted through the authentication process. 
            //                // The ProtocolMessage can also be used to add or customize
            //                // parameters sent to the identity provider.
            //                await Task.Yield();
            //            },
            //            OnAuthenticationFailed = async ctxt =>
            //            {
            //                // They tried to log in but it failed
            //                await Task.Yield();
            //            },
            //            OnSignedOutCallbackRedirect = async ctxt =>
            //            {
            //                ctxt.HttpContext.Response.Redirect(ctxt.Options.SignedOutRedirectUri);
            //                ctxt.HandleResponse();
            //                await Task.Yield();
            //            },
            //            OnTicketReceived = async ctxt =>
            //            {
            //                if (ctxt.Principal != null)
            //                {
            //                    if (ctxt.Principal.Identity is ClaimsIdentity identity)
            //                    {
            //                        var colClaims = await ctxt.Principal.Claims.ToDynamicListAsync();
            //                        var IdentityProvider = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.microsoft.com/identity/claims/identityprovider")?.Value;
            //                        var Objectidentifier = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            //                        var EmailAddress = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            //                        var FirstName = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            //                        var LastName = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            //                        var AzureB2CFlow = colClaims.FirstOrDefault(
            //                            c => c.Type == "http://schemas.microsoft.com/claims/authnclassreference")?.Value;
            //                        var auth_time = colClaims.FirstOrDefault(
            //                            c => c.Type == "auth_time")?.Value;
            //                        var DisplayName = colClaims.FirstOrDefault(
            //                            c => c.Type == "name")?.Value;
            //                        var idp_access_token = colClaims.FirstOrDefault(
            //                            c => c.Type == "idp_access_token")?.Value;
            //                    }
            //                }
            //                await Task.Yield();
            //            },
            //        };
            //    });
            builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
