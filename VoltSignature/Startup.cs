using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VoltSignature.Common.Logger;
using VoltSignature.Core.Extensions;
using VoltSignature.Model.Account;
using VoltSignature.Model.Settings; 
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
            string connectionString = Configuration.GetConnectionString("Mongo");
            services.AddMongoContext(connectionString);

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
                     })
                     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                     {
                         options.Events = new JwtBearerEvents
                         {
                             OnMessageReceived = context =>
                             {
                                 if (context.Request.Path.Value.StartsWith("/Account/Register") && context.Request.Query.TryGetValue("token", out StringValues token))
                                     context.Token = token;
                                 return Task.CompletedTask;
                             }
                         };
                         options.TokenValidationParameters = new TokenValidationParameters
                         { 
                             ValidateIssuer = true, 
                             ValidIssuer = RegisterTokenOptions.ISSUER, 
                             ValidateAudience = true, 
                             ValidAudience = RegisterTokenOptions.AUDIENCE, 
                             ValidateLifetime = true, 
                             IssuerSigningKey = RegisterTokenOptions.GetSymmetricSecurityKey(), 
                             ValidateIssuerSigningKey = true,
                         };
                     });

            services.AddAuthorization();
            services.Configure<LoggerSetting>(_loggerConfig);
            services.AddMapper();
            services.RegisterServices(Configuration);
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

            app.UseMiddleware<ErrorHandlerMiddleware>();
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

  

        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseKestrel()
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build()
              .Run();
    }
}