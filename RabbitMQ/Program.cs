using RabbitMQ;
using RabbitMQ.Client;

var messageSender = new SendMessage();
messageSender.SendFanoutMessage("ostadbash.com");

Console.Read();