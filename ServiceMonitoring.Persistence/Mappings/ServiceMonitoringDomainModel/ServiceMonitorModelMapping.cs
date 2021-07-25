using Microsoft.EntityFrameworkCore;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entity;
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
            .Entity<ServiceMonitorAggregate>()
            .Property(o => o.Id)
            .HasConversion(new SingleValueObjectIdentityValueConverter<ServiceMonitorAggregateId>());

            modelBuilder.Entity<ServiceMonitorAggregate>()
                .HasMany<ServiceMethod>();

            return modelBuilder;
        }
    }

}
