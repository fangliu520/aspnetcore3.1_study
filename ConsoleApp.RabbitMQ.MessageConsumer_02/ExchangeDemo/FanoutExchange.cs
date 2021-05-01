﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageConsumer_02.ExchangeDemo
{
    public class FanoutExchange
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (var connection = factory.CreateConnection())
            {
                //创建通道channel
                using (var channel = connection.CreateModel())
                {
                    //声明交换机exchang
                    channel.ExchangeDeclare(exchange: "FanoutExchange",
                                            type: ExchangeType.Fanout,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);
                    //声明队列queue
                    channel.QueueDeclare(queue: "FanoutExchangeZhaoxi002",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //绑定exchange和queue
                    channel.QueueBind(queue: "FanoutExchangeZhaoxi002", exchange: "FanoutExchange", routingKey: string.Empty, arguments: null);
                    //定义消费者                                      
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        //只是为了演示，并没有存入文本文件
                        Console.WriteLine($"接收成功！【{message}】，邮件通知");
                    };
                    Console.WriteLine("通知服务准备就绪...");
                    //处理消息
                    channel.BasicConsume(queue: "FanoutExchangeZhaoxi002",
                                         autoAck: true,
                                         consumer: consumer);
                    Console.ReadLine();
                }

            }
        }
    }
}
