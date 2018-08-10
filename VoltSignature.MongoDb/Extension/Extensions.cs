using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VoltSignature.MongoDb.Context;

namespace VoltSignature.MongoDb.Extension
{
    public static class Extensions
    {
        public static void AddMongoContext(this IServiceCollection services,string connectionString)
        {
            services.AddTransient<IStoreImage, SignatureImageContext>((factory) =>
            {
                return new SignatureImageContext(connectionString);
            });
        }
    }
}
