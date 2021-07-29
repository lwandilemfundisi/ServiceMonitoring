using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.Hubs
{
    public interface ISendMethodLogsHub
    {
        Task SendMethodLog(CancellationToken cancellationToken);
    }
}
