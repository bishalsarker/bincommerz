using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class CouponPayload
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string DiscountType { get; set; }

        [Required]
        public double MinimumPurchaseAmount { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
