using Microsoft.EntityFrameworkCore;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entity;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects;
using ServiceMonitoring.Persistence.ValueObjectConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Persistence.Mappings.ServiceMonitoringDomainModel
{
    public static class ServiceMonitorModelMapping
    {
        public static ModelBuilder ServiceMonitorModelMap(this ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<ServiceMethod>()
            .Property(o => o.Id)
            .HasConversion(new SingleValueObjectIdentityValueConverter<ServiceMethodId>());

            modelBuilder
            .Entity<ServiceMethod>()
            .Property(o => o.ExecutionsStatus)
            .HasConversion(new ValueObjectValueConverter<ExecutionsStatusType, ExecutionsStatusTypes>());

            modelBuilder
            .Entity<ServiceMonitorAggregate>()
            .Property(o => o.Id)
            .HasConversion(new SingleValueObjectIdentityValueConverter<ServiceMonitorAggregateId>());

            modelBuilder
                .Entity<ServiceMethod>()
                .HasOne<ServiceMonitorAggregate>()
                .WithMany(c => c.ServiceMethods);

            return modelBuilder;
        }
    }

}
