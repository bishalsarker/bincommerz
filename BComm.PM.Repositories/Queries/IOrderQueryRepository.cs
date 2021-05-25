using BComm.PM.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IOrderQueryRepository
    {
        Task<Order> GetOrder(string orderId);
        Task<IEnumerable<OrderItemModel>> GetOrderItems(string orderId);
        Task<IEnumerable<Order>> GetOrders(string shopId, bool isCompleted);
        Task<IEnumerable<OrderProcessLog>> OrderLogs(string orderId);
        Task<IEnumerable<OrderPaymentLog>> OrderPaymentLogs(string orderId);
    }
}