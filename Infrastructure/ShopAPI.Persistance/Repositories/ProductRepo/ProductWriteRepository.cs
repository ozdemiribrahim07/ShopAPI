using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories.ProductRepo
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ShopContext context) : base(context)
        {
        }
    }
}
