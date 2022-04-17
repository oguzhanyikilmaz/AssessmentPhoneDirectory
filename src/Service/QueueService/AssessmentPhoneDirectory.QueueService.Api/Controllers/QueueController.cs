using AssessmentPhoneDirectory.QueueService.Api.Models;
using AssessmentPhoneDirectory.QueueService.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.QueueService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;
        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public async Task<bool> SendQueue([FromBody] List<ListContactQueryResponse> requestModel)
        {
            bool retVal = true;

            var channel = _queueService.GetChannel();

            channel.ExchangeDeclare("reportCreateExchange", "direct");

            channel.QueueDeclare("createReport", false, false, false);
            channel.QueueBind("createReport", "reportCreateExchange", "createReport");

            channel.QueueDeclare("reportCreated", false, false, false);
            channel.QueueBind("reportCreated", "reportCreateExchange", "reportCreated");

            _queueService.WriteToQueue("createReport", "createReport", requestModel);

            var consumerEvent = new EventingBasicConsumer(channel);

            consumerEvent.Received += (ch, ea) =>
            {
                var modelReceived = JsonConvert.DeserializeObject<List<ListContactQueryResponse>>(Encoding.UTF8.GetString(ea.Body.ToArray()));
            };

            channel.BasicConsume("reportCreated", true, consumerEvent);

            return retVal;
        }

        [HttpGet("ConsumeQueue")]
        public async Task<bool> ConsumeQueue()
        {
            bool retVal = false;

            var channel = _queueService.GetChannel();

            channel.ExchangeDeclare("reportCreateExchange", "direct");

            channel.QueueDeclare("createReport", false, false, false);
            channel.QueueBind("createReport", "reportCreateExchange", "createReport");

            channel.QueueDeclare("reportCreated", false, false, false);
            channel.QueueBind("reportCreated", "reportCreateExchange", "reportCreated");

            var consumerEvent = new EventingBasicConsumer(channel);

            consumerEvent.Received += (ch, ea) =>
            {
                var modelJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var model = JsonConvert.DeserializeObject<List<ListContactQueryResponse>>(modelJson);

                retVal = RefitApiServiceDependency.JobApi.CreateReport(model).Result;

                _queueService.WriteToQueue("reportCreated", "createReport", model);
            };

            channel.BasicConsume("createReport", true, consumerEvent);

            return retVal;
        }
    }
}
