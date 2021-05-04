using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageProducer.ExchangeDemo
{
    public class DeadLetterExchange
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (var connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {

                    //延时队列
                    channel.QueueDeclare("DeadLetterMessage", durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object> {
                                         { "x-dead-letter-exchange","NomalLetterExChange"}, //设置当前队列的DLX
                                         { "x-dead-letter-routing-key","NomalLetterRoute"}, //设置DLX的路由key，DLX会根据该值去找到死信消息存放的队列
                                         { "x-message-ttl",10000} //设置消息的存活时间，即过期时间10s
                                         });
                    channel.ExchangeDeclare("DeadLetterExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "DeadLetterMessage", exchange: "DeadLetterExchange", routingKey: "DeadLetterRoute", arguments: null);

                    //正常队列
                    channel.QueueDeclare(queue: "NomalLetterMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    //持久化消息 要求：1.队列要求是持久化的，交换机是持久化的，发送的消息也是持久化的；
                    channel.ExchangeDeclare(exchange: "NomalLetterExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

                    channel.QueueBind(queue: "NomalLetterMessage", exchange: "NomalLetterExChange", routingKey: "NomalLetterRoute", arguments: null);


                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("生产者DeadLetterMessage已准备就绪~~~");

                    IBasicProperties basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;  //持久化消息
                                                        //basicProperties.DeliveryMode = 2;
                    string message = $"延时消息deadlettermessage";
                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "DeadLetterExchange",
                                    routingKey: "DeadLetterRoute",
                                    basicProperties: basicProperties,
                                    body: body);
                    Console.WriteLine($"延时消费消息：{message} 已发送~");

                   
                }
            }

        }
    }
}
