using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Repositories.ProductImageRepo
{
    public interface IProductImageWriteRepository : IWriteRepository<ProductImageFile>
    {
    }
}
