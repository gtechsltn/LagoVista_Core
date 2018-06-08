﻿using LagoVista.Core.Networking.Rpc.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LagoVista.Core.Networking.Rpc.Tests.AsyncRequestBrokerTests
{
    [TestClass]
    public class InstanceMethodPairTests
    {
        private static readonly string _orgId = "orgid";
        private static readonly string _insId = "insid";
        private static readonly string _replyPath = "replyPath";


        #region synchronous method simple params
        [TestMethod]
        public void InstanceMethodPair_Constructor_SynchronousMethodAndSimpleMethodParams()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            Assert.IsNotNull(pair);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_SynchronousMethodAndSimpleMethodParams_ReturnsNonNullResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_SynchronousMethodAndSimpleMethodParams_ReturnsSuccessfulResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_SynchronousMethodAndSimpleMethodParams_ReturnsCorrectResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.AreEqual(request.CorrelationId, response.CorrelationId);
            Assert.AreEqual(request.Id, response.RequestId);
            Assert.AreEqual(ProxySubject.EchoValueConst, response.ReturnValue);
            Assert.AreEqual(ProxySubject.EchoValueConst, response.GetTypedReturnValue<string>());
        }
        #endregion

        #region asynchronous method simple params
        [TestMethod]
        public void InstanceMethodPair_Constructor_AsynchronousMethodAndSimpleMethodParams()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.EchoAsync));
            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            Assert.IsNotNull(pair);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_AsynchronousMethodAndSimpleMethodParams_ReturnsNonNullResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.EchoAsync));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_AsynchronousMethodAndSimpleMethodParams_ReturnsSuccessfulResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.EchoAsync));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task InstanceMethodPair_Invoke_AsynchronousMethodAndSimpleMethodParams_ReturnsCorrectResponse()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.EchoAsync));

            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var response = await pair.Invoke(request);
            Assert.AreEqual(request.CorrelationId, response.CorrelationId);
            Assert.AreEqual(request.Id, response.RequestId);
            Assert.AreEqual(ProxySubject.EchoValueConst, response.ReturnValue);
            Assert.AreEqual(ProxySubject.EchoValueConst, response.GetTypedReturnValue<string>());
        }
        #endregion

        #region invoke null param check
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task InstanceMethodPair_Invoke_SynchronousMethodAndSimpleMethodParams_ThrowsArgumentNullException()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            IAsyncRequest request = null;

            var response = await pair.Invoke(request);
        }
        #endregion

        #region arguments validation
        [TestMethod]
        public void InstanceMethodPair_GetArguments_Succeeds_ReturnsNonNullArguments()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var parameters = methodInfo.GetParameters();
            var request = new AsyncRequest(methodInfo, new object[] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            Assert.AreEqual(1, request.ArgumentCount);
            Assert.AreEqual(1, parameters.Length);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
            Assert.IsNotNull(arguments);
        }

        [TestMethod]
        public void InstanceMethodPair_GetArguments_Succeeds_ReturnsCorrectArguments()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var parameters = methodInfo.GetParameters();
            var requestArguments = new object[] { ProxySubject.EchoValueConst };
            var request = new AsyncRequest(methodInfo, requestArguments, _orgId, _insId, _replyPath);

            Assert.AreEqual(1, request.ArgumentCount);
            Assert.AreEqual(1, parameters.Length);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);

            Assert.AreEqual(parameters.Length, arguments.Length);
            Assert.AreEqual(request.ArgumentCount, arguments.Length);
            Assert.IsTrue(requestArguments.SequenceEqual(arguments));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InstanceMethodPair_GetArguments_Fails_DueToCountMismatch()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var parameters = methodInfo.GetParameters();
            var request = new AsyncRequest(methodInfo, new object[] { ProxySubject.EchoValueConst, new object(), null }, _orgId, _insId, _replyPath);

            Assert.AreEqual(3, request.ArgumentCount);
            Assert.AreEqual(1, parameters.Length);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InstanceMethodPair_GetArguments_Fails_DueToTypeMismatch()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var parameters = methodInfo.GetParameters();
            var request = new AsyncRequest(methodInfo, new object[] { 3 }, _orgId, _insId, _replyPath);

            Assert.AreEqual(1, request.ArgumentCount);
            Assert.AreEqual(1, parameters.Length);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstanceMethodPair_GetArguments_Fails_DueToUnsupportedParamsKeyword()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.PassStringParams));
            var parameters = methodInfo.GetParameters();
            var request = new AsyncRequest(methodInfo, new object[] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            Assert.AreEqual(1, request.ArgumentCount);
            Assert.AreEqual(1, parameters.Length);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstanceMethodPair_GetArguments_NullRequest_ThrowsArgumentNullException()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            var parameters = methodInfo.GetParameters();
            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            IAsyncRequest request = null;

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstanceMethodPair_GetArguments_NullParameters_ThrowsArgumentNullException()
        {
            var methodInfo = typeof(ProxySubject).GetMethod(nameof(ProxySubject.Echo));
            ParameterInfo[] parameters = null;
            var subject = (IProxySubject)new ProxySubject();
            var pair = new InstanceMethodPair(subject, methodInfo);
            var request = new AsyncRequest(methodInfo, new object[1] { ProxySubject.EchoValueConst }, _orgId, _insId, _replyPath);

            var arguments = InstanceMethodPair.GetArguments(request, parameters);
        }

        #endregion


    }
}
