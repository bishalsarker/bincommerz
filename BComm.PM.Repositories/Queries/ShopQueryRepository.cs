using BComm.PM.Models.Auth;
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
    public class ShopQueryRepository : IShopQueryRepository
    {
        private readonly string _connectionString;

        public ShopQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<Shop> GetShopByUserId(string userId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where UserHashId=@userid", TableNameConstants.ShopsTable)
                    .ToString();

                var result = await conn.QueryAsync<Shop>(query, new { @userid = userId });

                return result.FirstOrDefault();
            }
        }

        public async Task<Shop> GetShopById(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@shopid", TableNameConstants.ShopsTable)
                    .ToString();

                var result = await conn.QueryAsync<Shop>(query, new { @shopid = shopId });

                return result.FirstOrDefault();
            }
        }
    }
}
