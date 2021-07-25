using Microservice.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects
{
    [ValueObjectResourcePath("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ValueObjects.Mappings.AddressType.xml")]
    public class ExecutionsStatusType : XmlValueObject
    {
    }

    public class ExecutionsStatusTypes : XmlValueObjectLookup<ExecutionsStatusType, ExecutionsStatusTypes>
    {
        public ExecutionsStatusType ExecutedSuccessfully { get { return FindValueObject("Ex_Suc"); } }

        public ExecutionsStatusType FailedExecution { get { return FindValueObject("Ex_Fal"); } }

        public ExecutionsStatusType NoExecution { get { return FindValueObject("Ex_Non"); } }
    }
}
