using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BComm.PM.Repositories.Migrations
{
    public partial class Schema_Migation_To_PostgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bcomm_pm");

            migrationBuilder.EnsureSchema(
                name: "bcomm_om");

            migrationBuilder.EnsureSchema(
                name: "bcomm_cm");

            migrationBuilder.EnsureSchema(
                name: "bcomm_user");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    TagHashId = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<string>(type: "text", nullable: false),
                    ParentCategoryId = table.Column<string>(type: "text", nullable: true),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "coupons",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DiscountType = table.Column<int>(type: "integer", nullable: false),
                    MinimumPurchaseAmount = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_charges",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_charges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "image_gallery",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageId = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Directory = table.Column<string>(type: "text", nullable: false),
                    OriginalImage = table.Column<string>(type: "text", nullable: false),
                    ThumbnailImage = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_payment_logs",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    TransactionMethod = table.Column<string>(type: "text", nullable: false),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    PaymentNotes = table.Column<string>(type: "text", nullable: true),
                    LogDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_payment_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_process_logs",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_process_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    OrderSubTotal = table.Column<double>(type: "double precision", nullable: false),
                    TotalPayable = table.Column<double>(type: "double precision", nullable: false),
                    TotalDue = table.Column<double>(type: "double precision", nullable: false),
                    ShippingCharge = table.Column<double>(type: "double precision", nullable: false),
                    CouponCode = table.Column<string>(type: "text", nullable: true),
                    CouponDiscount = table.Column<double>(type: "double precision", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentNotes = table.Column<string>(type: "text", nullable: false),
                    PlacedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentProcessId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                schema: "bcomm_cm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageTitle = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    LinkTitle = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "processes",
                schema: "bcomm_om",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TrackingTitle = table.Column<string>(type: "text", nullable: false),
                    TrackingDescription = table.Column<string>(type: "text", nullable: true),
                    StatusCode = table.Column<string>(type: "text", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_tags",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagHashId = table.Column<string>(type: "text", nullable: false),
                    ProductHashId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    StockQuantity = table.Column<double>(type: "double precision", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shops",
                schema: "bcomm_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: false),
                    OrderCode = table.Column<string>(type: "text", nullable: false),
                    ReorderLevel = table.Column<double>(type: "double precision", nullable: false),
                    UserHashId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "slider_images",
                schema: "bcomm_cm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageId = table.Column<string>(type: "text", nullable: false),
                    SliderId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ButtonText = table.Column<string>(type: "text", nullable: true),
                    ButtonUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_slider_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sliders",
                schema: "bcomm_cm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "bcomm_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PlanId = table.Column<string>(type: "text", nullable: false),
                    PlanName = table.Column<string>(type: "text", nullable: true),
                    ProductEntryLimit = table.Column<int>(type: "integer", nullable: false),
                    CanAddCustomDomain = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DurationType = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionType = table.Column<int>(type: "integer", nullable: false),
                    IntervalInMonths = table.Column<int>(type: "integer", nullable: false),
                    ValidTill = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NextPaymentOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                schema: "bcomm_cm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "url_mappings",
                schema: "bcomm_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<string>(type: "text", nullable: false),
                    UrlMapType = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Cname = table.Column<string>(type: "text", nullable: false),
                    CnameId = table.Column<string>(type: "text", nullable: true),
                    DnsId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_mappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "bcomm_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_HashId",
                schema: "bcomm_pm",
                table: "categories",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_ShopId",
                schema: "bcomm_pm",
                table: "categories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_coupons_Code",
                schema: "bcomm_om",
                table: "coupons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coupons_HashId",
                schema: "bcomm_om",
                table: "coupons",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coupons_ShopId",
                schema: "bcomm_om",
                table: "coupons",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_charges_HashId",
                schema: "bcomm_om",
                table: "delivery_charges",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_charges_ShopId",
                schema: "bcomm_om",
                table: "delivery_charges",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_image_gallery_HashId",
                schema: "bcomm_pm",
                table: "image_gallery",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_image_gallery_ProductId",
                schema: "bcomm_pm",
                table: "image_gallery",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_images_HashId",
                schema: "bcomm_pm",
                table: "images",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_items_OrderId",
                schema: "bcomm_om",
                table: "order_items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_ProductId",
                schema: "bcomm_om",
                table: "order_items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_order_payment_logs_OrderId",
                schema: "bcomm_om",
                table: "order_payment_logs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_process_logs_OrderId",
                schema: "bcomm_om",
                table: "order_process_logs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CurrentProcessId",
                schema: "bcomm_om",
                table: "orders",
                column: "CurrentProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_HashId",
                schema: "bcomm_om",
                table: "orders",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_IsCompleted",
                schema: "bcomm_om",
                table: "orders",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ShopId",
                schema: "bcomm_om",
                table: "orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_Category",
                schema: "bcomm_cm",
                table: "pages",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_pages_HashId",
                schema: "bcomm_cm",
                table: "pages",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pages_ShopId",
                schema: "bcomm_cm",
                table: "pages",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_Slug",
                schema: "bcomm_cm",
                table: "pages",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_processes_HashId",
                schema: "bcomm_om",
                table: "processes",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_processes_ShopId",
                schema: "bcomm_om",
                table: "processes",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_product_tags_ProductHashId",
                schema: "bcomm_pm",
                table: "product_tags",
                column: "ProductHashId");

            migrationBuilder.CreateIndex(
                name: "IX_products_HashId",
                schema: "bcomm_pm",
                table: "products",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_ShopId",
                schema: "bcomm_pm",
                table: "products",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_shops_HashId",
                schema: "bcomm_user",
                table: "shops",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shops_UserHashId",
                schema: "bcomm_user",
                table: "shops",
                column: "UserHashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_slider_images_HashId",
                schema: "bcomm_cm",
                table: "slider_images",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_slider_images_SliderId",
                schema: "bcomm_cm",
                table: "slider_images",
                column: "SliderId");

            migrationBuilder.CreateIndex(
                name: "IX_sliders_HashId",
                schema: "bcomm_cm",
                table: "sliders",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sliders_ShopId",
                schema: "bcomm_cm",
                table: "sliders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_HashId",
                schema: "bcomm_user",
                table: "subscriptions",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_UserId",
                schema: "bcomm_user",
                table: "subscriptions",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_HashId",
                schema: "bcomm_pm",
                table: "tags",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_ShopId",
                schema: "bcomm_pm",
                table: "tags",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_templates_HashId",
                schema: "bcomm_cm",
                table: "templates",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_templates_ShopId",
                schema: "bcomm_cm",
                table: "templates",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_HashId",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_ShopId",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_UrlMapType",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "UrlMapType");

            migrationBuilder.CreateIndex(
                name: "IX_users_HashId",
                schema: "bcomm_user",
                table: "users",
                column: "HashId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "coupons",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "delivery_charges",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "image_gallery",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "images",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "order_items",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "order_payment_logs",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "order_process_logs",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "pages",
                schema: "bcomm_cm");

            migrationBuilder.DropTable(
                name: "processes",
                schema: "bcomm_om");

            migrationBuilder.DropTable(
                name: "product_tags",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "products",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "shops",
                schema: "bcomm_user");

            migrationBuilder.DropTable(
                name: "slider_images",
                schema: "bcomm_cm");

            migrationBuilder.DropTable(
                name: "sliders",
                schema: "bcomm_cm");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "bcomm_user");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "bcomm_pm");

            migrationBuilder.DropTable(
                name: "templates",
                schema: "bcomm_cm");

            migrationBuilder.DropTable(
                name: "url_mappings",
                schema: "bcomm_user");

            migrationBuilder.DropTable(
                name: "users",
                schema: "bcomm_user");
        }
    }
}
