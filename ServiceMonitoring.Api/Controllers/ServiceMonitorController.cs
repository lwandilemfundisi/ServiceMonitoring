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
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects;
using Microservice.Framework.Common;

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
        public async Task<IActionResult> QueryService(GetServiceMonitorRequestModel model)
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

        [HttpPost("addservicemethodlog")]
        public async Task<IActionResult> AddServiceMethodLog(AddServiceMethodLogRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var addServiceMethodLogResult = await _commandBus.PublishAsync(new AddServiceMethodEntryCommand(
                        new ServiceMonitorAggregateId(model.ServiceId),
                        new ServiceMethod
                        {
                            Id = ServiceMethodId.New,
                            Name = model.MethodName,
                            Request = model.RequestUri,
                            Response = model.Response,
                            ExecutionTime = model.MethodExecutionTime,
                            TimeElapsed = model.ElapsedTime,
                            ExecutionsStatus = XmlValueObjectLookup.Repository.Find<ExecutionsStatusType>(model.ExecutionsStatus),
                            ExecutedBy = model.ExecutedBy
                        }), CancellationToken.None);

                if (addServiceMethodLogResult.IsSuccess)
                    return Ok(addServiceMethodLogResult);
                else
                    return BadRequest(addServiceMethodLogResult);
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }
    }
}
