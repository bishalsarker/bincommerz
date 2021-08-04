using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Tags
{
    public class TagsPopularityResponse
    {
        public string TagId { get; set; }

        public string TagName { get; set; }

        public double Percentage { get; set; }
    }
}
