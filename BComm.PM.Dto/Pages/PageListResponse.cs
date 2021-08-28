using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Pages
{
    public class PageListResponse
    {
        public IEnumerable<PageResponse> NavLink { get; set; }

        public IEnumerable<PageResponse> Support { get; set; }

        public IEnumerable<PageResponse> Faq { get; set; }

        public IEnumerable<PageResponse> About { get; set; }
    }
}
