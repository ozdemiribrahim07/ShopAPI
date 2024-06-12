using Microsoft.Extensions.DependencyInjection;
using ShopAPI.Application.Abstraction.Storage;
using ShopAPI.Application.Abstraction.Storage.AWS;
using ShopAPI.Application.Abstraction.TokenAbs;
using ShopAPI.Infrastructure.Services.Storage.Storage;
using ShopAPI.Infrastructure.Services.Storage.Storage.AWS;
using ShopAPI.Infrastructure.Services.Storage.Storage.Local;
using ShopAPI.Infrastructure.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IAwsStorage, AwsStorage>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }




    }
}
