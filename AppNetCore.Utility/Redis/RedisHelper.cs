/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       RedisHelper
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Utility.Redis
   * 文 件 名：       RedisHelper
   * 创建时间：       2020/10/20 10:45:24
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
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace AppNetCore.Utility.Redis
{
    public class RedisHelper : IRedisHelper
    {
        public RedisConnectionOption _option = null;

        public RedisHelper(IOptionsMonitor<RedisConnectionOption> optionMoniter)
        {
            _option = optionMoniter.CurrentValue;
        }


        #region  Redis缓存层
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        public void SetAll<T>(Dictionary<string, T> dic)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                rc.SetAll<T>(dic);
            }
        }
        //批量获取
        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                return rc.GetAll<T>(keys);
            }
        }

        /// <summary>
        /// 缓存数据存储在redis
        /// </summary>
        /// <param name="rkey">键</param>
        /// <param name="rvalue">值</param>
        /// <param name="exptime">过期时间</param>
        public void Add<T>(string rkey, T rvalue, DateTime? exptime = null)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                if (exptime == null)
                {
                    rc.Add(rkey, rvalue);
                }
                else
                {
                    rc.Add(rkey, rvalue, exptime.Value);
                }

            }
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="rkey"></param>
        public void Delete(string rkey)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                rc.Del(rkey);
            }

        }
        /// <summary>
        /// 存在返回true
        /// </summary>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public bool Exists(string rkey)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                return rc.Exists(rkey) > 0;
            }

        }
        /// <summary>
        /// 根据键名获取键值
        /// </summary>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public T Get<T>(string rkey)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                return rc.Get<T>(rkey);
            }

        }

        public void AddItemToList(string listId, string value)
        {
            using (RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password))
            {
                rc.AddItemToList(listId, value);
            }

        }


        #endregion

        #region 发布订阅消息（基于redis）
        /// <summary>
        /// 订阅者，一般用于窗体应用开发、或者windows 服务在进程中一直运行接收消息
        /// </summary>
        /// <param name="channel"></param>
        public void SubscribeMsg(string channel)
        {
            try
            {
                RedisClient rc = new RedisClient(_option.Server, _option.Port, _option.Password);
                Console.WriteLine("创建订阅信息文本");
                var subscription = rc.CreateSubscription();
                subscription.OnMessage = (channelRedis, msgRedis) =>
                {
                    //开始编写业务逻辑……
                    if (msgRedis != "CTRL:PULSE")
                    {
                        Console.WriteLine($"从频道{channelRedis}上接收消息：{msgRedis},时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")}");
                        Console.WriteLine("———————记录成功——————————————");
                    }
                };
                //开始订阅
                subscription.OnSubscribe = (channelRedis) =>
                {
                    Console.WriteLine($"订阅客户端：开始订阅{channelRedis}");
                };
                //取消订阅
                subscription.OnUnSubscribe = (a) =>
                {
                    Console.WriteLine($"订阅客户端：取消订阅{a}");
                };
                subscription.SubscribeToChannels(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}|{ex.StackTrace}");
            }

        }

        public void PubLishMsg(string channel, string msg)
        {
            try
            {

                Console.WriteLine("发布服务");
                IRedisClientsManager redisClients = new PooledRedisClientManager($"{_option.Password}@{_option.Server}:{_option.Port}");              
                RedisPubSubServer redisPubSub = new RedisPubSubServer(redisClients, channel)
                //{
                //    //OnMessage = (channel, msg) =>
                //    //{
                //    //    Console.WriteLine($"从频道{channel}上接收消息：{msg},时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")}");
                //    //    Console.WriteLine("————————————————————————————");
                //    //},
                //    OnStart = () =>
                //    {
                //        Console.WriteLine("————————发布服务已启动————————————");


                //    }
                //    ,
                //    OnStop = () => { Console.WriteLine("发布服务停止"); },
                //    OnUnSubscribe = c => { Console.WriteLine(c); },
                //    OnError = e => { Console.WriteLine(e.Message); },
                //    OnFailover = s => { Console.WriteLine(s); }

                //}
                ;
                redisPubSub.Start();
                redisClients.GetClient().PublishMessage(channel, msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
