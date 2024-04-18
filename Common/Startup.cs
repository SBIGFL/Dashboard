using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using common;
using DapperContext = Context.DapperContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication; // Add this line

namespace common
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // ConfigureServices method in Startup.cs
        public void ConfigureServices(IServiceCollection services)
        {
            // Other service configurations

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your desired session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configure method in Startup.cs
        public void Configure(IApplicationBuilder app)
        {

            app.UseSession();
        }



    }
}
