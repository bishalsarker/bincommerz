using BComm.PM.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IDeliveryChargeQueryRepository
    {
        Task<IEnumerable<DeliveryCharge>> GetAllDeliveryCharges(string shopId);
        Task<DeliveryCharge> GetDeliveryChargeById(string deliveryChargeId);
    }
}