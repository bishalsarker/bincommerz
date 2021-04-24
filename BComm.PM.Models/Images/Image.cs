using BComm.PM.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BComm.PM.Models.Images
{
    [Table("images", Schema = "bcomm_pm")]
    public class Image : WithHashId
    {
        [Required]
        public string Directory { get; set; }

        [Required]
        public string OriginalImage { get; set; }

        public string ThumbnailImage { get; set; }
    }
}
