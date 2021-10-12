using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Widgets
{
    public class SlideResponse
    {
        public string Id { get; set; }

        public string ImageURL { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }

        public string ButtonUrl { get; set; }
    }
}
