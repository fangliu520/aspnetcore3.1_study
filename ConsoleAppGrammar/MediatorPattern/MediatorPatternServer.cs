using ConsoleAppGrammar.MediatorPattern.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.MediatorPattern
{
    /// <summary>
    /// 中介者模式：
    /// 
    /// </summary>
    public class MediatorPatternServer
    {
        public static void Show()
        {

            BaseCharacter teacher = new Teacher() { 
            Name="张大钱"
            };
            BaseCharacter master = new Master() { 
            Name="卯金刀"
            };
            BaseCharacter student1 = new Students() { 
            Name="小舞"
            };
            BaseCharacter student2 = new Students()
            {
                Name = "小二"
            };
            BaseCharacter student3 = new Students()
            {
                Name = "香蕉"
            };
            BaseCharacter student4 = new Students()
            {
                Name = "果冻"
            };
            teacher.SendMessage("今晚八点准时上课！请通知大家", master);
            master.SendMessage("收到，老师，立马通知下去！", teacher);

            master.SendMessage("今天晚上八点上课，不要迟到！", student1);
            Console.WriteLine($"**********************************************");
            {
                //消息一对多，群发消息
                BaseMediator baseMediator = new BaseMediator();
                baseMediator.Add(master);
                baseMediator.Add(student1);
                baseMediator.Add(student2);
                baseMediator.Add(student3);
                baseMediator.Add(student4);

                baseMediator.SendMessage("今天晚上八点上课，不要迟到！", master);

            }

        }
    }
}
