using BComm.PM.Models.Templates;
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
    public class TemplateQueryRepository : ITemplateQueryRepository
    {
        private readonly string _connectionString;

        public TemplateQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<Template>> GetTemplates(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select \"Name\", \"IsDefault\", \"HashId\" from {0} where \"ShopId\"=@shopid", TableNameConstants.TemplatesTable)
                    .ToString();

                return await conn.QueryAsync<Template>(query, new { @shopid = shopId });
            }
        }

        public async Task<Template> GetTemplate(string templateId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"HashId\"=@hashid", TableNameConstants.TemplatesTable)
                    .ToString();

                var result = await conn.QueryAsync<Template>(query, new { @hashid = templateId });

                return result.FirstOrDefault();
            }
        }

        public async Task<Template> GetDefaultTemplate(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"ShopId\"=@shopid and \"IsDefault\"=@isdefault", TableNameConstants.TemplatesTable)
                    .ToString();

                var result = await conn.QueryAsync<Template>(query, new { @shopid = shopId, @isdefault = true });

                return result.FirstOrDefault();
            }
        }
    }
}
