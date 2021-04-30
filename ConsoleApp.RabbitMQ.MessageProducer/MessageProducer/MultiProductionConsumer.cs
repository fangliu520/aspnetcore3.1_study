using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageProducer.MessageProducer
{
    public class MultiProductionConsumer
    {
        public static void Show(string no)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (IConnection connection=factory.CreateConnection())
            {
                using (IModel channel=connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MultiProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "MultiProducerMessageExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "MultiProducerMessage", exchange: "MultiProducerMessageExchange", routingKey: string.Empty, arguments: null);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"生产者{no}已准备就绪~~~");
                    int i = 1;
                    while (true) {
                        string message = $"生产者{no},消息：{i}";
                        byte[] body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(
                            exchange: "MultiProducerMessageExchange",
                            routingKey:string.Empty,
                            basicProperties:null,
                            body:body
                            );
                        Console.WriteLine($"消息：{message} 已发送~");
                        i++;
                        Thread.Sleep(200);
                    }
                }

            }


        }
    }
}
