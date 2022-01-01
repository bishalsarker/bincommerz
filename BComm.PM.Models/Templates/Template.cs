using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Templates
{
    [Table("templates", Schema = "bcomm_cm")]
    public class Template : WithHashId
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public string ShopId { get; set; }
    }
}
