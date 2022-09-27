using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ClassLibrary1
{

    public sealed class MQService
    {
        private static IConnection conn;
        private static object _lock=new object();
        private static ConnectionFactory factory;

        static MQService()
        {
            factory = new ConnectionFactory();
            factory.HostName = "10.63.1.169";
            factory.Port = 49158;
            factory.UserName = "tms";
            factory.Password = "tms";
        }

        public static IConnection GetConn()
        {
            if (conn == null)
            {
                lock (_lock)
                {
                    if (conn == null)
                    {
                        conn = factory.CreateConnection();
                    }
                }
            }

            return conn;
        }
    }

    public class MyMsg
    {
        public MyMsg(string m)
        {
            Msg = m;
        }

        public string Msg { get; set; }
    }
}
