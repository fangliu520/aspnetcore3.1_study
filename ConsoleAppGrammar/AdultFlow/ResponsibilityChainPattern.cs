using ConsoleAppGrammar.AdultFlow.FactoryMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow
{
    public class ResponsibilityChainPattern
    {
        /// <summary>
        /// 审批流程的一步步封装
        ///
        /// 行为型设计模式的巅峰之作:责任链模式Respon

        /// </summary>
        public static void AdultShow()
        {
            ApplyContext context = new ApplyContext()
            {
                Id = 101,
                Name = "卯金刀",
                Hour = 30,
                AdultResult = false
            };

            //方法五//工厂方法模式 就是为了扩展 为了屏蔽细节
            {
                
                IFactory factory = new PMFactoryChild();//new PMFactory(); 包一层child就是为了进一步扩展
                AbstractAdult PM = factory.CreateInstance();
                factory = new ChargerFactory();
                AbstractAdult Charger = factory.CreateInstance();
                factory = new ManagerFactory();
                AbstractAdult Manager = factory.CreateInstance();

                //以上的创建对象还可以不用new ，可以用创建型设计模式 中的简单工厂 工厂方法 抽象工厂

                PM.SetNext(Charger);
                Charger.SetNext(Manager);
                //PM.SetNext(Manager);//保证环节的稳定、可以灵活的配置
                PM.Adult(context);
            }
            //方法四 简单工厂
            //{
            //    AbstractAdult PM = SimpleFactory.CreateInstance(SimpleFactory.AdultType.PM);
            //    AbstractAdult Charger = SimpleFactory.CreateInstance(SimpleFactory.AdultType.Charger);
            //    AbstractAdult Manager = SimpleFactory.CreateInstance(SimpleFactory.AdultType.Manager);

            //    //以上的创建对象还可以不用new ，可以用创建型设计模式 中的简单工厂 工厂方法 抽象工厂

            //    PM.SetNext(Charger);
            //    Charger.SetNext(Manager);
            //    //PM.SetNext(Manager);//保证环节的稳定、可以灵活的配置
            //    PM.Adult(context);
            //}

            //方法三
            //{
            //    AbstractAdult PM = new PM()
            //    {
            //        Name = "张三组长"
            //    };
            //    AbstractAdult Charger = new Charger()
            //    {
            //        Name = "李四主管"
            //    };
            //    AbstractAdult Manager = new Manager()
            //    {
            //        Name = "王五经理"
            //    };

            //    //以上的创建对象还可以不用new ，可以用创建型设计模式 中的简单工厂 工厂方法 抽象工厂

            //    //PM.SetNext(Charger);
            //    //Charger.SetNext(Manager);
            //    PM.SetNext(Manager);//保证环节的稳定、可以灵活的配置
            //    PM.Adult(context);
            //}

            //方法二 抽象父类 面向对象第一步 多态、封装、继承都有
            //{

            //    AbstractAdult PM = new PM()
            //    {
            //        Name = "张三组长"
            //    };
            //    PM.Adult(context);
            //    if (!context.AdultResult)
            //    {
            //        AbstractAdult Charger = new Charger()
            //        {
            //            Name = "李四主管"
            //        };
            //        Charger.Adult(context);

            //        if (!context.AdultResult)
            //        {
            //            AbstractAdult Manager = new Manager()
            //            {
            //                Name = "王五经理"
            //            };
            //            Manager.Adult(context);

            //        }
            //    }


            //}
            //方法一（最简单，不涉及面向对象封装）
            //{
            //    //面向过程编程
            //    if (context.Hour < 8)
            //    {
            //        Console.WriteLine("PM审批通过");
            //    }
            //    else if (context.Hour < 16)
            //    {
            //        Console.WriteLine("主管审批通过");
            //    }
            //    else {
            //        Console.WriteLine("……");
            //    }
            //}

        }
    }
}
