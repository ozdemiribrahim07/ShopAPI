using Microsoft.EntityFrameworkCore;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Contexts
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
        }


        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<FileBase> FileBases { get; set; }
        DbSet<ProductImageFile> ProductImageFiles { get; set; }
        


        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow.Date;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow.Date;
                        break;
                }
            }


            return await base.SaveChangesAsync(cancellationToken);
        }



    }
}
