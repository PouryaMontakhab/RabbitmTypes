using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class SendMessage
    {
        string Username = "guest";
        string Password = "guest";
        string Hostname = "localhost";

        public void SendDirectMessage(string message)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                UserName = Username,
                Password = Password,
                HostName = Hostname
            };
            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            DirectExchange(message, model);
            Console.WriteLine("direct message sent");
        }
        public void SendTopicMessage(string message)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                UserName = Username,
                Password = Password,
                HostName = Hostname
            };
            using (var connection = connectionFactory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                TopicExchange(message, model);
                Console.WriteLine("topic message sent");
            }
        }
        public void SendFanoutMessage(string message)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                UserName = Username,
                Password = Password,
                HostName = Hostname
            };
            using (var connection = connectionFactory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                FanoutExchange(message, model);
                Console.WriteLine("topic message sent");
            }
        }
        private void DirectExchange(string message, IModel model)
        {

            model.ExchangeDeclare("demoExchange", ExchangeType.Direct, false, false);
            model.QueueDeclare("demoQueue", false, false, false, null);
            model.QueueBind("demoQueue", "demoExchange", "directExchange_key");
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messageBuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("demoExchange", "directExchange_key", properties, messageBuffer);
        }
        private void TopicExchange(string message, IModel model)
        {
            model.ExchangeDeclare("topicExchange", ExchangeType.Topic,false,true);
            model.QueueDeclare("topic.iran.queue", false, true, false, null);
            model.QueueDeclare("topic.tehran.queue", false, true, false, null);
            model.QueueBind("topic.iran.queue", "topicExchange", "*.iran.*");
            model.QueueBind("topic.tehran.queue", "topicExchange", "tehran.#");
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messageBuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("topicExchange", "tasdlfj.iran.jsodf", properties, messageBuffer);
        }
        private void FanoutExchange(string message, IModel model)
        {
            model.ExchangeDeclare("fanoutExchange", ExchangeType.Fanout, false, true);
            model.QueueDeclare("firstQueue", false, true, false, null);
            model.QueueDeclare("secondQueue", false, true, false, null);
            model.QueueBind("firstQueue", "fanoutExchange",String.Empty);
            model.QueueBind("secondQueue", "fanoutExchange", String.Empty);
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messageBuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("fanoutExchange",String.Empty, properties, messageBuffer);
        }
    }
}

