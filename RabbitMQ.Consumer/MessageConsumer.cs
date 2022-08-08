using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    public class MessageConsumer:DefaultBasicConsumer
    {
        private readonly IModel _channel;
        public MessageConsumer(IModel channel)
        {
            _channel = channel;
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine("Consuming message");
            Console.WriteLine(String.Concat("Message was received from exchange : ", exchange));
            Console.WriteLine(String.Concat("Consumer tag : ", consumerTag));
            Console.WriteLine(String.Concat("Delivery tag :", deliveryTag));
            Console.WriteLine(String.Concat("Routing key", routingKey));
            Console.WriteLine(String.Concat("Message :", Encoding.UTF8.GetString(body.ToArray())));
            _channel.BasicAck(deliveryTag, false);
        }
    }
}

