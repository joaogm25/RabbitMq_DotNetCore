using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Send();
            
        }

        static void Send()
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while (true)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        string message = i.ToString();

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    break;
                }
                
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
