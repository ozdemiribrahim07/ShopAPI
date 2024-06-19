using Microsoft.Extensions.DependencyInjection;
using ShopAPI.Application.Abstraction.Hubs;
using ShopAPI.SignalR.HubService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();
        }

    }
}
