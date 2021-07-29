using Microservice.Framework.Common;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.AspNetCore.SignalR;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Events;
using ServiceMonitoring.Domain.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Subscribers
{
    public class AddedServiceMethodEntryEventSubscriber :
        ISubscribeSynchronousTo<ServiceMonitorAggregate, ServiceMonitorAggregateId, AddedServiceMethodEntryEvent>
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHubContext<SendMethodLogsHub> _hubContext;

        public AddedServiceMethodEntryEventSubscriber(
            IHubContext<SendMethodLogsHub> hubContext,
            IJsonSerializer jsonSerializer)
        {
            _hubContext = hubContext;
            _jsonSerializer = jsonSerializer;
        }

        public Task HandleAsync(
            IDomainEvent<ServiceMonitorAggregate, ServiceMonitorAggregateId, AddedServiceMethodEntryEvent> domainEvent, 
            CancellationToken cancellationToken)
        {

            var data = _jsonSerializer.Serialize(new 
            { 
                ExecutionTime = domainEvent.AggregateEvent.ServiceMethod.ExecutionTime,
                TimeElapsed = domainEvent.AggregateEvent.ServiceMethod.TimeElapsed.TotalSeconds
            });
            return _hubContext.Clients.All.SendAsync("SendMethodLog", data);
        }
    }
}
