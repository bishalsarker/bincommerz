using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class ShopUpdatePayload
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Logo { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public double ReorderLevel { get; set; }
    }
}
