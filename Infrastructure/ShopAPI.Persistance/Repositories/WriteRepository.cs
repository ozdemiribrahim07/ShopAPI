using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopAPI.Application.Repositories;
using ShopAPI.Domain.Entities.Common;
using ShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ShopContext _context;
        public WriteRepository(ShopContext context)
        {
            _context = context;
        }

        public DbSet<T> Table
            => _context.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
             EntityEntry<T> entityEntry = await Table.AddAsync(entity);
             return entityEntry.State == EntityState.Added;
        }
                    

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
           await Table.AddRangeAsync(entities);
           return true;
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            T entityUpdate = await Table.FirstOrDefaultAsync(x => x.Id == entity.Id);
            EntityEntry entityEntry = await Task.Run(() => Table.Update(entity));
            return entityEntry.State == EntityState.Modified;
        }



        public async Task<bool> DeleteAsync(T entity)
        {
           EntityEntry<T> entityEntry = await Task.Run(() => Table.Remove(entity));
           return entityEntry.State == EntityState.Deleted;
        }


        public async Task<bool> DeleteAsync(string id)
        {
            T entity = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            EntityEntry<T> entityEntry = await Task.Run(() => Table.Remove(entity));
            return entityEntry.State == EntityState.Deleted;
        }


        public bool DeleteRange(List<T> entity)
        {
            Table.RemoveRange(entity);
            return true;
         }



        public async Task<int> SaveAsync()
        {
          return await _context.SaveChangesAsync();
        }

       
    }
}
