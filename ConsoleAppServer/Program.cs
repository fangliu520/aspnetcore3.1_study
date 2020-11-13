using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Threading;

namespace ConsoleAppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                Console.WriteLine(args[0]);
            }
            Console.WriteLine("订阅消息开始!");
            //SecondKillServer();
            SubscribeMsg();
            Console.ReadLine();
        }


        private const string SERVER = "127.0.0.1";
        private const int PORT = 6379;
        private const string PWD = "123456";
        private static RedisClient rc = new RedisClient(SERVER, PORT, PWD);
        public static void SecondKillServer()
        {

            #region 秒杀 temple2   发布者-订阅者实现
            var flag = true;
            while (flag)
            {
                string listKey = "orderlist";
                //var content =rc.RemoveEndFromList(listKey);//先进后出 栈顺序
                var content =rc.RemoveStartFromList(listKey) ;//先进先出  堆顺序
                if (!string.IsNullOrWhiteSpace(content))
                {
                    OrderInfo mod = JsonConvert.DeserializeObject<OrderInfo>(content);
                    Console.WriteLine($"##############下单完成{mod.Name}######{mod.OrderTime}");
                }
                Thread.Sleep(1000);

            }
            #endregion

        }

        public static void SubscribeMsg()
        {
            try 
            {
                Console.WriteLine("创建订阅信息文本");
                var subscription = rc.CreateSubscription();
                subscription.OnMessage = (channel, msg) =>
                {
                    if (msg != "CTRL:PULSE")
                    {
                        Console.WriteLine($"从频道{channel}上接收消息：{msg},时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")}");
                        Console.WriteLine("———————记录成功——————————————");
                    }
                };
                //开始订阅
                subscription.OnSubscribe = (channel) =>
                {
                    Console.WriteLine($"订阅客户端：开始订阅{channel}");
                };
                //取消订阅
                subscription.OnUnSubscribe = (a) =>
                {
                    Console.WriteLine($"订阅客户端：取消订阅{a}");
                };
                string topicname = "send_log";
                subscription.SubscribeToChannels(topicname);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}|{ex.StackTrace}");
            }
        
        }
    }
    public class OrderInfo
    {
        public string Name { get; set; }
        public string OrderTime { get; set; }
    }
}

