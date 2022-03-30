using BComm.PM.Dto.Processes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderResponse
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public double TotalAmount { get; set; }

        public double Discount { get; set; }

        public double TotalPayable { get; set; }

        public double TotalDue { get; set; }

        public double ShippingCharge { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime PlacedOn { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsCompleted { get; set; }

        public ProcessResponse CurrentProcess { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderItemResponse> Items { get; set; }
    }
}
