using Microservice.Framework.Domain;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities
{
    public class ServiceMethod : Entity<ServiceMethodId>
    {
        #region Properties

        public string Name { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public DateTime? ExecutionTime { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public ExecutionsStatusType ExecutionsStatus { get; set; }

        public string ExecutedBy { get; set; }

        #endregion
    }
}
