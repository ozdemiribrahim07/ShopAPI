using ShopAPI.Application.Repositories.FileRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories.FileRepo
{
    public class FileReadRepository : ReadRepository<FileBase>, IFileReadRepository
    {
        public FileReadRepository(ShopContext context) : base(context)
        {
        }
    }
}
