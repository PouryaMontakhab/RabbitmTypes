using RabbitMQ.Consumer;

var messageReceiver = new ReceiveMessage();
//demoQueue for direct message
//topic.iran.queue and topic.tehran.queue for topic messaeg
messageReceiver.Receive("topic.iran.queue");

Console.ReadKey();
