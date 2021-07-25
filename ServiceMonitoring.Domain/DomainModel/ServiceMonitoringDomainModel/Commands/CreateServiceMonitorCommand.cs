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
    public class CreateServiceMonitorCommand 
        : Command<ServiceMonitorAggregate, ServiceMonitorAggregateId>
    {
        public CreateServiceMonitorCommand(
            ServiceMonitorAggregateId aggregateId,
            string serviceName)
            : base(aggregateId)
        {
            ServiceName = serviceName;
        }

        public string ServiceName { get; }
    }

    public class CreateServiceMonitorCommandHandler 
        : CommandHandler<ServiceMonitorAggregate, ServiceMonitorAggregateId, CreateServiceMonitorCommand>
    {
        public override async Task<IExecutionResult> ExecuteAsync(
            ServiceMonitorAggregate aggregate, 
            CreateServiceMonitorCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.CreateServiceMonitor(command.ServiceName);

            return ExecutionResult.Success();
        }
    }
}
