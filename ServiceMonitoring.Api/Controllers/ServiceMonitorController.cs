using Microservice.Framework.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using ServiceMonitoring.Api.Models;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceMonitorController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public ServiceMonitorController(
            ICommandBus commandBus
            )
        {
            _commandBus = commandBus;
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
    }
}
