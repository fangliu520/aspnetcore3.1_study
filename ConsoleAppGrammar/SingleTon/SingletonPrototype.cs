using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppGrammar.SingletonPrototype
{
    /// <summary>
    ///原型模式
    /// </summary>
    public class SingletonPrototype
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private SingletonPrototype()
        {
            long iResult = 0;
            for (int i = 0; i < 100000; i++)
            {
                iResult += 1;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}被构造一次{Thread.CurrentThread.ManagedThreadId}");
        }
        //提供一个静态变量
        private static SingletonPrototype _Singleton = null;
        /// <summary>
        /// 2公开一个静态的方法提供实例
        /// </summary>
        /// <returns></returns>
        public static SingletonPrototype CreateInstance()
        {
            SingletonPrototype singletonPrototype = (SingletonPrototype)_Singleton.MemberwiseClone();//内存copy
            return singletonPrototype;//这个跟单例的区别就是新的实例，不是同一个，就是不会相互影响 快速复制对象，不走构造函数
        }

        //既然是单例，大家用的是同一个对象，用的是同一个方法，那还会并发吗  还有线程安全问题吗？
        public int iTotal = 0;
        public void Show()
        {
            Console.WriteLine($"This is {this.GetType().Name} {Thread.CurrentThread.ManagedThreadId}");
            //lock (Singleton_lock)
            //{
            //    this.iTotal++;
            //}
        }
        public static void Test()
        {
            Console.WriteLine("Test1");
        }
    }
}
