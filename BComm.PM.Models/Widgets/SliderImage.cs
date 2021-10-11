using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Widgets
{
    [Table("slider_images", Schema = "bcomm_cm")]
    public class SliderImage : WithHashId
    {
        [Required]
        public string ImageId { get; set; }

        [Required]
        public string SliderId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }

        public string ButtonUrl { get; set; }
    }
}
