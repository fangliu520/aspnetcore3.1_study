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
                 rc.AddItemToList(listId,value);
            }

        }
        #endregion
    }
}
