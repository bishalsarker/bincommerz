using BComm.PM.Models.Coupons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ICouponQueryRepository
    {
        Task<IEnumerable<Coupon>> GetAllCoupons(string shopId);
        Task<Coupon> GetCouponByCode(string code, string shopId);
        Task<Coupon> GetCouponById(string couponId);
    }
}