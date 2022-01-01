using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Template
{
    public class TemplateResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public string Content { get; set; }
    }
}
