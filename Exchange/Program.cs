using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using RabbitMQ.Client;

namespace Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchange = MQService.GetConn().CreateModel();
            exchange.ExchangeDeclare("Exchange.TMS", ExchangeType.Direct, true, true, null);
            string msg = string.Empty;
            var prop = exchange.CreateBasicProperties();
            prop.Persistent = true;
            do
            {
                Console.Write("Input msg,enter to send,empty for exit ===>");
                msg = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    exchange.BasicPublish("Exchange.TMS","WORK",prop,new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(msg)));
                }

            } while (!string.IsNullOrWhiteSpace(msg));

            Console.WriteLine("exit...");
            Console.Read();
        }
    }
}
