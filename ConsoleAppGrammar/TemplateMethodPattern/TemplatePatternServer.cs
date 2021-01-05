using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    public class TemplatePatternServer
    {
        /// <summary>
        /// 模板方法设计模式 行为型设计模式：关注对象和行为的分离
        /// 抽象方法、虚方法、普通方法
        /// 钩子方法 指的就是虚方法
        /// 
        /// 简单、强大、无所不在，框架搭建必备
        /// 
        ///模板方法： 定义了通用处理流程，实现了通用的部分，可变部分留作扩展点
        ///框架搭建：定义了业务处理流程，实现了通用的部分，可变部分留作扩展点
        /// </summary>
        public static void Show()
        {
            {
                Console.WriteLine("***********普通客户**********");
                BankClient client = new BankClient();
                client.Query(11, "卯金刀", "565656");
            }
            {
                Console.WriteLine("***********活期客户**********");
                //抽象方法由子类实现，初始化实例的时候指定子类
                BaseClient baseClient = new ClientCurrent();
                baseClient.Query(23, "小二", "we2222");
            }

            {
                Console.WriteLine("***********定期客户**********");
                //抽象方法由子类实现，初始化实例的时候指定子类
                BaseClient baseClient = new ClientRegular();
                baseClient.Query(20, "礼物", "we2222");
            }
            {
                Console.WriteLine("***********VIP客户**********");
                //抽象方法由子类实现，初始化实例的时候指定子类
                BaseClient baseClient = new ClientVip();
                baseClient.Query(23, "张三", "we2222");
            }

        }
    }
}
