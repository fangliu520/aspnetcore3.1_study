using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppGrammar.SingleTon
{
    /// <summary>
    /// 懒汉式的单例模式
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private Singleton()
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
        private static Singleton _Singleton = null;
        private static readonly object Singleton_lock = new object();
        /// <summary>
        /// 2公开一个静态的方法提供实例
        /// </summary>
        /// <returns></returns>
        public static Singleton CreateInstance()
        {
            //_Singleton = new Singleton();//不用单例
            if (_Singleton == null)
            {
                lock (Singleton_lock)
                {
                    if (_Singleton == null)
                    {
                        _Singleton = new Singleton();
                    }
                }

            }
            return _Singleton;
        }

        //既然是单例，大家用的是同一个对象，用的是同一个方法，那还会并发吗  还有线程安全问题吗？
        public int iTotal = 0;
        public void Show()
        {
            //Console.WriteLine($"This is {this.GetType().Name} {Thread.CurrentThread.ManagedThreadId}");
            lock (Singleton_lock)
            {
                this.iTotal++;
            }
        }
        public static void Test()
        {
            Console.WriteLine("Test1");
        }
    }
}
