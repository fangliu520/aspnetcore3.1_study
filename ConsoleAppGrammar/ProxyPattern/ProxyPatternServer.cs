using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppGrammar.ProxyPattern
{
    public class ProxyPatternServer
    {
        /// <summary>
        /// 代理模式（proxy) 是一个极简的模式，是AOP实现最最核心的
        /// 本身没有实现任何业务，只是做了代理：VPN 、火车票代售、翻墙软件
        /// 
        /// 异常代理 try catch  在ProxySubject
        /// 
        /// 单例代理 
        /// 单例就是某个进程中只有一个实例，升级类对象，添加单例逻辑
        /// 在Proxy里面完成 通过升级proxy 避免修改real
        /// 
        /// 缓存代理
        /// 系统性能优化的第一步就是使用缓存
        /// 缓存--第一使用的结果直接缓存起来，下次直接用，可以节约 时间
        /// 封装一个第三方缓存--代理查询时，优先使用缓存--节约时间
        /// 通过升级proxy 避免修改real
        /// 
        /// 
        /// 延迟代理
        /// 哪些地方有延迟
        /// EF--前端延迟加载--Lazy<T> --Linq---队列--泛型声明--多线程
        /// 延迟加载，对象初始化的时候不占用资源
        /// 通过升级proxy 避免修改real
        /// 
        /// 权限代理        /// 
        /// 权限 鉴权-授权-认证 ，方法不能直接调用，需要验证权限--登录
        /// 加个参数--用户信息--方法里面校验--通过才允许--执行
        /// --web里面没有加参数，而是通过HttpContext.Current.Session
        /// --权限验证逻辑很麻烦，多种多样，不能都放在real
        /// 通过升级proxy 避免修改real
        /// 
        /// 6个代理 通过升级proxy 避免修改real 完成功能扩展 是AOP实现最最核心的
        /// 
        /// 但是，无论是装饰器 还是代理模式 他们都是静态的
        /// AOP如果真的实战中用，必须是动态的，动态代理 .Net Core 里面有个第三方插件Castle.Core
        /// </summary>
        public static void Show()
        {

            {
                //直接调用真实业务
               // Console.WriteLine("********RealSubject***********");
               //ISubject subject = new RealSubject();
               // subject.DoSomethingLong();
               // subject.GetSomethingLong();
            }

            {
                ///代理模式
                Console.WriteLine("********ProxySubject***********");
                ISubject subject = new ProxySubject();
                subject.DoSomethingLong();
                subject.GetSomethingLong();
               
                
            }

            {
                Console.WriteLine("********使用缓存ProxySubject***********");
                ISubject subject = new ProxySubject();

                Thread.Sleep(1000);
                Console.WriteLine("延迟1s");

                Thread.Sleep(1000);
                Console.WriteLine("延迟2s");

                Thread.Sleep(1000);
                Console.WriteLine("延迟3s");
                subject.GetSomethingLong();
            }
        }
    }
}
