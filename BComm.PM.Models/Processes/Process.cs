using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Processes
{
    [Table("processes", Schema = "bcomm_om")]
    public class Process: WithHashId
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TrackingTitle { get; set; }

        public string TrackingDescription { get; set; }

        [Required]
        public string StatusCode { get; set; }

        public int StepNumber { get; set; }

        public string ShopId { get; set; }

    }
}
