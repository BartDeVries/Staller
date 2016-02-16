using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;

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
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
