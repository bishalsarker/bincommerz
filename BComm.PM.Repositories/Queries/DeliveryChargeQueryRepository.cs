using BComm.PM.Models.Orders;
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
    public class DeliveryChargeQueryRepository : IDeliveryChargeQueryRepository
    {
        private readonly string _connectionString;

        public DeliveryChargeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<DeliveryCharge> GetDeliveryChargeById(string deliveryChargeId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * " +
                    "from {0} " +
                    "where {0}.HashId=@deliverychargeid",
                    TableNameConstants.DeliveryChargesTable)
                    .ToString();

                var result = await conn.QueryAsync<DeliveryCharge>(query, new { @deliverychargeid = deliveryChargeId });

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<DeliveryCharge>> GetAllDeliveryCharges(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Title, {0}.Amount " +
                    "from {0} " +
                    "where {0}.ShopId=@shopid",
                    TableNameConstants.DeliveryChargesTable)
                    .ToString();

                return await conn.QueryAsync<DeliveryCharge>(query, new { @shopid = shopId });
            }
        }
    }
}
