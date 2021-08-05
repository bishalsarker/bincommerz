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
                    .AppendFormat("select HashId, Name, Description from {0} where ShopId=@shopid", TableNameConstants.CategoriesTable)
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
    }
}
