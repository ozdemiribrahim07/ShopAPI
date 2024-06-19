using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Abstraction.Hubs
{
    public interface IProductHubService
    {
        Task ProductAddedMessage(string message);


    }
}
