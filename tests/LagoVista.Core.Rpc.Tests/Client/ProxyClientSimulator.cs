﻿using LagoVista.Core.Interfaces;
using LagoVista.Core.PlatformSupport;
using LagoVista.Core.Rpc.Client;
using LagoVista.Core.Rpc.Messages;
using LagoVista.Core.Rpc.Settings;
using LagoVista.Core.Rpc.Tests.Middelware;
using System;
using System.Threading.Tasks;

namespace LagoVista.Core.Rpc.Tests.Client
{
    public sealed class ProxyClientSimulator : AbstractProxyClient
    {
        private readonly QueueSimulator _queue;

        public ProxyClientSimulator(
            ITransceiverConnectionSettings connectionSettings, 
            IAsyncCoupler<IMessage> asyncCoupler, 
            ILogger logger, 
            QueueSimulator queue) : base(connectionSettings, asyncCoupler, logger)
        {
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        protected override void CustomStart()
        {
            _queue.RegisterListener(this, QueueSimulator.ListenerType.Response);
        }

        protected override async Task CustomTransmitMessageAsync(IMessage message)
        {
            await _queue.Send((IRequest)message);
        }
    }
}
