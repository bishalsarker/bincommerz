using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Images
{
    public class ImageResponse
    {
        public string Id { get; set; }

        public string OriginalImage { get; set; }

        public string ThumbnailImage { get; set; }
    }
}
