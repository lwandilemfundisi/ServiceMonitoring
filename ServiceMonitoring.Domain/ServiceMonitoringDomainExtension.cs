using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain
{
    public static class ServiceMonitoringDomainExtension
    {
        public static Assembly Assembly { get; } = typeof(ServiceMonitoringDomainExtension).Assembly;

        public static IDomainContainer ConfigureServiceMonitoringDomain(
            this IDomainContainer domainContainer,
            IConfiguration configuration = null)
        {
            return domainContainer
                .AddDefaults(Assembly);
        }
    }
}
