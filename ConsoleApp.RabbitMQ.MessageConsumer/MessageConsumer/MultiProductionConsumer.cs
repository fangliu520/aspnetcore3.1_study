using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageConsumer.MessageConsumer
{
    public class MultiProductionConsumer
    {
        public static void Show01()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MultiProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "MultiProducerMessageExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "MultiProducerMessage", exchange: "MultiProducerMessageExchange", routingKey: string.Empty, arguments: null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body.ToArray());
                            Console.WriteLine($"消费者01 接受消息: {message}");
                        };
                        channel.BasicConsume(
                            queue: "MultiProducerMessage",
                            autoAck: true,
                            consumer: consumer);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }

        public static void Show02()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MultiProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "MultiProducerMessageExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "MultiProducerMessage", exchange: "MultiProducerMessageExchange", routingKey: string.Empty, arguments: null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body.ToArray());
                            Console.WriteLine($"消费者02 接受消息: {message}");
                        };
                        channel.BasicConsume(
                            queue: "MultiProducerMessage",
                            autoAck: true,
                            consumer: consumer);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }

        public static void Show03()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MultiProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "MultiProducerMessageExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "MultiProducerMessage", exchange: "MultiProducerMessageExchange", routingKey: string.Empty, arguments: null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body.ToArray());
                            Console.WriteLine($"消费者03 接受消息: {message}");
                        };
                        channel.BasicConsume(
                            queue: "MultiProducerMessage",
                            autoAck: true,
                            consumer: consumer);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }
    }

}
