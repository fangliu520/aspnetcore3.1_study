/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       CustomConnectionFactory
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Interface.Extend
   * 文 件 名：       CustomConnectionFactory
   * 创建时间：       2020/9/21 14:02:09
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
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppNetCore.Interface.Extend
{
    public class CustomConnectionFactory : ICustomConnectionFactory
    {
        private DBConnectionOption _Options = null;
        private IDbConnection _Connection = null;
        private static int _iSeed = 0;

        private static bool _isSet = true;
        private static readonly object lock_Read = new object();
        public CustomConnectionFactory(IOptionsMonitor<DBConnectionOption> options, IDbConnection dbConnection)
        {
            _Options = options.CurrentValue;
            _Connection = dbConnection;

            if (_isSet)
            {

                lock (lock_Read) {
                    if (_isSet)
                    {
                        _iSeed = _Options.ReadConnectionList.Count;
                        _isSet = false;
                    }
                }
            
            }

        }
        public IDbConnection GetConnection(WriteAndRead writeAndRead)
        {
            switch (writeAndRead)
            {
                case WriteAndRead.Write:
                    _Connection.ConnectionString = _Options.WriteConnection;
                    break;
                case WriteAndRead.Read:
                    _Connection.ConnectionString = QueryStategy();
                    break;
            }
            return _Connection;
        }

        private string QueryStategy()
        {
            switch (_Options.strategy)
            {
                case Strategy.Polling:
                    return Polling();

                case Strategy.Random:
                    return Random();

                default:
                    throw new Exception("分库查询策略不存在");


            }
        }

        /// <summary>
        /// 随机策略
        /// </summary>
        /// <returns></returns>
        private string Random()
        {
            int Count = _Options.ReadConnectionList.Count;
            int index = new Random().Next(0, Count);
            return _Options.ReadConnectionList[index];
        }
        /// <summary>
        /// 轮询策略
        /// </summary>
        /// <returns></returns>
        private string Polling()
        {

            return _Options.ReadConnectionList[_iSeed++ % _Options.ReadConnectionList.Count];
        }
    }
}
