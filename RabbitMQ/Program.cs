using RabbitMQ;
using RabbitMQ.Client;

var messageSender = new SendMessage();
messageSender.SendTopicMessage("ostadbash.com");

Console.Read();