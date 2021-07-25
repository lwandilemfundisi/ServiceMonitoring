using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Exceptions;
using Microservice.Framework.Domain.Extensions;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Events;
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

        #region Properties

        public string ServiceName { get; set; }

        public IList<ServiceMethod> ServiceMethods { get; set; }

        public void CreateServiceMonitor(string serviceName)
        {
            Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
            ServiceName = serviceName;
            Emit(new CreatedServiceMonitorEvent(serviceName));
        }

        public void AddServiceMethodEntry(ServiceMethod serviceMethod)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

            if(ServiceMethods.IsNull())
            {
                ServiceMethods = new List<ServiceMethod>();
            }

            ServiceMethods.Add(serviceMethod);
            Emit(new AddedServiceMethodEntryEvent(serviceMethod));
        }

        #endregion
    }
}
