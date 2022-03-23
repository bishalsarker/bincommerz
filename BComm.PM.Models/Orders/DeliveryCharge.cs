using BComm.PM.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BComm.PM.Models.Orders
{
    [Table("delivery_charges", Schema = "bcomm_om")]
    public class DeliveryCharge : WithHashId
    {
        [Required]
        public string ShopId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
