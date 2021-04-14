using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Tags
{
    [Table("tags", Schema = "bcomm_pm")]
    public class Tag: BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ShopId { get; set; }
    }
}
