﻿using ConsoleApp.RabbitMQ.MessageConsumer.MessageConsumer;
using System;

namespace ConsoleApp.RabbitMQ.MessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 架构师VIP班-1
                {
                    //生产者消费者
                    //ProductionConsumer.Show();
                }

                {
                    //多生产消费者
                    //Task.Run(() => { MultiProductionConsumer.Show01(); });
                    //Task.Run(() => { MultiProductionConsumer.Show02(); });
                    //Task.Run(() => { MultiProductionConsumer.Show03(); });
                }
                {
                    //互为生产消费者
                    //Task.Run(() => { MutualProductionConsumer.ShowProductio(); });
                    //Task.Run(() => { MutualProductionConsumer.ShowConsumer(); });
                }
                {
                    ////秒杀
                      SeckillConsumer.Show();
                }

                {
                    //发布订阅模式
                    //PublishSubscribeConsumer.Show();
                }
                {
                    //PriorityQueue.Show();
                }

                #endregion

                #region 架构师VIP班-2
                {
                    // DirectExchangeConsumerLogAll.Show();
                }
                {
                    // FanoutExchange.Show();
                }
                {
                    //TopicExchange.Show();
                }
                #endregion

                #region 架构师VIP班-3 
                {
                    //ConsumptionACKConfirm.Show();
                }
                #endregion

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
