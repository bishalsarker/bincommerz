using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Orders
{
    [Table("order_items", Schema = "bcomm_om")]
    public class OrderItemModel : BaseEntity
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Quantity { get; set; }
    }
}
