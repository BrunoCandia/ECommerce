using Microsoft.EntityFrameworkCore;
using Order.Core.Common;

namespace Order.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Core.Entities.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Core.Entities.Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.LastModifiedBy).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.EmailAddress).HasMaxLength(100);
                entity.Property(e => e.AddressLine).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(100);
                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");

                entity.Property(e => e.CardName).HasMaxLength(100);
                entity.Property(e => e.CardNumber).HasMaxLength(20);
                entity.Property(e => e.Expiration).HasMaxLength(10);
                entity.Property(e => e.Cvv).HasMaxLength(5);
            });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateNow = DateTime.Now;
                        entry.Entity.CreatedDateUtcNow = DateTime.UtcNow;
                        entry.Entity.CreatedDateTimeOffsetNow = DateTimeOffset.Now;
                        entry.Entity.CreatedDateTimeOffsetUtcNow = DateTimeOffset.UtcNow;
                        entry.Entity.CreatedBy = "User"; //TODO: Replace with auth server
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "User"; //TODO: Replace with auth server
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
