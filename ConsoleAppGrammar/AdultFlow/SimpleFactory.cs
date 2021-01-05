using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow
{
    public class SimpleFactory
    {
        /// <summary>
        /// 简单工厂集中了所有的类，不推荐，下一个升级到工厂方法
        /// </summary>
        /// <param name="adultType"></param>
        /// <returns></returns>
        public static AbstractAdult CreateInstance(AdultType adultType)
        {
            AbstractAdult _adult = null;
            switch (adultType)
            {
                case AdultType.PM:
                    _adult = new PM()
                    {
                        Name = "张三组长"
                    };
                    break;
                case AdultType.Charger:
                    _adult = new Charger()
                    {
                        Name = "李四主管"
                    };
                    break;
                case AdultType.Manager:
                    _adult= new Manager()
                    {
                        Name = "王五经理"
                    };
                    break;
                default:
                    break;
            }
            return _adult;

        }

        public enum AdultType
        {

            PM,
            Charger,
            Manager
        }
    }
}
