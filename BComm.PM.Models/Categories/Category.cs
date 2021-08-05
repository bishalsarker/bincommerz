using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Categories
{
    [Table("categories", Schema = "bcomm_pm")]
    public class Category : WithHashId
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string TagHashId { get; set; }

        public string ParentCategoryId { get; set; }

        [Required]
        public string ShopId { get; set; }
    }
}
