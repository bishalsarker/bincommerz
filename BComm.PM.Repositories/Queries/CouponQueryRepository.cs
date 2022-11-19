using BComm.PM.Models.Coupons;
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
    public class CouponQueryRepository : ICouponQueryRepository
    {
        private readonly string _connectionString;

        public CouponQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<Coupon>> GetAllCoupons(string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.* from {0} " +
                    "where {0}.\"ShopId\"=@shopid",
                    TableNameConstants.CouponsTable)
                    .ToString();

                return await conn.QueryAsync<Coupon>(query, new { @shopid = shopId });
            }
        }

        public async Task<Coupon> GetCouponById(string couponId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.* from {0} " +
                    "where {0}.\"HashId\"=@couponid",
                    TableNameConstants.CouponsTable)
                    .ToString();

                return (await conn.QueryAsync<Coupon>(query, new { @couponid = couponId })).FirstOrDefault();
            }
        }

        public async Task<Coupon> GetCouponByCode(string code, string shopId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.* from {0} " +
                    "where {0}.\"Code\"=@couponcode and {0}.\"ShopId\"=@shopid",
                    TableNameConstants.CouponsTable)
                    .ToString();

                return (await conn.QueryAsync<Coupon>(query, new { @couponcode = code, @shopid = shopId })).FirstOrDefault();
            }
        }
    }
}
