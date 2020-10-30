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
    }
}
