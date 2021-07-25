using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Rules.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Domain.DomainModel
{
    public static class Specs
    {
        public static ISpecification<IAggregateRoot> AggregateIsNew { get; } = new AggregateIsNewSpecification();
        public static ISpecification<IAggregateRoot> AggregateIsCreated { get; } = new AggregateIsCreatedSpecification();

        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
        {
            protected override Notification IsNotSatisfiedBecause(IAggregateRoot obj)
            {
                if (obj.IsNew)
                {
                    return Notification.Create(new Message 
                    {
                        Text = $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new",
                        Severity = SeverityType.Critical
                    });
                }

                return Notification.CreateEmpty();
            }
        }
    }
}
