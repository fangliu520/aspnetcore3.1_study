﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppGrammar.SingleTonSecond
{
    /// <summary>
    /// 饿汉式构造函数
    /// </summary>
    public class SingletonSecond
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private SingletonSecond()
        {
            long iResult = 0;
            for (int i = 0; i < 100000; i++)
            {
                iResult += 1;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}被构造一次");
        }
        //提供一个静态变量
        private static SingletonSecond _Singleton = null;

        static SingletonSecond()
        {
            _Singleton = new SingletonSecond();
        }
        public static SingletonSecond CreateInstance()
        {                      
            return _Singleton;
        }

        //既然是单例，大家用的是同一个对象，用的是同一个方法，那还会并发吗  还有线程安全问题吗？
        public int iTotal = 0;
        public void Show()
        {
            Console.WriteLine($"This is {this.GetType().Name} {Thread.CurrentThread.ManagedThreadId}");
            //lock (Singleton_Lock)
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
