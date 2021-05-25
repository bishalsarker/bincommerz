using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Orders
{
    [Table("order_payment_logs", Schema = "bcomm_om")]
    public class OrderPaymentLog : BaseEntity
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public string TransactionMethod { get; set; }

        [Required]
        public string TransactionType { get; set; }

        public double Amount { get; set; }

        public string PaymentNotes { get; set; }

        public DateTime LogDateTime { get; set; }
    }
}
