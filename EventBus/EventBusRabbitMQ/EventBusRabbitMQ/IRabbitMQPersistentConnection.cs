﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
    {
         bool IsConnected { get; }

         bool TryConnect();

         IModel CreateModel();
    }
}
