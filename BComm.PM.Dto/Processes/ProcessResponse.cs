using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Processes
{
    public class ProcessResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TrackingTitle { get; set; }

        public string StatusCode { get; set; }

        public int StepNumber { get; set; }
    }
}
