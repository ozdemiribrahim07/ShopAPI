using Microsoft.EntityFrameworkCore;
using ShopAPI.Application.Repositories;
using ShopAPI.Domain.Entities.Common;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        readonly ShopContext _context; 

        public ReadRepository(ShopContext context)
        {
            _context = context;
        }

        public DbSet<T> Table
            => _context.Set<T>();

        public IQueryable<T> GetAll()
                => Table;

        public async Task<T> GetAsync(Expression<Func<T, bool>> method)
                => await Table.FirstOrDefaultAsync(method);

        public async Task<T> GetByIdAsync(string id)
                => await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
                => Table.Where(method);
    }
}
