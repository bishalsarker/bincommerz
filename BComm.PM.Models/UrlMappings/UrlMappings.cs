using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.UrlMappings
{
    [Table("url_mappings", Schema = "bcomm_user")]
    public class UrlMappings : WithHashId
    {
        [Required]
        public string ShopId { get; set; }

        [Required]
        public UrlMapTypes UrlMapType { get; set; }

        [Required]
        public string Url { get; set; }
    }

    public enum UrlMapTypes
    {
        AppUrl = 1,
        Domain = 2
    }
}
