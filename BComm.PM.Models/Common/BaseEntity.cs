using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Common
{
    public class BaseEntity
    {
        public string Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}
