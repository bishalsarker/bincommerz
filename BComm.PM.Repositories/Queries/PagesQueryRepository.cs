using BComm.PM.Models.Pages;
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
    public class PagesQueryRepository : IPagesQueryRepository
    {
        private readonly string _connectionString;

        public PagesQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<Page> GetById(string pageId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * " +
                    "from {0} " +
                    "where {0}.\"HashId\"=@hashid",
                    TableNameConstants.PagesTable)
                    .ToString();

                var results = await conn.QueryAsync<Page>(query, new { @hashid = pageId });

                return results.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Page>> GetAllPages(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * " +
                    "from {0} " +
                    "where {0}.\"ShopId\"=@shopid",
                    TableNameConstants.PagesTable)
                    .ToString();

                return await conn.QueryAsync<Page>(query, new { @shopid = shopId });
            }
        }


        public async Task<IEnumerable<Page>> GetByCategory(PageCategories category, string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * " +
                    "from {0} " +
                    "where {0}.\"IsPublished\"='true' and {0}.\"Category\"=@cat and {0}.\"ShopId\"=@shopid",
                    TableNameConstants.PagesTable)
                    .ToString();

                return await conn.QueryAsync<Page>(query, new { @shopid = shopId, @cat = category });
            }
        }

        public async Task<Page> GetByCategoryAndSlug(PageCategories category, string slug, string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * " +
                    "from {0} " +
                    "where {0}.\"IsPublished\"='true' and {0}.\"Category\"=@cat and {0}.\"Slug\"=@slug and {0}.\"ShopId\"=@shopid",
                    TableNameConstants.PagesTable)
                    .ToString();

                var results = await conn.QueryAsync<Page>(query, new { @shopid = shopId, @cat = category, @slug = slug });

                return results.FirstOrDefault();
            }
        }
    }
}
