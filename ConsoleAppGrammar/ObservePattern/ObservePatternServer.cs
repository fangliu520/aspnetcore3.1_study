using ConsoleAppGrammar.ObservePattern.Observe;
using ConsoleAppGrammar.ObservePattern.Subject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ObservePattern
{
    /// <summary>
    ///  观察者模式
    ///  时间起源：
    ///  大半夜猫叫一声
    ///  婴儿哭
    ///  狗汪汪叫
    ///  老鼠跑
    ///  ……
    /// </summary>
    public class ObservePatternServer
    {
        public static void Show()
        {
            Console.WriteLine($"大半夜猫叫一声，引发一系列动作");
            {
                //Cat cat = new Cat();
                //cat.Miao();
            }
            Console.WriteLine($"******************");

            Cat cat = new Cat();
            IObserve obDog = new Dog();
            {
                
                //cat.AddObserve(obDog);//观察者人数可增可减少
                //cat.AddObserve(new Baby());
                //cat.MiaoObserve();
                //cat.AddObserve(new Mouse());
                //cat.RemoveObserve(obDog);
                //cat.MiaoObserve();
            }

            {
                //基于委托事件
                cat.catMiaoEvent +=new Dog().Wang;//观察者人数可增可减少
                cat.catMiaoEvent +=new Baby().Cry;          
                cat.catMiaoEvent +=new Mouse().Run;
             
                cat.MiaoEvent();
            }



        }
    }
}
