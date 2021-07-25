using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Persistence
{
    public class ServiceMonitorContextProvider : IDbContextProvider<ServiceMonitorContext>, IDisposable
    {
        private readonly DbContextOptions<ServiceMonitorContext> _options;

        public ServiceMonitorContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ServiceMonitorContext>()
                .UseSqlServer(configuration["DataConnection:Database"])
                .Options;
        }

        public ServiceMonitorContext CreateContext()
        {
            return new ServiceMonitorContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
