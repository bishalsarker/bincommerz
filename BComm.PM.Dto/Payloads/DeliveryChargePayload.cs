using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class DeliveryChargePayload
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
