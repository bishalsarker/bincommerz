using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class ProcessUpdatePayload
    {
        [Required]
        public string ProcessId { get; set; }

        [Required]
        public string OrderId { get; set; }
    }
}
