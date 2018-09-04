using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoltSignature.Core.Mapper;
using VoltSignature.Core.Services;
using VoltSignature.DbCore.Context;
using VoltSignature.Files.Interface;
using VoltSignature.Interface;

namespace VoltSignature.Core.Extensions
{
    public static class Extensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IStorage, Storage.Storage>();
            services.AddTransient<IFileService, FileService>();
        }

        public static void AddMapper(this IServiceCollection services)
        {
            var profile = new MappingProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            services.AddSingleton(config.CreateMapper());
        }

        public static void AddMongoContext(this IServiceCollection services, string connectionString)
        {
            services.AddTransient((factory) =>
            {
                return new MongoContext(connectionString);
            })
            .AddTransient<IFileStorage, MongoFileContext>((factory) =>
            {
                return new MongoFileContext(connectionString);
            });
        }
    }
}