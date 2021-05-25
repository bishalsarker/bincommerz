using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class OrderPaymentPayload
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public string TransactionMethod { get; set; }

        public double Amount { get; set; }

        public string PaymentNotes { get; set; }
    }
}
