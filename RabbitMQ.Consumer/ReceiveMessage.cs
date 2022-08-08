using System;
using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    public class ReceiveMessage
    {
        string Username = "guest";
        string Password = "guest";
        string Hostname = "localhost";

        public void Receive(string queueName)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                UserName = Username,
                Password = Password,
                HostName = Hostname
            };
            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.BasicQos(0, 1, false);
            var messageConsumer = new MessageConsumer(model);
            model.BasicConsume(queueName, false, messageConsumer);
            Console.Read();
        }
    }
}

