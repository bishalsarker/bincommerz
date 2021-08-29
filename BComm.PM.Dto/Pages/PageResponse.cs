using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Pages
{
    public class PageResponse
    {
        public string Id { get; set; }

        public string PageTitle { get; set; }

        public string Slug { get; set; }

        public string Category { get; set; }

        public string LinkTitle { get; set; }

        public string Content { get; set; }

        public bool IsPublished { get; set; }
    }
}
