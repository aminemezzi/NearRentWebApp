using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NeaRentWeb.Components;
using NeaRentWeb.Components.Account;
using NeaRentWeb.Components.Common;
using System.Security.Claims;
namespace NeaRentWeb
{
    public class Program
    {
        protected NavigationManager navigationManager;


        public static void Main(string[] args)
        {
            //IdentityModelEventSource.ShowPII = true;

            var builder = WebApplication.CreateBuilder(args);

            Endpoints.UserManager = builder.Configuration.GetValue<string>("Endpoints:UserManager");
            Endpoints.Products = builder.Configuration.GetValue<string>("Endpoints:Products");
            Endpoints.KeyCloak = builder.Configuration.GetValue<string>("Endpoints:KeyCloak");

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = _ => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = builder.Configuration.GetValue<string>("Oidc:Authority");
                options.ClientId = builder.Configuration.GetValue<string>("Oidc:ClientId");
                options.ClientSecret = builder.Configuration.GetValue<string>("Oidc:ClientSecret");
                options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Oidc:RequireHttpsMetadata"); // disable only in dev env
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
                options.MapInboundClaims = true;
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("roles");
                options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.Events = new OpenIdConnectEvents
                {
                    OnUserInformationReceived = context =>
                    {
                        MapKeyCloakRolesToRoleClaims(context);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Tokens.IdToken = context.TokenEndpointResponse.IdToken;
                        Tokens.AccessToken = context.TokenEndpointResponse.AccessToken;
                        Tokens.IdTokenHint = context.TokenEndpointResponse.IdTokenHint;

                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.MapGet("/login", async (HttpContext context) =>
            {
                await context.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = "/" });
            });
            //app.MapGet("/register", async (HttpContext context) =>
            //{
                
            //    //https://${KC_HOST}/auth/realms/acme-apps/protocol/openid-connect/registrations?client_id=${CLIENT_ID}&redirect_uri=${CLIENT_REDIRECT_URI}
            //    //await context("oidc", new AuthenticationProperties { RedirectUri = "/" });
            //});
            app.MapGet("/logout", async (HttpContext context) =>
            {
                //await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                await context.SignOutAsync(new AuthenticationProperties { RedirectUri = "/" });
               
            });

            app.Run();






           // var builder = WebApplication.CreateBuilder(args);

            // Endpoints.UserManager = builder.Configuration.GetValue<string>("Endpoints:UserManager");
            // Endpoints.Products = builder.Configuration.GetValue<string>("Endpoints:Products");
            // Endpoints.KeyCloak = builder.Configuration.GetValue<string>("Endpoints:KeyCloak");

            // builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            // //builder.Services.AddRazorPages();
            // //builder.Services.AddServerSideBlazor();
            // builder.Services.AddHttpClient();

            // builder.Services.Configure<CookiePolicyOptions>(options =>
            // {
            //     options.CheckConsentNeeded = _ => false;
            //     options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            // });

            // builder.Services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            // })
            // .AddCookie()
            // .AddOpenIdConnect(options =>
            // {
            //     options.Authority = builder.Configuration.GetValue<string>("Oidc:Authority");
            //     options.ClientId = builder.Configuration.GetValue<string>("Oidc:ClientId");
            //     options.ClientSecret = builder.Configuration.GetValue<string>("Oidc:ClientSecret");
            //     options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Oidc:RequireHttpsMetadata"); // disable only in dev env
            //     options.ResponseType = OpenIdConnectResponseType.Code;
            //     options.GetClaimsFromUserInfoEndpoint = true;
            //     options.SaveTokens = true;
            //     options.MapInboundClaims = true;
            //     options.Scope.Clear();
            //     options.Scope.Add("openid");
            //     options.Scope.Add("profile");
            //     options.Scope.Add("email");
            //     options.Scope.Add("roles");
            //     options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //     options.Events = new OpenIdConnectEvents
            //     {
            //         OnUserInformationReceived = context =>
            //         {
            //             MapKeyCloakRolesToRoleClaims(context);
            //             return Task.CompletedTask;
            //         },
            //         OnTokenValidated = context =>
            //         {
            //             Tokens.IdToken = context.TokenEndpointResponse.IdToken;
            //             Tokens.AccessToken = context.TokenEndpointResponse.AccessToken;
            //             Tokens.IdTokenHint = context.TokenEndpointResponse.IdTokenHint;

            //             return Task.CompletedTask;
            //         }
            //     };
            // });

            // builder.Services.AddBlazorBootstrap();

            // builder.Services.AddAuthorization();

            // builder.Services.AddCascadingAuthenticationState();

            // var app = builder.Build();

            // if (app.Environment.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Error");
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            //// app.UseRouting();

            // app.UseAntiforgery();
            // app.UseCookiePolicy();
            // app.UseAuthentication();
            // app.UseAuthorization();

            // app.MapGet("/logout", async (HttpContext context) =>
            // {
            //     await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //     await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            // });

            // // Add additional endpoints required by the Identity /Account Razor components.
            // app.MapAdditionalIdentityEndpoints();

            // app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            // app.Run();
        }

        private static void MapKeyCloakRolesToRoleClaims(UserInformationReceivedContext context)
        {
            if (context.Principal.Identity is not ClaimsIdentity claimsIdentity)
                return;

            if (context.User.RootElement.TryGetProperty("preferred_username", out var username))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, username.ToString()));
            }

            if (context.User.RootElement.TryGetProperty("realm_access", out var realmAccess)
                && realmAccess.TryGetProperty("roles", out var globalRoles))
            {
                foreach (var role in globalRoles.EnumerateArray())
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            if (context.User.RootElement.TryGetProperty("resource_access", out var clientAccess)
                && clientAccess.TryGetProperty(context.Options.ClientId, out var client)
                && client.TryGetProperty("roles", out var clientRoles))
            {
                foreach (var role in clientRoles.EnumerateArray())
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }
        }
    }
}
