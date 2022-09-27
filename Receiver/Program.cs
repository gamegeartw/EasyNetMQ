using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
    class Program
    {
        static IModel channel;

        static void Main(string[] args)
        {
            using (MQService.GetConn())
            {
                using (channel = MQService.GetConn().CreateModel())
                {
                    Console.WriteLine("Connect ok,wait for message...");
                    channel.QueueDeclare("hello", true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("hello", false, consumer);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine("已接收： {0}", message);
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    Console.ReadLine();
                }
            }
        }
    }
}