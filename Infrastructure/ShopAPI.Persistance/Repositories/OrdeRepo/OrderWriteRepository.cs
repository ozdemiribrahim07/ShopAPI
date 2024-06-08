using ShopAPI.Application.Repositories.OrderRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories.OrdeRepo
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ShopContext context) : base(context)
        {
        }
    }
}
