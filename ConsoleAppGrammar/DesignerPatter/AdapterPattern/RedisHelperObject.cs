using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.AdapterPattern
{
    /// <summary>
    /// 对象适配器，不需要继承
    /// </summary>
    public class RedisHelperObject :  IHelper
    {
        private RedisHelper _redisHelper = new RedisHelper();
        public void Add<T>(T t)
        {
            _redisHelper.AddRedis<T>(t);
        }

        public void Delete<T>(T t)
        {
            _redisHelper.DeleteRedis<T>(t);
        }

        public void Query<T>(T t)
        {
            _redisHelper.QueryRedis<T>(t);
        }

        public void Update<T>(T t)
        {
            _redisHelper.UpdateRedis<T>(t);
        }
    }
}
