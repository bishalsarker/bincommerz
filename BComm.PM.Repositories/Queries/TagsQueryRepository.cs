using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class TagsQueryRepository : ITagsQueryRepository
    {
        private readonly string _connectionString;

        public TagsQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<ProductTags>> GetTagsByProductId(string productId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.\"TagHashId\", {1}.\"Name\" as TagName " +
                    "from {0} " +
                    "inner join {1} on {0}.\"TagHashId\" = {1}.\"HashId\" " +
                    "where \"ProductHashId\"=@productid", 
                    TableNameConstants.ProductTagsTable,
                    TableNameConstants.TagsTable)
                    .ToString();

               return await conn.QueryAsync<ProductTags>(query, new { @productid = productId });
            }
        }

        public async Task<IEnumerable<ProductTags>> GetTagReference(string tagId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select \"ProductHashId\" from {0} where \"TagHashId\"=@tagid", TableNameConstants.ProductTagsTable)
                    .ToString();

                return await conn.QueryAsync<ProductTags>(query, new { @tagid = tagId });
            }
        }

        public async Task DeleteTagsByProductId(string productId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("delete from {0} where \"ProductHashId\"=@productid", TableNameConstants.ProductTagsTable)
                    .ToString();

                await conn.ExecuteAsync(query, new { @productid = productId });
            }
        }

        public async Task<Tag> GetTag(string tagId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select \"Id\", \"ShopId\", \"Name\", \"Description\" from {0} where \"HashId\"=@tagid", TableNameConstants.TagsTable)
                    .ToString();

                var model = await conn.QueryAsync<Tag>(query, new { @tagid = tagId });

                return model.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Tag>> GetTags(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {  
                var query = new StringBuilder()
                    .AppendFormat("select \"Id\", \"HashId\", \"Name\", \"Description\" from {0} where \"ShopId\"=@shopid", TableNameConstants.TagsTable)
                    .ToString();

                return await conn.QueryAsync<Tag>(query, new { @shopid = shopId });
            }
        }
    }
}
