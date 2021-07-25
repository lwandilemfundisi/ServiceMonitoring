using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Events
{
    [EventVersion("CreatedServiceMonitor", 1)]
    public class CreatedServiceMonitorEvent : AggregateEvent<ServiceMonitorAggregate, ServiceMonitorAggregateId>
    {
        public CreatedServiceMonitorEvent(string serviceName)
        {
            ServiceName = serviceName;
        }

        public string ServiceName { get; }
    }
}
