using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using ServiceMonitoring.Api.Models;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Commands;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Queries;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ServiceMonitoring.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceMonitorController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ServiceMonitorController(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor
            )
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpPost("addservice")]
        public async Task<IActionResult> AddService(CreateServiceMonitorRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var createServiceMonitorResult = await _commandBus
                    .PublishAsync(
                        new CreateServiceMonitorCommand(ServiceMonitorAggregateId.New, model.ServiceName),
                        CancellationToken.None);

                if (createServiceMonitorResult.IsSuccess)
                    return Ok(createServiceMonitorResult);
                else
                    return BadRequest(createServiceMonitorResult);
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }

        [HttpPost("queryservice")]
        public async Task<IActionResult> AddService(GetServiceMonitorRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _queryProcessor
                    .ProcessAsync(new QueryServiceMonitor(
                        new ServiceMonitorAggregateId(model.Id)),
                        CancellationToken.None));
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }
    }
}
