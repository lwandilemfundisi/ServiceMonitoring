using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;
using Microservice.Framework.Persistence.EFCore.Queries.Filtering;
using Microservice.Framework.Persistence.Queries.Filtering;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Queries
{
    public class QueryServiceMonitor : EFCoreCriteriaDomainQuery<ServiceMonitorAggregate>, IQuery<IEnumerable<SeriesModel>>
    {
        public QueryServiceMonitor(ServiceMonitorAggregateId id)
        {
            Id = id;
        }

        protected override void OnBuildDomainCriteria(EFCoreDomainCriteria domainCriteria)
        {
            domainCriteria.Include = new Include("ServiceMethods");
        }
    }

    public class QueryServiceMonitorHandler : EFCoreCriteriaDomainQueryHandler<ServiceMonitorAggregate>, IQueryHandler<QueryServiceMonitor, IEnumerable<SeriesModel>>
    {
        public QueryServiceMonitorHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {
        }

        public async Task<IEnumerable<SeriesModel>> ExecuteQueryAsync(QueryServiceMonitor query, CancellationToken cancellationToken)
        {
            var service = await Find(query);
            var byName = service.ServiceMethods.OrderBy(sm => sm.ExecutionTime).GroupBy(m => m.Name);
            return byName.Select(g => new SeriesModel { Name = g.Key, Data = g.ToList().Select(m => new KeyValuePair<DateTime, double>(m.ExecutionTime, m.TimeElapsed.TotalSeconds)) });
        }
    }

    public class SeriesModel
    {
        public string Name { get; set; }

        public IEnumerable<KeyValuePair<DateTime, double>> Data { get; set; }
    }
}
