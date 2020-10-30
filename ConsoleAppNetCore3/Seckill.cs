/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       Seckill
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       ConsoleAppNetCore3
   * 文 件 名：       Seckill
   * 创建时间：       2020/10/20 14:58:33
   * 作    者：       LIUFANG
   * 说   明：
   * 类型                    命外规则                     说明
   * 命名空间  namespace     Pascal           以.分隔，其中每一个限定词均为Pascal命名方式 如ExcelQuicker.Work
   * 类 class                Pascal           每一个逻辑断点首字母大写 如public class MyHome
   * 接口 interface          IPascal          每一个逻辑断点首字母大写,总是以I前缀开始，后接Pascal命名 如public interface IBankAccount 
   * 方法 method             Pascal           每一个逻辑断点首字母大写如private void SetMember(string)
   * 枚举类型 enum           Pascal           每一个逻辑断点首字母大写
   * 委托 delegate        Pascal           每一个逻辑断点首字母大写局部变量方法的
   * 参数                    Camel            首字母小写，之后Pascal命名如string myName   
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack.Redis;
namespace ConsoleAppNetCore3
{
    public class Seckill
    {
        private const string SERVER = "127.0.0.2";
        private const int PORT = 6379;
        private const string PWD = "123456";
        private static RedisClient rc = new RedisClient(SERVER, PORT, PWD);
        public void SecondKill(string id, int minute)
        {

            rc.Set<long>("number", 10);

            Console.WriteLine($"在{minute}分0秒进行秒杀开始！");

            #region 秒杀Temple1
            //var flag = true;
            //while (flag)
            //{
            //    if (DateTime.Now.Minute == minute)
            //    {
            //        flag = false;
            //        for (int i = 0; i < 100; i++)
            //        {
            //            string name = $"客户端ID{id}号：{i}";
            //            Task.Run(() =>
            //            {           

            //                long num = rc.Decr("number");//减库存1 rc.Incr("number");//加一
            //                if (num < 0)
            //                {
            //                    Console.WriteLine($"{name}抢购失败！");
            //                }
            //                else
            //                {
            //                    Console.WriteLine($"{name}*************抢购成功！***********");
            //                }

            //            });
            //            Thread.Sleep(10);
            //        }
            //    } 
            #endregion

            #region 秒杀 temple2   
            var flag = true;
            while (flag)
            {
                if (DateTime.Now.Minute == minute)
                {
                    flag = false;
                    for (int i = 0; i < 100; i++)
                    {
                        string name = $"客户端ID{id}号：{i}";
                        Add(name);
                        //Task.Factory.StartNew(()=> {
                        //    OrderInfo mod = new OrderInfo()
                        //    {
                        //        Name = name,
                        //        OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        //    };
                        //    Console.WriteLine($"{name}下单成功！");
                        //    string listKey = "orderlist";
                        //    rc.AddItemToList(listKey, JsonConvert.SerializeObject(mod));
                        //});
                        //Task.Run(() =>
                        //{
                        //    OrderInfo mod = new OrderInfo()
                        //    {
                        //        Name = name,
                        //        OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        //    };
                        //    Console.WriteLine($"{name}下单成功！");
                        //    string listKey = "orderlist";
                        //    rc.AddItemToList(listKey, JsonConvert.SerializeObject(mod));


                        //});
                        // Thread.Sleep(10);
                    }
                }
                #endregion

            }


        }
        public static void Add(string name)
        {
            //Task.Factory.StartNew(() =>
            //{
            OrderInfo mod = new OrderInfo()
            {
                Name = name,
                OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            };
            string c = JsonConvert.SerializeObject(mod);
            Console.WriteLine($"{c}下单成功！");
            string listKey = "orderlist";
            rc.AddItemToList(listKey, c);
            //});
        }

    }
    public class OrderInfo
    {
        public string Name { get; set; }
        public string OrderTime { get; set; }
    }
}
