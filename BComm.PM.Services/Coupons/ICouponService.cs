using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Coupons
{
    public interface ICouponService
    {
        Task<Response> AddNewCoupon(CouponPayload newCouponRequest, string shopId);
        Task<Response> DeleteCoupon(string couponId);
        Task<Response> GetAllCoupons(string shopId);
        Task<Response> GetCouponById(string couponId);
        Task<Response> UpdateCoupon(CouponUpdatePayload newCouponRequest);
    }
}