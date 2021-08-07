using BComm.PM.Models.Categories;
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
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly string _connectionString;

        public CategoryQueryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<Category>> GetCategories(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Name, {0}.Description, {0}.Slug, {1}.Name as TagName " +
                    "from {0} " +
                    "inner join {1} on {0}.TagHashId = {1}.HashId " +
                    "where {0}.ShopId=@shopid", 
                    TableNameConstants.CategoriesTable,
                    TableNameConstants.TagsTable)
                    .ToString();

                return await conn.QueryAsync<Category>(query, new { @shopid = shopId });
            }
        }

        public async Task<Category> GetCategory(string categoryId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@hashid", TableNameConstants.CategoriesTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @hashid = categoryId });

                return result.FirstOrDefault();
            }
        }

        public async Task<Category> GetCategoryBySlug(string slug, string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where Slug=@slug and ShopId=@shopid", TableNameConstants.CategoriesTable)
                    .ToString();

                var result = await conn.QueryAsync<Category>(query, new { @slug = slug, @shopid = shopId });

                return result.FirstOrDefault();
            }
        }
    }
}
