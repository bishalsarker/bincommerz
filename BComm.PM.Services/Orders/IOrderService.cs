using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public interface IOrderService
    {
        Task<Response> AddNewOrder(OrderPayload newOrderRequest);
    }
}