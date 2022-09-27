using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;


namespace EasyNetMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MQService.GetConn())
            {
                using (var channel = MQService.GetConn().CreateModel())
                {
                    channel.QueueDeclare("hello", true, false, false, null); //创建一个名称为hello的消息队列
                    string message = string.Empty;
                    do
                    {
                        Console.Write("input word,empty for exit==>");
                        message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("", "hello", true,null, body); //开始传递
                        Console.WriteLine("Send： {0}", message);
                    } while (!string.IsNullOrWhiteSpace(message));

                    Console.ReadLine();
                }
            }
            
        }
    }
}