using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class UrlMappingPayload
    {
        [Required]
        public string Url { get; set; }
    }
}
