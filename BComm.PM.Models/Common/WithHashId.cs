using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Models.Common
{
    public class WithHashId : BaseEntity
    {
        [Required]
        public string HashId { get; set; }
    }
}
