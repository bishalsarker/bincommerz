using BComm.PM.Models.Categories;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly string _connectionString;

        public CategoryQueryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<Category>> GetParentCategories(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.\"HashId\", {0}.\"Name\", {0}.\"Description\", {0}.\"ImageId\", {0}.\"Slug\", {0}.\"OrderNumber\", " +
                    "{1}.\"Name\" as TagName " +
                    "from {0} " +
                    "inner join {1} on {0}.\"TagHashId\" = {1}.\"HashId\" " +
                    "where {0}.\"ShopId\"=@shopid and {0}.\"ParentCategoryId\" is null", 
                    TableNameConstants.CategoriesTable,
                    TableNameConstants.TagsTable)
                    .ToString();

                return await conn.QueryAsync<Category>(query, new { @shopid = shopId });
            }
        }

        public async Task<Category> GetCategory(string categoryId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"HashId\"=@hashid", TableNameConstants.CategoriesTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @hashid = categoryId });

                return result.FirstOrDefault();
            }
        }

        public async Task<Category> GetLastAddedCategory(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select top (1) * from {0} where \"ShopId\"=@shopid " +
                    "order by \"CreatedOn\" desc;", TableNameConstants.CategoriesTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @shopid = shopId });

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Category>> GetChildCategories(string categoryId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.\"HashId\", {0}.\"Name\", {0}.\"Description\", {0}.\"ImageId\", {0}.\"Slug\", " +
                    "{1}.\"Name\" as TagName " +
                    "from {0} " +
                    "inner join {1} on {0}.\"TagHashId\" = {1}.\"HashId\" " +
                    "where {0}.\"ParentCategoryId\" = @hashid",
                    TableNameConstants.CategoriesTable,
                    TableNameConstants.TagsTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @hashid = categoryId });

                return result;
            }
        }

        public async Task<Category> GetCategoryBySlug(string slug, string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"Slug\"=@slug and \"ShopId\"=@shopid", TableNameConstants.CategoriesTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @slug = slug, @shopid = shopId });

                return result.FirstOrDefault();
            }
        }

        public async Task DeleteChildCategories(string categoryId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("delete from {0} where \"ParentCategoryId\"=@catid", TableNameConstants.CategoriesTable)
                    .ToString();

                await conn.ExecuteAsync(query, new { @catid = categoryId });
            }
        }
    }
}
