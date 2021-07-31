using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public interface IOrderService
    {
        Task<Response> AddNewOrder(OrderPayload newOrderRequest, string shopId);
        Task<Response> CancelOrder(OrderUpdatePayload orderUpdatePayload);
        Task<Response> CompleteOrder(OrderUpdatePayload orderUpdatePayload);
        Task<Response> DeleteOrder(string orderId);
        Task<Response> GetAllOrders(string shopId, bool isCompleted);
        Task<Response> GetCanceledOrders(string shopId);
        Task<Response> GetOrder(string orderId);

        Task<Response> TrackOrder(string orderId);
        Task<Response> UpdateProcess(ProcessUpdatePayload processUpdateRequest);
    }
}