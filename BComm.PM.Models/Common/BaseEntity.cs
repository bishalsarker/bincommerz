using System;
using System.ComponentModel.DataAnnotations;

namespace BComm.PM.Models.Common
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }

        //audit fields
        public DateTime CreatedOn { get; set; }
    }
}
