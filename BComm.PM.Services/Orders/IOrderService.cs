using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public interface IOrderService
    {
        Task<Response> AddNewOrder(OrderPayload newOrderRequest);
        Task<Response> CancelOrder(OrderUpdatePayload orderUpdatePayload);
        Task<Response> CompleteOrder(OrderUpdatePayload orderUpdatePayload);
        Task<Response> GetAllOrders(string shopId, bool isCompleted);

        Task<Response> GetOrder(string orderId);

        Task<Response> TrackOrder(string orderId);
        Task<Response> UpdateProcess(ProcessUpdatePayload processUpdateRequest);
    }
}