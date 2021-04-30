﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.RabbitMQ.MessageConsumer.Advance
{
    public class PriorityQueue
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
                    #region EventingBasicConsumer
                    //定义消费者                                      
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        string msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine(msg);
                        if (msg.Equals("消息：1"))
                        {
                            channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: false);

                            ///设置消息优先级最高 重新写入到队列中去
                            IBasicProperties props = channel.CreateBasicProperties();
                            props.Priority = 10;
                            channel.BasicPublish(exchange: "PriorityQueueExchange",
                                           routingKey: "PriorityKey",
                                           basicProperties: props,
                                           body: Encoding.UTF8.GetBytes(msg));

                        }
                    };
                    Console.WriteLine("消费者准备就绪....");
                    //处理消息
                    channel.BasicConsume(queue: "PriorityQueue", autoAck: false, consumer: consumer);
                    Console.ReadKey();
                    #endregion
                }
            }
        }
    }
}
