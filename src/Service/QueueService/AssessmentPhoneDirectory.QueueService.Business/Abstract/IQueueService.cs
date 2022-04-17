using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.QueueService.Business.Abstract
{
   public interface IQueueService
    {
        public IConnection GetConnection();
        public IModel GetChannel();
        public void WriteToQueue(string queueName,string queueMessage,object model);
    }
}
