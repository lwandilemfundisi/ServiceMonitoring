using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMonitoring.Api.Models
{
    public class AddServiceMethodLogRequestModel
    {
        public string ServiceId { get; set; }
        public string MethodName { get; set; }
        public string RequestUri { get; set; }
        public string Response { get; set; }
        public DateTime MethodExecutionTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }
}
