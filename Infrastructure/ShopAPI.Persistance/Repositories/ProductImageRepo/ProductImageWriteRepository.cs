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
    public class ProductImageWriteRepository : WriteRepository<ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(ShopContext context) : base(context)
        {
        }
    }
}
