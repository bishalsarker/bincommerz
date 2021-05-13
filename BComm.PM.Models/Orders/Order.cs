using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Orders
{
    [Table("orders", Schema = "bcomm_om")]
    public class Order : WithHashId
    {
        [Required]
        public string ShopId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public double TotalPayable { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public bool IsPaid { get; set; }

        public string PaymentNotes { get; set; }

        public DateTime PlacedOn { get; set; }

        public bool IsCompleted { get; set; }
    }
}
