using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.WebEncoders;
using System.Threading.Tasks;
using Staller.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using System.Linq;
using ElCamino.AspNet.Identity.AzureTable.Model;
using Staller.Web.Models;
using System;
using Microsoft.AspNet.Hosting;
using Staller.Web.Services;

namespace Staller.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddAzureTableStores<ApplicationDbContext>(new Func<IdentityConfiguration>(() =>
                {
                    return Configuration.Get<IdentityConfiguration>("IdentityAzureTable:identityConfiguration");
                }))
                .AddDefaultTokenProviders()
                .CreateAzureTablesIfNotExists<ApplicationDbContext>(); //can remove after first run;


            //services.AddOptions();
            //services.Configure<ConnectionStringsOptions>(Configuration);
            services.AddMvc();
            //services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            //services.AddAuthorization(options => options. )

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        public Startup(IHostingEnvironment env)//, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var configurationBuilder = new ConfigurationBuilder()
                //.SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile("config.local.json", true);

            if (env.IsEnvironment("Development"))
            {
                // This reads the configuration keys from the secret store.
                //configurationBuilder.AddUserSecrets();
            }

            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();

        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    LoginPath = new PathString("/login")
            //});

            //app.UseGoogleAuthentication(options =>
            //{
            //    options.ClientId = Configuration.Instance.Authentication.Google.ClientId;
            //    options.ClientSecret = Configuration.Instance.Authentication.Google.ClientSecret;
            //});

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    LoginPath = new PathString("/login")
            //});



            //// See config.json
            //app.UseOAuthAuthentication(new OAuthOptions
            //{
            //    AuthenticationScheme = "Google-AccessToken",
            //    DisplayName = "Google-AccessToken",
            //    ClientId = Configuration.Instance.Authentication.Google.ClientId,
            //    ClientSecret = Configuration.Instance.Authentication.Google.ClientSecret,
            //    CallbackPath = new PathString("/signin-google-token"),
            //    AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint,
            //    TokenEndpoint = GoogleDefaults.TokenEndpoint,
            //    Scope = { "openid", "profile", "email" },
            //    SaveTokensAsClaims = true
            //});

            //// See config.json
            //// https://console.developers.google.com/project
            //app.UseGoogleAuthentication(new GoogleOptions
            //{
            //    ClientId = Configuration.Instance.Authentication.Google.ClientId,
            //    ClientSecret = Configuration.Instance.Authentication.Google.ClientSecret,
            //    Events = new OAuthEvents()
            //    {
            //        OnRemoteError = ctx =>

            //        {
            //            ctx.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.UrlEncode(ctx.Error.Message));
            //            ctx.HandleResponse();
            //            return Task.FromResult(0);
            //        }
            //    }
            //});





            //// Choose an authentication type
            //app.Map("/login", signoutApp =>
            //{
            //    signoutApp.Run(async context =>
            //    {
            //        var authType = context.Request.Query["authscheme"];
            //        if (!string.IsNullOrEmpty(authType))
            //        {
            //            // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
            //            // send them to the home page instead (/).
            //            await context.Authentication.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
            //            return;
            //        }

            //        context.Response.ContentType = "text/html";
            //        await context.Response.WriteAsync("<html><body>");
            //        await context.Response.WriteAsync("Choose an authentication scheme: <br>");
            //        foreach (var type in context.Authentication.GetAuthenticationSchemes())
            //        {
            //            await context.Response.WriteAsync("<a href=\"?authscheme=" + type.AuthenticationScheme + "\">" + (type.DisplayName ?? "(suppressed)") + "</a><br>");
            //        }
            //        await context.Response.WriteAsync("</body></html>");
            //    });
            //});

            //// Sign-out to remove the user cookie.
            //app.Map("/logout", signoutApp =>
            //{
            //    signoutApp.Run(async context =>
            //    {
            //        context.Response.ContentType = "text/html";
            //        await context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //        await context.Response.WriteAsync("<html><body>");
            //        await context.Response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
            //        await context.Response.WriteAsync("<a href=\"/\">Home</a>");
            //        await context.Response.WriteAsync("</body></html>");
            //    });
            //});

            //// Display the remote error
            //app.Map("/error", errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.ContentType = "text/html";
            //        await context.Response.WriteAsync("<html><body>");
            //        await context.Response.WriteAsync("An remote failure has occurred: " + context.Request.Query["FailureMessage"] + "<br>");
            //        await context.Response.WriteAsync("<a href=\"/\">Home</a>");
            //        await context.Response.WriteAsync("</body></html>");
            //    });
            //});

            //// Deny anonymous request beyond this point.
            //app.Use(async (context, next) =>
            //{
            //    if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
            //    {
            //        // The cookie middleware will intercept this 401 and redirect to /login
            //        await context.Authentication.ChallengeAsync();
            //        return;
            //    }
            //    await next();
            //});


            //// Display user information
            //app.Run(async context =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.WriteAsync("<html><body>");
            //    await context.Response.WriteAsync("Hello " + (context.User.Identity.Name ?? "anonymous") + "<br>");
            //    foreach (var claim in context.User.Claims)
            //    {
            //        await context.Response.WriteAsync(claim.Type + ": " + claim.Value + "<br>");
            //    }
            //    await context.Response.WriteAsync("<a href=\"/logout\">Logout</a>");
            //    await context.Response.WriteAsync("</body></html>");
            //});
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
