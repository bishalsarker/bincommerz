using BComm.PM.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IOrderQueryRepository
    {
        Task DeleteOrderItems(string orderId);
        Task DeleteOrderPaymentLogs(string orderId);
        Task DeleteOrderProcessLogs(string orderId);
        Task<IEnumerable<int>> GetAllOrderCount(string shopId);
        Task<IEnumerable<Order>> GetAllOrders(string shopId);
        Task<IEnumerable<Order>> GetAllOrdersForAllShops();
        Task<IEnumerable<int>> GetCanceledOrderCount(string shopId);
        Task<IEnumerable<Order>> GetCanceledOrders(string shopId);
        Task<Order> GetOrder(string orderId);
        Task<IEnumerable<int>> GetOrderCountByItems(List<string> orderIdList);
        Task<IEnumerable<int>> GetOrderCountByStatus(string shopId, bool isCompleted);
        Task<IEnumerable<OrderItemModel>> GetOrderItems(string orderId);
        Task<IEnumerable<Order>> GetOrders(string shopId, bool isCompleted);
        Task<IEnumerable<ProductOrderCount>> GetOrdersByMonthAndYear(List<string> orderIdList, string shopId, int month, int year, int limit);
        Task<IEnumerable<OrderProcessLog>> OrderLogs(string orderId);
        Task<IEnumerable<OrderPaymentLog>> OrderPaymentLogs(string orderId);
    }
}