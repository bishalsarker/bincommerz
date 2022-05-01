using BComm.PM.Models.Auth;
using BComm.PM.Models.Categories;
using BComm.PM.Models.Coupons;
using BComm.PM.Models.Images;
using BComm.PM.Models.Orders;
using BComm.PM.Models.Pages;
using BComm.PM.Models.Processes;
using BComm.PM.Models.Products;
using BComm.PM.Models.Tags;
using BComm.PM.Models.Templates;
using BComm.PM.Models.UrlMappings;
using BComm.PM.Models.Widgets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace BComm.PM.Repositories.Common
{
    public class MainDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductTags> ProductTags { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ImageGalleryItem> ImageGallery { get; set; }

        public DbSet<Process> Processes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<DeliveryCharge> DeliveryCharges { get; set; }

        public DbSet<OrderItemModel> OrderItems { get; set; }

        public DbSet<OrderProcessLog> OrderProcessLogs { get; set; }

        public DbSet<OrderPaymentLog> OrderPaymentLogs { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<SliderImage> SliderImages { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UrlMappings> UrlMappings { get; set; }


        private readonly string _connectionString;

        public MainDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, 
                x => x.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                "bcomm"));
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

            modelBuilder.Entity<Category>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Category>()
               .HasIndex(t => t.Slug)
               .IsUnique(false)
               .IsClustered(false);

            modelBuilder.Entity<Category>()
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

            modelBuilder.Entity<DeliveryCharge>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<DeliveryCharge>()
                .HasIndex(t => t.ShopId)
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

            modelBuilder.Entity<Coupon>()
                .HasIndex(t => t.Code)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Coupon>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Coupon>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Page>()
               .HasIndex(t => t.HashId)
               .IsUnique(true)
               .IsClustered(false);

            modelBuilder.Entity<Page>()
               .HasIndex(t => t.Category)
               .IsUnique(false)
               .IsClustered(false);

            modelBuilder.Entity<Page>()
               .HasIndex(t => t.Slug)
               .IsUnique(false)
               .IsClustered(false);

            modelBuilder.Entity<Page>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Slider>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Slider>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Template>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<Template>()
                .HasIndex(t => t.ShopId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<SliderImage>()
                .HasIndex(t => t.HashId)
                .IsUnique(true)
                .IsClustered(false);

            modelBuilder.Entity<SliderImage>()
                .HasIndex(t => t.SliderId)
                .IsUnique(false)
                .IsClustered(false);

            modelBuilder.Entity<Shop>()
               .HasIndex(t => t.HashId)
               .IsUnique(true)
               .IsClustered(false);

            modelBuilder.Entity<Shop>()
               .HasIndex(t => t.UserHashId)
               .IsUnique(true)
               .IsClustered(false);

            modelBuilder.Entity<User>()
               .HasIndex(t => t.HashId)
               .IsUnique(true)
               .IsClustered(false);

            modelBuilder.Entity<UrlMappings>()
               .HasIndex(t => t.ShopId)
               .IsUnique(false)
               .IsClustered(false);

            modelBuilder.Entity<UrlMappings>()
               .HasIndex(t => t.UrlMapType)
               .IsUnique(false)
               .IsClustered(false);

            modelBuilder.Entity<UrlMappings>()
               .HasIndex(t => t.HashId)
               .IsUnique(true)
               .IsClustered(false);
        }
    }
}
