using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Coupons
{
    [Table("coupons", Schema = "bcomm_om")]

    public class Coupon : WithHashId
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public CouponDiscountTypes DiscountType { get; set; }

        [Required]
        public double MinimumPurchaseAmount { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string ShopId { get; set; }
    }

    public enum CouponDiscountTypes
    {
        Percentage = 1,
        FixedAmount = 2
    }

    public enum CouponDiscountDurationTypes
    {
        NoDuration = 1,
        FixedDuration = 2
    }
}
