using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Widgets
{
    public class SliderImage : WithHashId
    {
        public string SliderId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }

        public string ButtonUrl { get; set; }

        public string ImageId { get; set; }
    }
}
