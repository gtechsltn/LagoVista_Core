﻿using LagoVista.Core.PlatformSupport;
using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;

namespace LagoVista.Core.Networking.AsyncMessaging
{
    /// <summary>
    /// "handles" requests by sending them to service bus. They get picked up on "the other side" by 
    /// ServiceBusAsyncRequestModerator which routes requests and returns responses via service bus.
    /// This class lives where ever NuvIoT core management rest api is hosted.
    /// </summary>
    public class ServiceBusAsyncRequestSender : IAsyncRequestHandler
    {
        private readonly ILogger _logger;
        private readonly string _topicConnectionString;
        private readonly string _destinationEntityPath;
        private readonly IServiceBusAsyncRequestSenderConnectionSettings _settings;

        public ServiceBusAsyncRequestSender(IServiceBusAsyncRequestSenderConnectionSettings settings, ILogger logger)
        {
            Console.WriteLine("ServiceBusAsyncRequestSender::ctor >>");

            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Endpoint - AccountId
            // SharedAccessKeyName - UserName
            // SharedAccessKey - AccessKey
            // DestinationEntityPath - ResourceName
            _topicConnectionString = $"Endpoint=sb://{settings.ServiceBusAsyncRequestSender.AccountId}.servicebus.windows.net/;SharedAccessKeyName={settings.ServiceBusAsyncRequestSender.UserName};SharedAccessKey={settings.ServiceBusAsyncRequestSender.AccessKey};";
            _destinationEntityPath = settings.ServiceBusAsyncRequestSender.ResourceName;

            Console.WriteLine("ServiceBusAsyncRequestSender::ctor <<");
        }

        //[JsonObject("topicInstructions")]
        //internal sealed class TopicInstructions
        //{
        //    [JsonProperty("organizationId"), JsonRequired]
        //    public string OrganizationId { get; set; }

        //    //[JsonProperty("instanceKey"), JsonRequired]
        //    //public string InstanceKey { get; set; }

        //    [JsonProperty("responsePath"), JsonRequired]
        //    public string ResponsePath { get; set; }

        //    [JsonProperty("instanceId"), JsonRequired]
        //    public string InstanceId { get; set; }

        //    public static implicit operator TopicInstructions(string jsonValue)
        //    {
        //        return string.IsNullOrEmpty(jsonValue) ? null : JsonConvert.DeserializeObject<TopicInstructions>(jsonValue);
        //    }

        //    public static implicit operator string (TopicInstructions instructions)
        //    {
        //        if(instructions == null) throw new ArgumentNullException(nameof(instructions));

        //        //return $"_{instructions.OrganizationKey}_{instructions.InstanceKey}_{instructions.InstanceId}";
        //        return $"_{instructions.OrganizationId}_{instructions.InstanceId}";
        //    }
        //}

        public async Task HandleRequest(IAsyncRequest request)
        {
            Console.WriteLine("sending request");
            if (request == null) throw new ArgumentNullException(nameof(request));

            //todo: ML - if service bus topic and subscription doesn't exist, then create it: https://github.com/Azure-Samples/service-bus-dotnet-management/tree/master/src/service-bus-dotnet-management

            //todo: ML - need to set retry policy and operation timeout etc.
            var topicClient = new TopicClient(_topicConnectionString, _destinationEntityPath + $"_{request.OrganizationId}_{request.InstanceId}", null);
            try
            {
                // package response in service bus message and send to topic
                var messageOut = new Message(request.Payload)
                {
                    MessageId = request.Id,
                    To = request.DestinationPath,
                    CorrelationId = request.CorrelationId,
                    ContentType = request.GetType().FullName,
                    Label = request.DestinationPath,
                    ReplyTo = request.ReplyPath,
                };
                await topicClient.SendAsync(messageOut);
            }
            catch (Exception ex)
            {
                //todo: ML - log exception
                throw;
            }
            finally
            {
                await topicClient.CloseAsync();
            }
        }
    }
}
