using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleAppGrammar.FlyweightPattern
{
    /// <summary>
    /// 享元模式（flyweight)
    /// 
    /// 享元模式--池化资源管理--C#内存分配
    /// </summary>
    public class FlyweightPatternServer
    {
        public static void Show()
        {
            ///做到对象复用，又不破坏封装
            ///1、统一入口，控制全局的东西
            ///2、添加重用逻辑--静态字典缓存起来
            ///
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    Task.Run(() =>
                //                   {
                //                       BaseWord e1 = FlyweightFactory.CreateWord(WordType.E);
                //                       BaseWord e = FlyweightFactory.CreateWord(WordType.E);
                //                       BaseWord l = FlyweightFactory.CreateWord(WordType.L);
                //                       BaseWord v = FlyweightFactory.CreateWord(WordType.V);

                //                       Console.WriteLine($"{e1.Get()}{l.Get()}{v.Get()}{e.Get()}");
                //                    //   Thread.Sleep(1000);
                //                   });
                //}


            }
            ///享元模式：为了解决对象的复用问题，提供第三方的管理，能完成对象的复用
            ///1、还可以自行实例化对象，不像单例模式强制保证
            ///2、享元工厂可以初始化保存多个对象——其他的地方调用都可以找我拿（修改个状态）--用完之后再放回来（状态改回来）
            ///--避免重复创建和销毁对象，尤其对于非托管资源--》这个就是我们池化资源管理
            {
                BaseWord e1 = FlyweightFactory.CreateWord(WordType.E);
                E e2 = new E();
                E e3 = new E();
                Console.WriteLine(object.ReferenceEquals(e1,e2));
                Console.WriteLine(object.ReferenceEquals(e1, e3));
                Console.WriteLine(object.ReferenceEquals(e3, e2));
                //打印结果：false false false
            }
            //C#内存分配问题
            {
                string a = "Hello";
                string b = "Hello";
                Console.WriteLine(object.ReferenceEquals(a, b));//true  
                Console.WriteLine(object.ReferenceEquals(a, new OtherClass().C));//true  
                // string 在内存分配时使用享元模式
                //只要是统一进程，分配 Hello都是同一个内存地址 所以以上答案都是true


                string d = string.Format("He{0}", "llo");
                string part = "llo";
                string e = "He" + part;
                Console.WriteLine(object.ReferenceEquals(a, d));//false
                Console.WriteLine(object.ReferenceEquals(a, e));//false
                Console.WriteLine(object.ReferenceEquals(d, e));//false
                //以上这两个是部分分配空间，虽然最终结果是Hello，没法重用
                Console.WriteLine("**************");
                string f = "He" + "llo";
                string h = $"He{part}";
                Console.WriteLine(object.ReferenceEquals(a, f));//true
                Console.WriteLine(object.ReferenceEquals(a, h));//false
                Console.WriteLine(object.ReferenceEquals(h, f));//false
                //编译器的优化问题 a="Hello" 等同于  f = "He" + "llo"; 反编译器可以查看 +号不存在，直接是 f="Hello "

            }
            {
                //BaseWord e1 = FlyweightFactory.CreateWord(WordType.E);
                //BaseWord e = FlyweightFactory.CreateWord(WordType.E);
                //BaseWord l = FlyweightFactory.CreateWord(WordType.L);
                //BaseWord v = FlyweightFactory.CreateWord(WordType.V);

                //Console.WriteLine($"{e1.Get()}{l.Get()}{v.Get()}{e.Get()}");
            }

        }

        public class OtherClass {
            public string C = "Hello";
        }
    }
}
