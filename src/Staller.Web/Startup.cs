using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Staller.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddOptions();
            //services.Configure<ConnectionStringsOptions>(Configuration);
            services.AddMvc();

        }

        //public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        //{
        //    // Setup configuration sources.
        //    var configurationBuilder = new ConfigurationBuilder()
        //        .SetBasePath(appEnv.ApplicationBasePath)
        //        .AddJsonFile("config.json")
        //        .AddJsonFile($"config.{env.EnvironmentName}.json", true);

        //    if (env.IsEnvironment("Development"))
        //    {
        //        // This reads the configuration keys from the secret store.
        //        //configurationBuilder.AddUserSecrets();
        //    }

        //    configurationBuilder.AddEnvironmentVariables();
        //    Configuration = configurationBuilder.Build();

        //}



        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIISPlatformHandler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
