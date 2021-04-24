using BComm.PM.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BComm.PM.Models.Products
{
    [Table("image_gallery", Schema = "bcomm_pm")]
    public class ImageGalleryItem : WithHashId
    {
        [Required]
        public string ImageId { get; set; }

        [Required]
        public string ProductId { get; set; }
    }
}
