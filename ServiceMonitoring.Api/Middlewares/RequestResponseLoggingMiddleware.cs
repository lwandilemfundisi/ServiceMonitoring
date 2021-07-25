﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microservice.Framework.Persistence;
using System.Net.Http;
using System.Diagnostics;
using Microservice.Framework.Domain.Commands;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Commands;
using System.Threading;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities;
using ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects;

namespace ServiceMonitoring.Api.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var stopwatch = Stopwatch.StartNew();
            string request = await FormatRequest(context.Request);
            try
            {
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);
                    var elapsed = stopwatch.Elapsed;
                    var response = await FormatResponse(context.Response);
                    var bus = serviceProvider.GetService<ICommandBus>();
                    var result = await bus.PublishAsync(new AddServiceMethodEntryCommand(
                        new ServiceMonitorAggregateId("servicemonitoraggregate-699da1fd-2350-411c-aec1-ec6f65ac8c93"),
                        new ServiceMethod
                        {
                            Id = ServiceMethodId.New,
                            Request = request,
                            Response = response,
                            ExecutionTime = DateTime.Now,
                            TimeElapsed = stopwatch.Elapsed
                        }), CancellationToken.None);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception e)
            {
                var bus = serviceProvider.GetService<ICommandBus>();
                var result = await bus.PublishAsync(new AddServiceMethodEntryCommand(
                    new ServiceMonitorAggregateId("servicemonitoraggregate-699da1fd-2350-411c-aec1-ec6f65ac8c93"),
                    new ServiceMethod
                    {
                        Id = ServiceMethodId.New,
                        Request = request,
                        Response = e.ToString(),
                        ExecutionTime = DateTime.Now,
                        TimeElapsed = stopwatch.Elapsed,
                        ExecutionsStatus = ExecutionsStatusTypes.Of().FailedExecution
                    }), CancellationToken.None);

                throw;
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}
