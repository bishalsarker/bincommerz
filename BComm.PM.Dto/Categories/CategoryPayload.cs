using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto
{
    public class CategoryPayload
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string TagHashId { get; set; }

        [Required]
        public string Image { get; set; }

        public string ParentCategoryId { get; set; }
    }
}
