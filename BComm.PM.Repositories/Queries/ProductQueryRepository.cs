using BComm.PM.Models.Products;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly string _connectionString;

        public ProductQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<int> GetProductCount(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select count(Id) from {0} where ShopId=@shopid", TableNameConstants.ProductsTable)
                    .ToString();

                return await conn.ExecuteScalarAsync<int>(query, new { @shopid = shopId });
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Name, {0}.Description, {0}.Price, {0}.Discount, {0}.StockQuantity " +
                    "from {0} where {0}.ShopId=@shopid",
                    TableNameConstants.ProductsTable)
                    .ToString();

                return await conn.QueryAsync<Product>(query, new { @shopid = shopId });
            }
        }

        public async Task<IEnumerable<Product>> GetOutOfStockProducts(string shopId, double reodrerLevel)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select HashId, Name, StockQuantity " +
                    "from {0} where ShopId=@shopid and StockQuantity <= @reorderlevel",
                    TableNameConstants.ProductsTable)
                    .ToString();

                return await conn.QueryAsync<Product>(query, new { @shopid = shopId, @reorderlevel = reodrerLevel });
            }
        }

        public async Task<IEnumerable<Product>> GetProducts(
            string shopId, string tagId, string sortCol, string sortOrder, int offset, int rows, string searchQuery)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Name, {0}.Price, {0}.Discount, {0}.StockQuantity, " +
                    "{1}.Directory as ImageDirectory, {1}.ThumbnailImage as ImageUrl " +
                    "from {0} " +
                    "inner join {1} on {0}.ImageUrl={1}.HashId and {0}.ShopId=@shopid ",
                    TableNameConstants.ProductsTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                if (!string.IsNullOrEmpty(tagId))
                {
                    query = query + new StringBuilder()
                    .AppendFormat("inner join {0} on {1}.HashId={0}.ProductHashId and {0}.TagHashId=@tagid ",
                    TableNameConstants.ProductTagsTable,
                    TableNameConstants.ProductsTable)
                    .ToString();
                }

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query = query + new StringBuilder()
                    .AppendFormat("where {0}.Name like N'%" + searchQuery + " %' or " +
                    "{0}.Name like N'% " + searchQuery + " %' or " +
                    "{0}.Name like N'% " + searchQuery + "%' ",
                    TableNameConstants.ProductsTable)
                    .ToString();
                }

                query = query + new StringBuilder()
                    .AppendFormat("order by {0}.{1} {2} ",
                    TableNameConstants.ProductsTable,
                    sortCol,
                    sortOrder)
                    .ToString();

                query = query + new StringBuilder()
                    .AppendFormat("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY",
                    offset,
                    rows)
                    .ToString();

                Console.WriteLine(query);

                return await conn.QueryAsync<Product>(query, new { @shopid = shopId, @tagid = tagId });
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByKeywords(string keyword, string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Name, {0}.Description, {0}.Price, {0}.Discount, {0}.StockQuantity, " +
                    "{1}.Directory as ImageDirectory, {1}.ThumbnailImage as ImageUrl " +
                    "from {0} " +
                    "inner join {1} on {0}.ImageUrl={1}.HashId and {0}.ShopId=@shopid " +
                    "where {0}.Name like N'% " + keyword + " %'" +
                    "or {0}.Description like N'% " + keyword + " %'",
                    TableNameConstants.ProductsTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                return await conn.QueryAsync<Product>(query, new { @shopid = shopId });
            }
        }

        public async Task<Product> GetProductById(string productId, bool resolveImage)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var queryAllCol = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@productid", TableNameConstants.ProductsTable)
                    .ToString();

                var queryWithImageDirectory = new StringBuilder()
                    .AppendFormat("select {0}.Name, {0}.Description, {0}.Price, {0}.Discount, {0}.StockQuantity, " +
                    "{1}.Directory as ImageDirectory, {1}.ThumbnailImage as ImageUrl " +
                    "from {0} " +
                    "left join {1} on {0}.ImageUrl={1}.HashId " +
                    "where {0}.HashId=@productid",
                    TableNameConstants.ProductsTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                var query = resolveImage ? queryWithImageDirectory : queryAllCol;

                var model = await conn.QueryAsync<Product>(query, new { @productid = productId });

                return model.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsBySlug(string slug, bool resolveImage)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var queryAllCol = new StringBuilder()
                    .AppendFormat("select * from {0} where Slug=@slug", TableNameConstants.ProductsTable)
                    .ToString();

                var queryWithImageDirectory = new StringBuilder()
                    .AppendFormat("select {0}.Name, {0}.Description, {0}.Price, {0}.Discount, {0}.StockQuantity, " +
                    "{1}.Directory as ImageDirectory, {1}.ThumbnailImage as ImageUrl " +
                    "from {0} " +
                    "left join {1} on {0}.ImageUrl={1}.HashId " +
                    "where {0}.Slug=@slug",
                    TableNameConstants.ProductsTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                var query = resolveImage ? queryWithImageDirectory : queryAllCol;

                return await conn.QueryAsync<Product>(query, new { @slug = slug });
            }
        }

        public async Task<IEnumerable<Product>> GetProductsById(List<string> productIds, string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select HashId, Name, Price, Discount, StockQuantity from {0} where HashId in @productids and ShopId=@shopid", TableNameConstants.ProductsTable)
                    .ToString();

                return await conn.QueryAsync<Product>(query, new { @productids = productIds, @shopid = shopId });
            }
        }

        public async Task<Product> GetProductByTag(string tagId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select Id, ShopId, Name, Description, StockQuantity from {0} where HashId=@tagid", TableNameConstants.ProductsTable)
                    .ToString();

                var model = await conn.QueryAsync<Product>(query, new { @tagid = tagId });

                return model.FirstOrDefault();
            }
        }

        public async Task UpdateProductStock(string productId, string shopId, double newStock)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("update {0} set StockQuantity=@newstock where HashId=@productids and ShopId=@shopid", TableNameConstants.ProductsTable)
                    .ToString();

                await conn.ExecuteAsync(query, new { @productids = productId, @shopid = shopId, @newstock = newStock });
            }
        }
    }
}
