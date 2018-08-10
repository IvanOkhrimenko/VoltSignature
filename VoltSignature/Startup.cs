using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using VoltSignature.MongoDb.Context;
using VoltSignature.MongoDb.Extension;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Storage;

namespace VoltSignature
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string PostgresConnectionString = "Host=localhost;Port=5432;Database=SignatureDb;Username=postgres;Password=fsgpsa";
            //string MSSQLconnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VoltSignatureDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string mongoDbConnectionString = "mongodb://localhost:27017/SignatureDb";

            services.AddDbContext<DbSignatureContext>(options => options
                                                                .UseNpgsql(PostgresConnectionString, b => b.MigrationsAssembly("VoltSignature"))
                                                                //.UseSqlServer(MSSQLconnectionString, b => b.MigrationsAssembly("VoltSignature"))
                                                                );
            services.AddMongoContext(mongoDbConnectionString);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                     .AddCookie(options =>
                     {
                         options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                     });

            AddServices(services);

            services.AddMvc(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true,
                //    ReactHotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
             
             
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Login",
                    template: "{controller=Account}/{action=Login}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        public void AddServices(IServiceCollection services)
        {  
            services.AddTransient<IStorage, Storage>();
 
        }
    }
}
