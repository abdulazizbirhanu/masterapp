using EventBus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TIntegrationType> : IIntegrationEventHandler
        where TIntegrationType: IntegrationEvent
    {
        Task Handle(IntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }


}
