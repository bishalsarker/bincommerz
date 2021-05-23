using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Orders
{
    [Table("order_process_logs", Schema = "bcomm_om")]
    public class OrderProcessLog : BaseEntity
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime LogDateTime { get; set; }
    }
}
