using System;
using System.Collections.Generic;
using System.Text;

namespace AppNetCore.Utility.Redis
{
    public interface IRedisHelper
    {
        void Add<T>(string rkey, T rvalue, DateTime? exptime = null);
        void Delete(string rkey);
        bool Exists(string rkey);
        T Get<T>(string rkey);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
         void SetAll<T>(Dictionary<string, T> dic);
        //批量获取
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        void AddItemToList(string listId, string value);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel">频道</param>
        /// <param name="msg">消息内容</param>
        void SubscribeMsg(string channel);
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        void PubLishMsg(string channel,string msg);
    }
}
