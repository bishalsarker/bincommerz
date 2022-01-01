using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Template
{
    public class TemplateUpdatePayload
    {
        public string Id { get; set; }

        public TemplatePayload TemplateData { get; set; }
    }
}
