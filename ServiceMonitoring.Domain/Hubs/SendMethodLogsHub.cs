using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.Hubs
{
    public class SendMethodLogsHub : Hub<ISendMethodLogsHub>
    {
        public Task SendMethodLog(object data, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
