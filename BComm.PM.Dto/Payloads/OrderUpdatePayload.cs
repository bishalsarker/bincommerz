using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class OrderUpdatePayload
    {
        [Required]
        public string OrderId { get; set; }
    }
}
