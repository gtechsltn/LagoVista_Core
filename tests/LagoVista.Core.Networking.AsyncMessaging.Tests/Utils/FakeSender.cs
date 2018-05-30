﻿using LagoVista.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace LagoVista.Core.Networking.AsyncMessaging.Tests.Utils
{
    /// <summary>
    /// fake sender "sends" a request, then waits a bit to simulate network and then completes the request with the coupler
    /// </summary>
    internal sealed class FakeSender : IAsyncRequestHandler
    {
        private IAsyncCoupler<IAsyncResponse> _asyncCoupler;

        public FakeSender(IAsyncCoupler<IAsyncResponse> asyncCoupler, object result)
        {
            _asyncCoupler = asyncCoupler;
            Result = result;
        }

        public Task HandleRequest(IAsyncRequest request)
        {
            Request = request;
            CompleteRequest(TimeSpan.FromSeconds(1));
            return Task.FromResult<object>(null);
        }

        private void CompleteRequest(TimeSpan delay)
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(delay);
                var response = new AsyncResponse(Request, Result);
                await _asyncCoupler.CompleteAsync(response.CorrelationId, response);
            });
        }

        public object Result { get; private set; }
        public IAsyncRequest Request { get; private set; }
    }

}