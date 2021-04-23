using BComm.PM.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BComm.PM.Models.Tags
{
    [Table("product_tags", Schema = "bcomm_pm")]
    public class ProductTags : BaseEntity
    {
        [Required]
        public string TagHashId { get; set; }

        [Required]
        public string ProductHashId { get; set; }
    }
}
