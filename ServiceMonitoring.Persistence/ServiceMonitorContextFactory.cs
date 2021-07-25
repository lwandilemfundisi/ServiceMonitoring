using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Persistence
{
    public class ServiceMonitorContextFactory : IDesignTimeDbContextFactory<ServiceMonitorContext>
    {
        public ServiceMonitorContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ServiceMonitorContext>();
            optionsBuilder.UseSqlServer(configuration["DataConnection:Database"]);

            return new ServiceMonitorContext(optionsBuilder.Options);
        }
    }
}
