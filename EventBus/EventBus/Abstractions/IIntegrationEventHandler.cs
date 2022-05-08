﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent:IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
    public interface IIntegrationEventHandler
    {
    }

}
