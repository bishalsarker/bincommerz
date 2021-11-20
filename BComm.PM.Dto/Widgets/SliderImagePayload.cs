using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Widgets
{
    public class SliderImagePayload
    {
        public string SliderId { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }

        public string ButtonUrl { get; set; }
    }
}
