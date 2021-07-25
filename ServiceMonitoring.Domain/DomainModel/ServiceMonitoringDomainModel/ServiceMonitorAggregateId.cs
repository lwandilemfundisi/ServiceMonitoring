using Microservice.Framework.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ServiceMonitorAggregateId : Identity<ServiceMonitorAggregateId>
    {
        public ServiceMonitorAggregateId(string value) : base(value)
        {
        }
    }
}
