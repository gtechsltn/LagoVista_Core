﻿using LagoVista.Core.Networking.AsyncMessaging.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace LagoVista.Core.Networking.AsyncMessaging.Tests.AsyncRequestBroker
{
    [TestClass]
    public class AsyncRequestBrokerTests
    {
        private readonly IProxySubject _instance = new ProxySubject();
        private readonly MethodInfo _methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
        private readonly MethodInfo _asyncMethodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.EchoAsync));
        private InstanceMethodPair _pair = null;
        private InstanceMethodPair _asyncPair = null;

        [TestInitialize]
        public void Init()
        {
            Assert.IsNotNull(_methodInfo);
            Assert.IsNotNull(_asyncMethodInfo);

            _pair = new InstanceMethodPair(_instance, _methodInfo);
            _asyncPair = new InstanceMethodPair(_instance, _asyncMethodInfo);
        }

        [TestMethod]
        public async Task TestInstanceMethodPairInvokeSuccess()
        {
            IAsyncRequest request = new AsyncRequest()
            {
                Id = "id",
                CorrelationId = "correlationId",
                Path = $"{_methodInfo.DeclaringType.FullName}.{_methodInfo.Name}",
                TimeStamp = DateTime.UtcNow
            };
            request.SetValue("value", ProxySubject.EchoValueConst);
            Assert.AreEqual(1, request.ArgumentCount);

            var response = await _pair.Invoke(request);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.ReturnValue);
            Assert.AreEqual(ProxySubject.EchoValueConst, response.ReturnValue);
            Assert.AreNotEqual(request.Id, response.Id);
            Assert.AreEqual(request.CorrelationId, response.CorrelationId);
            Assert.AreEqual(request.Path, response.Path);
            Assert.AreNotEqual(request.TimeStamp, response.TimeStamp);
        }

        [TestMethod]
        public async Task TestInstanceMethodPairInvokeFailure()
        {
            IAsyncRequest request = new AsyncRequest()
            {
                Id = "id",
                CorrelationId = "correlationId",
                Path = $"{_methodInfo.DeclaringType.FullName}.{_methodInfo.Name}",
                TimeStamp = DateTime.UtcNow
            };
            request.SetValue("value", ProxySubject.EchoValueConst);
            request.SetValue("bunk", ProxySubject.EchoValueConst);
            Assert.AreEqual(2, request.ArgumentCount);

            var response = await _pair.Invoke(request);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ArgumentException), response.Exception.GetType());
            Assert.AreNotEqual(request.Id, response.Id);
            Assert.AreEqual(request.CorrelationId, response.CorrelationId);
            Assert.AreEqual(request.Path, response.Path);
            Assert.AreNotEqual(request.TimeStamp, response.TimeStamp);
        }
    }
}