using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public interface IOrderPaymentService
    {
        Task<Response> AddPayment(OrderPaymentPayload newOrderPaymentRequest);
        Task<Response> DeductPayment(OrderPaymentPayload newOrderPaymentRequest);
        Task<Response> GetPaymentLogs(string orderId);
    }
}