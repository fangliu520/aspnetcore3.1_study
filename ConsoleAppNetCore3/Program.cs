using AppNetCore.Utility.CustomAop;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Threading;
using AppNetCore.Utility.Redis;
using AppNetCore.Utility.ConfigFile;

namespace ConsoleAppNetCore3
{
    class Program
    {
        private const string SERVER = "127.0.0.1";
        private const int PORT = 6379;
        private const string PWD = "123456";
        //ServiceStack.Redis;
        private static RedisClient rc = new RedisClient(SERVER, PORT, PWD);


        //StackExchange.Redis;
        //private static ConnectionMultiplexer rc = ConnectionMultiplexer.Connect("127.0.0.1:6379,127.0.0.1:6380,127.0.0.1:6381,127.0.0.1:6382,127.0.0.1:6383,127.0.0.1:6384,password=123456");
        static void Main(string[] args)
        {

            var conn_w = ConfigurationManager.GetNode("ConnectionString:WriteConnection");
            var conn_read=  ConfigurationManager.GetNode("ConnectionString:ReadConnectionList:0");
            string s = conn_read;
           // RedisClientHelper.MainHelper();
           //
           //string cityLimit = "{\"南京\":{shipCost:20},\"苏州\":{shipCost:20}}";
           //string str = "[{\"id\":1,\"cities\":[\"上海\",\"成都\",\"苏州\",\"杭州\",\"长沙\",\"宁波\",\"无锡\",\"南京\",\"北京\",\"天津\",\"重庆\",\"台州\"],\"warnLabel\":\"（满39元免配送费）\",\"minAmount\":39,\"shipCost\":10,\"status\":true,\"allowCoupon\":false,\"allowActivityProducts\":true,\"allowDaijinCard\":true,\"allowNorDaijinCard\":true,\"allowEmpDaijinCard\":true,\"allowBigGiftBag\":true,\"allowExchangeVouchers\":true,\"cityLimit\":{\"南京\":{shipCost:20},\"苏州\":{shipCost:20}}}]";

            //var item2 = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(str);
            //var l = item2[0]["cityLimit"];
            //var item=  JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string,int>>>(l.ToString());

            //var city = item["南京"];
            //var ship = city["shipCost"];




            //{
            //    new ReflectionCeshi().Test();
            //}

            #region 多线程编程Task
            //ThreadServer server = new ThreadServer();
            //List<string> rs = new List<string>();

            //List<Task> tasks = new List<Task>();
            //Console.WriteLine($"开始请求：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            //tasks.Add(Task.Factory.StartNew(() =>
            //{
            //    var s = server.A();
            //    rs.Add(s);
            //}));
            //tasks.Add(Task.Factory.StartNew(() =>
            //{
            //    var s = server.B();
            //    rs.Add(s);
            //}));
            //tasks.Add(Task.Factory.StartNew(() =>
            //{
            //    var s = server.C();
            //    rs.Add(s);
            //}));
            //// Task.WaitAll(tasks.ToArray());
            //Task.Factory.ContinueWhenAll(tasks.ToArray(), (t) =>
            //{

            //    foreach (string str in rs)
            //        Console.WriteLine(str);
            //    Console.WriteLine($"请求结束：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            //});

            //Console.WriteLine($"请求结束1：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            #endregion
            #region 多线程编程Task 带返回值

            //{
            //    Func<string> func = () =>
            //    {
            //        Thread.Sleep(3000);
            //        return DateTime.Now.ToShortTimeString();
            //    };
            //    Task<string> task = new Task<string>(func);
            //    task.Start();
            //    string iResult = task.Result;
            //    Console.WriteLine(iResult);
            //} 
            #endregion
            //DateTime dt = Convert.ToDateTime("2020-09-29 13:53:31");
            //int num = dt.CompareTo(DateTime.Now);
            //Console.WriteLine(num);
            //rc.Set<long>("number",1);

            //long n = rc.Decr("number");
            //Console.WriteLine(n);

            #region Redis分布式事务
            //rc.Set("mjd_trans_a", "1");
            //rc.Set("mjd_trans_b", "1");
            //rc.Set("mjd_trans_c", "1");

            //rc.Watch("mjd_trans_a", "mjd_trans_b", "mjd_trans_c");//redis分布式事务关键点:监控，  只能提交、不能回滚
            //using (var tran = rc.CreateTransaction())
            //{
            //    tran.QueueCommand(p => p.Set("mjd_trans_a", 3));
            //    tran.QueueCommand(p => p.Set("mjd_trans_b", 3));
            //    tran.QueueCommand(p => p.Set("mjd_trans_c", 3));
            //    //提交事务，触发事务提交过程中，其他进程操作了当前的key,则提交失败，没有指令没有修改成功！
            //    var flag = tran.Commit();
            //    Console.WriteLine(flag);
            //} 
            #endregion

            #region 发布订阅日志消息

            //try
            //{

            //    Console.WriteLine("发布服务");

            //    IRedisClientsManager redisClients = new PooledRedisClientManager($"{PWD}@{SERVER}:{PORT}");
            //    string topicname = "send_log";
            //    RedisPubSubServer redisPubSub = new RedisPubSubServer(redisClients, topicname)
            //    {
            //        OnMessage = (channel, msg) =>
            //        {
            //            Console.WriteLine($"从频道{channel}上接收消息：{msg},时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")}");
            //            Console.WriteLine("————————————————————————————");
            //        },
            //        OnStart = () =>
            //        {
            //            Console.WriteLine("————————发布服务已启动————————————");


            //        }
            //        ,
            //        OnStop = () => { Console.WriteLine("发布服务停止"); },
            //        OnUnSubscribe = channel => { Console.WriteLine(channel); },
            //        OnError = e => { Console.WriteLine(e.Message); },
            //        OnFailover = s => { Console.WriteLine(s); }

            //    };
            //    redisPubSub.Start();
            //    while (true)
            //    {
            //        Console.WriteLine("请输入您的内容：");
            //        string msg = Console.ReadLine();
            //        redisClients.GetClient().PublishMessage(topicname, msg);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion
            #region redis秒杀功能测试
            //string port; //姓名
            //Console.Write("请输入您的控制台号：");
            //port = Console.ReadLine();//接受控制台输入的一个字符串

            //string minute; //分钟
            //Console.Write("请输入分钟：");
            //minute = Console.ReadLine();
            //Console.WriteLine($"开始{port}");
            //new Seckill().SecondKill(port, int.Parse(minute));



            #region 秒杀测试指令 dotnet ConsoleAppNetCore3.dll  1  56
            //string port = "";
            //if (!string.IsNullOrEmpty(args[0]))
            //{
            //    Console.Write($"请输入您的控制台号：{args[0]}");
            //    port = args[0];//接受控制台输入的一个字符串
            //}


            //string minute = "0"; //分钟
            //if (!string.IsNullOrWhiteSpace(args[1]))
            //{
            //    Console.Write($"请输入分钟：{args[1]}");
            //    minute = args[1];
            //}

            //Console.WriteLine($"开始{port}");
            //new Seckill().SecondKill(port, int.Parse(minute)); 
            #endregion
            #endregion


            #region StackExchage Redis 操作集群

            //IDatabase db = rc.GetDatabase();
            //string value = "1";
            //db.StringSet("mykey", value);
            //string value1 = db.StringGet("mykey");
            //Console.WriteLine(value1); // writes: "1"

            #endregion
            #region HashReids简单应用
            //string hashid = "pro1:mjd";
            //rc.SetEntryInHash(hashid, "id", "1");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "id").FirstOrDefault()}");
            //rc.SetEntryInHash(hashid, "name", "卯金刀");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "name").FirstOrDefault()}");
            //rc.SetEntryInHash(hashid, "score", "100");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "score").FirstOrDefault()}");

            //hashid = "pro2:mjd";
            //rc.SetEntryInHash(hashid, "id", "1");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "id").FirstOrDefault()}");
            //rc.SetEntryInHash(hashid, "name", "卯金刀2");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "name").FirstOrDefault()}");
            //rc.SetEntryInHash(hashid, "score", "100");
            //Console.WriteLine($"获取：{rc.GetValuesFromHash(hashid, "score").FirstOrDefault()}"); 
            #endregion

            #region HashReids简单应用 批量插入
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("id","2");
            //dic.Add("name", "牛勇");
            //dic.Add("score", "99");
            //string hashid = "mjd_batch";
            //rc.SetRangeInHash(hashid, dic);

            //var list = rc.GetValuesFromHash(hashid, new string[] { "id", "name", "score" });

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"获取：{item}");
            //}

            #endregion
            #region set集合  自动去重功能

            //string set_key = "mjd_set_1";

            //Info mod = new Info() { name = "da天", num = "101", score = 3 };
            //rc.Set<Info>(set_key, mod);

            //Info mods = rc.Get<Info>(set_key);
            //Console.WriteLine($"{mods.name}|{mods.num}");

            //rc.AddItemToSet(set_key, JsonConvert.SerializeObject(mod));
            //rc.AddItemToSet(set_key, JsonConvert.SerializeObject(mod));
            //rc.AddItemToSet(set_key, JsonConvert.SerializeObject(mod));
            //mod = new Info() { name = "da天", num = "10",score=5 };
            //rc.AddItemToSet(set_key, JsonConvert.SerializeObject(mod));
            //Console.WriteLine("ok");
            #endregion

            #region zset集合 加入权重值可供排序
            //string zset_key = "mjd_zset_1";
            //Info mod = new Info() { name = "da天", num = "10", score = 10 };
            //rc.AddItemToSortedSet(zset_key, JsonConvert.SerializeObject(mod), 33);
            //List<string> infos = new List<string>();
            //for (int i = 0; i < 10; i++)
            //{
            //    infos.Add(JsonConvert.SerializeObject(new Info() { name = "da天", num = "10", score = i }));
            //}
            //rc.AddRangeToSortedSet(zset_key, infos, 32);

            //Console.WriteLine(rc.GetAllItemsFromSortedSet(zset_key)); 
            #endregion

            //#region list集合 redis常用list
            //Console.WriteLine("Hello World!");
            //string listkey = "redis_listlog";
            //while (1 == 1)
            //{
            //    Console.WriteLine("请输入发送的内容");
            //    var message = Console.ReadLine();
            //    rc.AddItemToList(listkey, message);
            //}
            //string list_key = "mjd_list_1";
            //List<string> lists = new List<string>();
            //for (int i = 10; i > 0; i--)
            //{
            //    rc.AddItemToList(list_key, JsonConvert.SerializeObject(new Info() { name = $"list集合{i}", num = $"{i}", score = i }));
            //}
            /////批量写入list
            ////for (int i = 10; i > 0; i--)
            ////{
            ////    lists.Add(JsonConvert.SerializeObject(new Info() { name = $"list集合{i}", num = $"{i}", score = i }));
            ////}

            ////rc.AddRangeToList(list_key, lists);
            ////// rc.ExpireEntryAt(list_key, DateTime.Now.AddMinutes(1));
            //var infos = rc.GetRangeFromList(list_key, 0, 5);
            //////rc.AddRangeToSet(list_key, lists);
            //////var infos = rc.GetAllItemsFromSet(list_key); 
            ////var infos = rc.GetAllItemsFromList(list_key);

            //foreach (string str in infos)
            //    Console.WriteLine(str); 
            //#endregion

            #region redis集合并集
            //rc.AddRangeToSet("keyone", new List<string>() { "001", "002", "003" });

            //rc.AddRangeToSet("keytwo", new List<string>() { "001", "002", "004" });

            //var uion = rc.GetUnionFromSets("keyone", "keytwo");
            //foreach (string s in uion)
            //{
            //    Console.WriteLine(s);
            //}
            #endregion
            //string jwt = new JWTHelper().Encode("");
            //Console.WriteLine(jwt);

            //string content = new JWTHelper().Decode(jwt);

            //Student stu = JsonConvert.DeserializeObject<Student>(content);
            //Console.WriteLine(stu.username);
            //AOP castle
            //CustomAopTest.show();

            //string phone = "127.0.211.122";
            ////Regex rx = new Regex(@"^(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[8-9])[0-9]{8}$");
            ////Regex rx = new Regex(@"^(1[3-9][0-9])[0-9]{8}$");
            ////Regex yx = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            //Regex rx = new Regex(@"^((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))$");

            //if (!rx.IsMatch(phone)) //不匹配
            //{
            //    Console.WriteLine($"{phone}手机格式不正确");
            //}
            //else
            //{
            //    Console.WriteLine($"{phone}手机格式正确");

            //}

            //string d = "2020-05-20T00:00:00";

            //DateTime dt = Convert.ToDateTime(d);

            //Console.WriteLine(dt.ToString("yyyy-MM-dd 00:00;00"));

            ////int[] str ={ 1, 2, 3, 4, 5, 6 };
            ////var s =[{ 1:"a"},{2:"b" },{3:"c" }];
            //////var s =["a":1];
            ////string s = "Hello World!";
            ////Console.WriteLine($"{str[1]}");
            //List<Info> arr = new List<Info>();
            //arr.Add(new Info() { num = "a",name="1" });
            //arr.Add(new Info() { num = "b", name = "2" });
            //arr.Add(new Info() { num = "c", name = "3" });
            ////string s = string.Join(',', arr);
            //List<string> s = arr.Where(x => x.num == "a").Select(x => x.name).ToList();

            //Console.WriteLine(s[0]);
            Console.ReadLine();
        }

        public class Info
        {
            public string num { get; set; }
            public string name { get; set; }
            public int score { get; set; }
        }

    }

    public class Student
    {
        public string username { get; set; }
    }


    //头信息Header、有效载荷Payload、签名sign_key，中间以（.）分隔
    public class JWTHelper
    {
        private const string SIGNKEY = "mjd";

        /// <summary>
        /// 用md5加密当前字符串
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>string,加密后的字符串</returns>
        public static string Md5(string value)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] arr = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(value));
            foreach (byte b in arr)
            {
                sb.AppendFormat("{0:x}", b);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(string content)
        {
            Dictionary<string, object> dicH = new Dictionary<string, object>() { { "Alt", "HS256" } };

            string header = JsonConvert.SerializeObject(dicH);


            string payLoad = JsonConvert.SerializeObject(new Student() { username = "卯金刀" });

            string signKeyStr = Md5(payLoad + SIGNKEY);

            string jwtStr = $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(header))}.{Convert.ToBase64String(Encoding.UTF8.GetBytes(payLoad))}.{signKeyStr}";
            return jwtStr;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Decode(string content)
        {
            string[] arr = content.Split(".");
            string header = Encoding.UTF8.GetString(Convert.FromBase64String(arr[0]));

            string payload = Encoding.UTF8.GetString(Convert.FromBase64String(arr[1]));

            string signmd5 = Md5(payload + SIGNKEY);
            if (signmd5.Equals(arr[2]))
            {
                return payload;
            }
            return "";
        }
    }
}
