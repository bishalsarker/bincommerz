using BComm.PM.Models.Subscriptions;
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
    public class SubscriptionQueryRepository : ISubscriptionQueryRepository
    {
        private readonly string _connectionString;

        public SubscriptionQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<Subscription> GetSubscription(string userId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"UserId\"=@userid", TableNameConstants.SubscriptionsTable)
                    .ToString();

                var result = await conn.QueryAsync<Subscription>(query, new { userid = userId });

                return result.FirstOrDefault();
            }
        }
    }
}
