using Microsoft.AspNetCore.SignalR;
using ShopAPI.Application.Abstraction.Hubs;
using ShopAPI.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.SignalR.HubService
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;
        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(SignalrTypeNames.ProductAddedMessage, message);
        }
    }
}
