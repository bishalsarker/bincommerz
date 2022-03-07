using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public interface IDeliveryChargeService
    {
        Task<Response> AddNewDeliveryCharge(DeliveryChargePayload deliveryChargeRequest, string shopId);
        Task<Response> DeleteDeliveryCharge(string deliveryChargeId);
        Task<Response> GetAllDeliveryCharges(string shopId);
        Task<Response> GetDeliveryCharge(string deliveryChargeId);
        Task<Response> UpdateDeliveryCharge(DeliveryChargeUpdatePayload deliveryChargeUpdateRequest);
    }
}