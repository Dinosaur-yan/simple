using Microsoft.EntityFrameworkCore;
using Simple.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            SetTrackInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTrackInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTrackInfo()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is ITrack)
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var entityBase = entity as ITrack;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entityBase.UpdateTime = DateTime.Now;
                        break;

                    case EntityState.Added:
                        entityBase.CreateTime = DateTime.Now;
                        break;
                }
            }
        }
    }
}
