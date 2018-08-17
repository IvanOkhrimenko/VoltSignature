using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using VoltSignature.Core.Logger;
using VoltSignature.Core.Mapper;
using VoltSignature.Core.Services;
using VoltSignature.Interface;
using VoltSignature.Model.Settings;
using VoltSignature.MongoDb.Extension;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Storage;
using VoltSignature.UI.Middleware;

namespace VoltSignature.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _loggerConfig = Configuration.GetSection("Logging:LoggerSetting");
        }

        public IConfiguration Configuration { get; }
        private IConfigurationSection _loggerConfig { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbSignatureContext>(options => options
                                                                .UseNpgsql(Configuration.GetConnectionString("Postgres"), b => b.MigrationsAssembly("VoltSignature.PostgreSQL"))
                                                                //.UseSqlServer(Configuration.GetConnectionString("MSSQL"), b => b.MigrationsAssembly("VoltSignature"))
                                                                );
            services.AddMongoContext(Configuration.GetConnectionString("Mongo"));

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
        public void Configure(IApplicationBuilder app, IOptions<LoggerSetting> loggersetting, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true,
                    HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
                });
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            loggerFactory.AddConsole().AddDebug().AddProvider(new LoggerProvider(_loggerConfig, env.EnvironmentName));

            app.UseMiddleware<ErrorHadnlerMiddleware>();
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
            services.Configure<LoggerSetting>(_loggerConfig);

            services.AddTransient<IStorage, Storage>();
            services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
        }

        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseKestrel()
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build()
              .Run();
    }
}