using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderItemResponse
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double DiscountAmount { 
            get {
                if (Discount > 0)
                {
                    return Price - Discount;
                }
                else
                {
                    return Discount;
                }
            } 
        }

        public double Quantity { get; set; }

        public double Subtotal { 
            get { 
                if (Discount > 0)
                {
                    return Discount * Quantity;
                }
                else
                {
                    return Price * Quantity;
                }
            } 
        }
    }
}
