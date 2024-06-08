using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopAPI.Application.Repositories.CustomerRepo;
using ShopAPI.Application.Repositories.OrderRepo;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Persistance.Contexts;
using ShopAPI.Persistance.Repositories.CustomerRepo;
using ShopAPI.Persistance.Repositories.OrdeRepo;
using ShopAPI.Persistance.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(opt => opt.UseNpgsql(Configuration.ConnectionString));

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>(); 
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        }

    }
}
