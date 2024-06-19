using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopAPI.Application.Abstraction.Services;
using ShopAPI.Application.Repositories.CustomerRepo;
using ShopAPI.Application.Repositories.FileRepo;
using ShopAPI.Application.Repositories.OrderRepo;
using ShopAPI.Application.Repositories.ProductImageRepo;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Persistance.Contexts;
using ShopAPI.Persistance.Repositories.CustomerRepo;
using ShopAPI.Persistance.Repositories.FileRepo;
using ShopAPI.Persistance.Repositories.OrdeRepo;
using ShopAPI.Persistance.Repositories.ProductImageRepo;
using ShopAPI.Persistance.Repositories.ProductRepo;
using ShopAPI.Persistance.Services;
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

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ShopContext>();

          
                
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>(); 
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
            services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

        }

    }
}
