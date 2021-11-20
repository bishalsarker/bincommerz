using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Widgets
{
    public class SliderImageUpdatePayload
    {
        public string Id { get; set; }

        public SliderImagePayload SliderImage { get; set; }
    }
}
