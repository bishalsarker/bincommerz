using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Coupons
{
    public class CouponResponse
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string DiscountType { get; set; }

        public double MinimumPurchaseAmount { get; set; }

        public double Discount { get; set; }

        public bool IsActive { get; set; }
    }
}
