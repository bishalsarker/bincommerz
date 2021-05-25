using BComm.PM.Models.Orders;
using BComm.PM.Repositories.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        public async Task<IEnumerable<Order>> GetOrders(string shopId, bool isCompleted)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} " +
                    "where ShopId=@shopid and IsCompleted=@iscompleted " +
                    "order by PlacedOn desc", TableNameConstants.OrdersTable)
                    .ToString();

                return await conn.QueryAsync<Order>(query, new { @shopid = shopId, @iscompleted = isCompleted });
            }
        }

        public async Task<Order> GetOrder(string orderId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} " +
                    "where HashId=@orderid", TableNameConstants.OrdersTable)
                    .ToString();

                var result = await conn.QueryAsync<Order>(query, new { @orderid = orderId });

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<OrderProcessLog>> OrderLogs(string orderId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} " +
                    "where OrderId=@orderid " +
                    "order by LogDateTime desc", TableNameConstants.OrderProcessLogsTable)
                    .ToString();

                return await conn.QueryAsync<OrderProcessLog>(query, new { @orderid = orderId });
            }
        }

        public async Task<IEnumerable<OrderPaymentLog>> OrderPaymentLogs(string orderId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} " +
                    "where OrderId=@orderid " +
                    "order by LogDateTime desc", TableNameConstants.OrderPaymentLogsTable)
                    .ToString();

                return await conn.QueryAsync<OrderPaymentLog>(query, new { @orderid = orderId });
            }
        }

        public async Task<IEnumerable<OrderItemModel>> GetOrderItems(string orderId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} " +
                    "where OrderId=@orderid", TableNameConstants.OrderItemsTable)
                    .ToString();

                return await conn.QueryAsync<OrderItemModel>(query, new { @orderid = orderId });
            }
        }
    }
}
