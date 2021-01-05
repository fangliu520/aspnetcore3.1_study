using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.AdapterPattern
{
    /// <summary>
    /// 类适配器：通过继承实现，缺点是父类有的所有东西，子类都存在，强类型耦合 父类  （不推荐）
    /// </summary>
    public class RedisHelperClass : RedisHelper, IHelper
    {
        public void Add<T>(T t)
        {
            base.AddRedis<T>(t);
        }

        public void Delete<T>(T t)
        {
            base.DeleteRedis<T>(t);
        }

        public void Query<T>(T t)
        {
            base.QueryRedis<T>(t);
        }

        public void Update<T>(T t)
        {
            base.UpdateRedis<T>(t);
        }
    }
}
