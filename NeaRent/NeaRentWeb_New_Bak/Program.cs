using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using NeaRentWeb.Components;
using NeaRentWeb.Components.Account;
using NeaRentWeb.Components.Common;

namespace NeaRentWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Read and store endpoints from appsettings
            Endpoints endpoints = new Endpoints();
            builder.Configuration.GetSection("Endpoints").Bind(endpoints);

            AzureSettings azureSettings = new AzureSettings();
            builder.Configuration.GetSection("AzureAd").Bind(azureSettings);

            builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd");

            builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor()
                            .AddMicrosoftIdentityConsentHandler();

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthorizationCore();

        //    builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddHttpClient();

            // Declare global variables
            UserTokenProvider userTokenProvider = new UserTokenProvider();
            VariableStorage variableStorage = new VariableStorage();
            ProductContainer productContainer = new ProductContainer();
            CategoryContainer categoryContainer = new CategoryContainer();
            Addresses addresses = new Addresses();
            SearchParameters searchParameters = new SearchParameters();

            builder.Services.AddSingleton<Endpoints>(endpoints);
            builder.Services.AddSingleton<AzureSettings>(azureSettings);
            builder.Services.AddSingleton<UserTokenProvider>(userTokenProvider);
            builder.Services.AddSingleton<VariableStorage>(variableStorage);
            builder.Services.AddSingleton<ProductContainer>(productContainer);
            builder.Services.AddSingleton<CategoryContainer>(categoryContainer);
            builder.Services.AddSingleton<Addresses>(addresses);
            builder.Services.AddSingleton<SearchParameters>(searchParameters);

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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add supported cultures
            string[] supportedCultures = new[] { "en-GB", "en-US" };
            app.UseRequestLocalization(options =>
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture(supportedCultures[0])
            );

            app.Run();
        }
    }
}
