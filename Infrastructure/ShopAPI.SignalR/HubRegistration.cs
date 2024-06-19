using Microsoft.AspNetCore.Builder;
using ShopAPI.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHub(this WebApplication app)
        {
            app.MapHub<ProductHub>("/productHub");
        }


    }
}
