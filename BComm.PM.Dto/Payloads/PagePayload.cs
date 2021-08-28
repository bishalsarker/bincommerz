using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class PagePayload
    {
        [Required]
        public string PageTitle { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Category { get; set; }

        public string LinkTitle { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
