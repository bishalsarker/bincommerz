﻿// <auto-generated />
using System;
using BComm.PM.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BComm.PM.Repositories.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BComm.PM.Models.Auth.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ReorderLevel")
                        .HasColumnType("double precision");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserHashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("UserHashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("shops", "bcomm_user");
                });

            modelBuilder.Entity("BComm.PM.Models.Auth.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("users", "bcomm_user");
                });

            modelBuilder.Entity("BComm.PM.Models.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<string>("ParentCategoryId")
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TagHashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("Slug")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("categories", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Coupons.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<int>("DiscountType")
                        .HasColumnType("integer");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<double>("MinimumPurchaseAmount")
                        .HasColumnType("double precision");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("coupons", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Images.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ThumbnailImage")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("images", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Orders.DeliveryCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("delivery_charges", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CouponCode")
                        .HasColumnType("text");

                    b.Property<double>("CouponDiscount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CurrentProcessId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<double>("OrderSubTotal")
                        .HasColumnType("double precision");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentNotes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PlacedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("ShippingCharge")
                        .HasColumnType("double precision");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalDue")
                        .HasColumnType("double precision");

                    b.Property<double>("TotalPayable")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CurrentProcessId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("IsCompleted")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("orders", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Orders.OrderItemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ProductId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("order_items", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Orders.OrderPaymentLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentNotes")
                        .HasColumnType("text");

                    b.Property<string>("TransactionMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("order_payment_logs", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Orders.OrderProcessLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("order_process_logs", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Pages.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkTitle")
                        .HasColumnType("text");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Category")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("Slug")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("pages", "bcomm_cm");
                });

            modelBuilder.Entity("BComm.PM.Models.Processes.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .HasColumnType("text");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StepNumber")
                        .HasColumnType("integer");

                    b.Property<string>("TrackingDescription")
                        .HasColumnType("text");

                    b.Property<string>("TrackingTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("processes", "bcomm_om");
                });

            modelBuilder.Entity("BComm.PM.Models.Products.ImageGalleryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ProductId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("image_gallery", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("StockQuantity")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("products", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Subscriptions.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("CanAddCustomDomain")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DurationType")
                        .HasColumnType("integer");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IntervalInMonths")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("NextPaymentOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PlanName")
                        .HasColumnType("text");

                    b.Property<int>("ProductEntryLimit")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ValidTill")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("subscriptions", "bcomm_user");
                });

            modelBuilder.Entity("BComm.PM.Models.Tags.ProductTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ProductHashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TagHashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductHashId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("product_tags", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("tags", "bcomm_pm");
                });

            modelBuilder.Entity("BComm.PM.Models.Templates.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("templates", "bcomm_cm");
                });

            modelBuilder.Entity("BComm.PM.Models.UrlMappings.UrlMappings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Cname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CnameId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DnsId")
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UrlMapType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("UrlMapType")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("url_mappings", "bcomm_user");
                });

            modelBuilder.Entity("BComm.PM.Models.Widgets.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ShopId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("sliders", "bcomm_cm");
                });

            modelBuilder.Entity("BComm.PM.Models.Widgets.SliderImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ButtonText")
                        .HasColumnType("text");

                    b.Property<string>("ButtonUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SliderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HashId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("SliderId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("slider_images", "bcomm_cm");
                });
#pragma warning restore 612, 618
        }
    }
}
