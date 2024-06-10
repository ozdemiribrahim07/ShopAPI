using ShopAPI.Application.Repositories.ProductImageRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories.ProductImageRepo
{
    public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageReadRepository
    {
        public ProductImageReadRepository(ShopContext context) : base(context)
        {
        }
    }
}
