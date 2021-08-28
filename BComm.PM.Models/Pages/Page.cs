using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Pages
{
    [Table("pages", Schema = "bcomm_cm")]
    public class Page : WithHashId
    {
        [Required]
        public string PageTitle { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public PageCategories Category { get; set; }

        public string LinkTitle { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ShopId { get; set; }
    }

    public enum PageCategories
    {
        UsefulLink,
        NavbarLink,
        Support,
        Article,
        FAQ,
        About,
        ExternalLink
    }
}
