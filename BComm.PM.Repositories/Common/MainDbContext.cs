using BComm.PM.Models.Images;
using BComm.PM.Models.Orders;
using BComm.PM.Models.Processes;
using BComm.PM.Models.Products;
using BComm.PM.Models.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BComm.PM.Repositories.Common
{
    public class MainDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductTags> ProductTags { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ImageGalleryItem> ImageGallery { get; set; }

        public DbSet<Process> Processes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItemModel> OrderItems { get; set; }

        public DbSet<OrderProcessLog> OrderProcessLogs { get; set; }

        public DbSet<OrderPaymentLog> OrderPaymentLogs { get; set; }


        private readonly string _connectionString;

        public MainDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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

            modelBuilder.Entity<ProductTags>()
                .HasIndex(t => t.ProductHashId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Models.Images.Image>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<ImageGalleryItem>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<ImageGalleryItem>()
                .HasIndex(t => t.ProductId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Process>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Process>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Order>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Order>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Order>()
                .HasIndex(t => t.IsCompleted)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Order>()
                .HasIndex(t => t.CurrentProcessId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<OrderItemModel>()
                .HasIndex(t => t.OrderId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<OrderItemModel>()
                .HasIndex(t => t.ProductId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<OrderProcessLog>()
                .HasIndex(t => t.OrderId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<OrderPaymentLog>()
                .HasIndex(t => t.OrderId)
                .IsUnique(false)
                .IsClustered(false);
        }
    }
}
