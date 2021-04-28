using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageProducer.MessageProducer
{
    public class SeckillConsumer
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "SeckillProduct", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "SeckillProductExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "SeckillProduct", exchange: "SeckillProductExchange", routingKey: string.Empty, arguments: null);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("开始秒杀~~~~");

                    for (int i = 0; i < 1000; i++)
                    {
                        string message = $"第{i}个用户进入 秒杀商品";
                        byte[] body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "SeckillProductExchange",
                            routingKey: string.Empty,
                            basicProperties: null,
                            body: body);
                        Console.WriteLine(message);
                    }
                }
            }
        }


    }
}
