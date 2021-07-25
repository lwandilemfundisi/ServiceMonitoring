using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Commands
{
    public class AddServiceMethodEntryCommand : Command<ServiceMonitorAggregate, ServiceMonitorAggregateId>
    {
        public AddServiceMethodEntryCommand(
            ServiceMonitorAggregateId aggregateId,
            ServiceMethod serviceMethod)
            : base(aggregateId)
        {
            ServiceMethod = serviceMethod;
        }

        public ServiceMethod ServiceMethod { get; }
    }

    public class AddServiceMethodEntryCommandHandler
        : CommandHandler<ServiceMonitorAggregate, ServiceMonitorAggregateId, AddServiceMethodEntryCommand>
    {
        public override Task<IExecutionResult> ExecuteAsync(
            ServiceMonitorAggregate aggregate,
            AddServiceMethodEntryCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.AddServiceMethodEntry(command.ServiceMethod);
            return Task.FromResult(ExecutionResult.Success());
        }
    }
}
