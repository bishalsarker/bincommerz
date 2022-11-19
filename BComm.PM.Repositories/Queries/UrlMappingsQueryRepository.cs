using BComm.PM.Models.UrlMappings;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class UrlMappingsQueryRepository : IUrlMappingsQueryRepository
    {
        private readonly string _connectionString;

        public UrlMappingsQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<UrlMappings> GetDomainById(string domainId)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"HashId\"=@domainid;",
                    TableNameConstants.UrlMappingsTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return (await conn.QueryAsync<UrlMappings>(query, new { domainid = domainId })).FirstOrDefault();
            }
        }

        public async Task<UrlMappings> GetDomainByName(string domainName, string shopId)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"Url\" = @domainname and \"UrlMapTyp\"e=2 and \"ShopId\"=@shopid",
                    TableNameConstants.UrlMappingsTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return (await conn.QueryAsync<UrlMappings>(query, new { domainname = domainName, shopid = shopId })).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<UrlMappings>> GetUrlMappingsListByType(UrlMapTypes mapType, string shopId)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"UrlMapType\"=@maptype and \"ShopId\"=@shopid",
                    TableNameConstants.UrlMappingsTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return await conn.QueryAsync<UrlMappings>(query, new { maptype = mapType, shopid = shopId });
            }
        }

        public async Task<IEnumerable<UrlMappings>> GetAllUrlMappingsListByType(UrlMapTypes mapType)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"UrlMapType\"=@maptype;",
                    TableNameConstants.UrlMappingsTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return await conn.QueryAsync<UrlMappings>(query, new { maptype = mapType });
            }
        }
    }
}
