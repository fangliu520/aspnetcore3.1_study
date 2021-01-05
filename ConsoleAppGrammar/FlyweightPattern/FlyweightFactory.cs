using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FlyweightPattern
{
    public class FlyweightFactory
    {
        private static Dictionary<WordType, BaseWord> FlyweightFactoryDic = new Dictionary<WordType, BaseWord>();
        private static readonly object FlyweightFactoryLock = new object();
        public static BaseWord CreateWord(WordType wordType)
        {
            if (!FlyweightFactoryDic.ContainsKey(wordType))//为了优化性能，避免对象已经被初始化后还要等待锁
            {
                lock (FlyweightFactoryLock)//C#语法糖 Monitor.Enter 保证方法体只有一个线程进去
                {
                    if (!FlyweightFactoryDic.ContainsKey(wordType))
                    {
                        BaseWord baseWord = null;
                        switch (wordType)
                        {
                            case WordType.E:
                                baseWord = new E();
                                break;
                            case WordType.L:
                                baseWord = new L();
                                break;
                            case WordType.V:
                                baseWord = new V();
                                break;
                            default:
                                throw new Exception("Wrong word TYpe");
                        }
                        FlyweightFactoryDic.Add(wordType, baseWord);                       
                    }
                }
            }

            return FlyweightFactoryDic[wordType];

            //方法一
            //BaseWord baseWord = null;
            //switch (wordType)
            //{
            //    case WordType.E:
            //        baseWord = new E();
            //        break;
            //    case WordType.L:
            //        baseWord = new L();
            //        break;
            //    case WordType.V:
            //        baseWord = new V();
            //        break;
            //    default:
            //        throw new Exception("Wrong word TYpe");
            //}

            //return baseWord;
        }
    }

    public enum WordType
    {
        E,
        L,
        V

    }
}
