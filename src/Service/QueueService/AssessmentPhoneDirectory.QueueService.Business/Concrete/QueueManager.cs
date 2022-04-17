using AssessmentPhoneDirectory.QueueService.Business.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.QueueService.Business.Concrete
{
    public class QueueManager : IQueueService
    {
        IConnection _connection;
        IModel _channel;
        public QueueManager()
        {
            _connection = GetConnection();
            _channel = GetChannel();
        }
        public IModel GetChannel()
        {
            return _connection.CreateModel();
        }

        public IConnection GetConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:15672")
            };

            return connectionFactory.CreateConnection();
        }

        public void WriteToQueue(string queueName,string queueMessage, object model)
        {
            var messageArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            _channel.BasicPublish(queueMessage, queueName, null, messageArr);
        }
    }
}
