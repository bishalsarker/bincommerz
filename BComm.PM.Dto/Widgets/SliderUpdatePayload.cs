using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Widgets
{
    public class SliderUpdatePayload
    {
        public string Id { get; set; }

        public SliderPayload Slider { get; set; }
    }
}
