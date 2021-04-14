using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class TagPayload
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
