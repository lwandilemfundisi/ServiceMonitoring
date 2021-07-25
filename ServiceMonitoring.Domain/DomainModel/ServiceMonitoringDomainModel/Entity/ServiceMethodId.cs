using Microservice.Framework.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entity
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ServiceMethodId : Identity<ServiceMethodId>
    {
        public ServiceMethodId(string value)
            : base(value)
        {

        }
    }
}
