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
    [EventVersion("AddedServiceMethodEntry", 1)]
    public class AddedServiceMethodEntryEvent : AggregateEvent<ServiceMonitorAggregate, ServiceMonitorAggregateId>
    {
        public AddedServiceMethodEntryEvent(ServiceMethod serviceMethod)
        {
            ServiceMethod = serviceMethod;
        }

        public ServiceMethod ServiceMethod { get; }
    }
}
