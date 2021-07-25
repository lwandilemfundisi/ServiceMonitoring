using Microservice.Framework.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel
{
    public class ServiceMonitorAggregate : AggregateRoot<ServiceMonitorAggregate, ServiceMonitorAggregateId>
    {
        #region Constructors

        public ServiceMonitorAggregate()
            : this(null)
        {

        }

        public ServiceMonitorAggregate(ServiceMonitorAggregateId aggregateId)
            : base(aggregateId)
        {

        }

        #endregion
    }
}
