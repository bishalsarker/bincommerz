using BComm.PM.Models.Products;
using BComm.PM.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace BComm.PM.Repositories.Common
{
    public class MainDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Product>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Product>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);
        }
    }
}
