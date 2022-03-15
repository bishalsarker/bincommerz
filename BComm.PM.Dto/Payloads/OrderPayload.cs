using BComm.PM.Dto.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class OrderPayload
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string PaymentNotes { get; set; }

        public string DeliveryChargeId { get; set; }

        [Required]
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
