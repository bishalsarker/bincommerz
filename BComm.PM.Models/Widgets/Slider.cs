using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Widgets
{
    [Table("sliders", Schema = "bcomm_cm")]
    public class Slider : WithHashId
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShopId { get; set; }
    }
}
