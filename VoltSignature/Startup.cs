using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.SpaServices.Webpack;
using VoltSignature.PostgreSQL.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Storage;
using VoltSignature.MongoDb.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using VoltSignature.Core.Mapper;
using VoltSignature.Interface;
using VoltSignature.Core.Services;

namespace VoltSignature.UI
{
	public class Startup
	{

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string PostgresConnectionString = "Host=localhost;Port=5432;Database=SignatureDb;Username=postgres;Password=fsgpsa";
            //string MSSQLconnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VoltSignatureDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string mongoDbConnectionString = "mongodb://localhost:27017/SignatureDb";

            services.AddDbContext<DbSignatureContext>(options => options
                                                                .UseNpgsql(PostgresConnectionString, b => b.MigrationsAssembly("VoltSignature.PostgreSQL"))
                                                                //.UseSqlServer(MSSQLconnectionString, b => b.MigrationsAssembly("VoltSignature"))
                                                                );
            services.AddMongoContext(mongoDbConnectionString);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) 
                     .AddCookie(options =>
                     {
                         options.LoginPath = new PathString("/Account/Login");
                         options.Events.OnRedirectToLogin = context =>
                         {
                             if (context.Request.Path.Value.StartsWith("/api"))
                             {
                                 context.Response.Clear();
                                 context.Response.StatusCode = 401;
                                 return Task.FromResult(0);
                             }
                             context.Response.Redirect(context.RedirectUri);
                             return Task.FromResult(0);
                         };
                     });


            services.AddAuthorization();
            AddServices(services);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseDeveloperExceptionPage();
			if (env.IsDevelopment())
			{
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
					HotModuleReplacement = true,
					ReactHotModuleReplacement = true,
                    HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
                });
			}

            app.UseAuthentication();
            app.UseStaticFiles();
			loggerFactory.AddConsole();
			app.UseMvc(routes =>
			{

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


                routes.MapRoute(
                    name: "Login",
                    template: "{controller}/{action}/{id?}");


                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
		}

        public void AddServices(IServiceCollection services)
        {
            services.AddTransient<IStorage, Storage>(); 
            services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
        }

    }
}
