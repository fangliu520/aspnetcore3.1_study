﻿using ConsoleApp.RabbitMQ.MessageConsumer_02.ExchangeDemo;
using System;

namespace ConsoleApp.RabbitMQ.MessageConsumer_02
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 架构师VIP班-1
                {
                    //发布订阅模式
                    //PublishSubscribeConsumer.Show();
                }
                #endregion

                #region 架构师VIP班-2
                {
                   // DirectExchangeConsumerLogError.Show();
                }
                {
                    //FanoutExchange.Show();
                }
                {
                    TopicExchange.Show();
                }
                #endregion

                #region 架构师VIP班-3
                //ConsumptionACKConfirm.Show();
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
