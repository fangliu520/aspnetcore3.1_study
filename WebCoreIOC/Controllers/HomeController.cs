using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppNetCore.Interface.Extend;
using AppNetCore.Service;
using AppNetCore.Service.SqlHelper;
using AppNetCore.Service.Utility;
using AppNetCore.Utility;
using AppNetCore.Utility.CustomContainer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebCoreIOC.Models;
using AppNetCore.Utility.CustomAop;
using Microsoft.AspNetCore.Authorization;

namespace WebCoreIOC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //自定义容器  常规IOC容器 容器对象--注册--生成
            //构造对象A时，需要依赖B对象，那么就需要先构造B对象传入，也就是能在构造对象时，对象的依赖初始化并注入进去，这种技术手段就叫做依赖注入
            //为什么要依赖注入  要IOC控制反转，不依赖细节，细节还会依赖细节，为了屏蔽细节，所以需要依赖注入去解决无限层级的对象依赖
            //IOC是一种设计模式，是一种程序设计的目标
            //DI是IOC实现的手段，是一种编码技术
            IMJDContainer container = new MJDContainer();
            //container.Register<IProductService, ProductService>();
            //container.Register<ISqlHelper, SqlHelper>();
            //container.Register<ISqlHelper, SqlHelper>("mysql");      
            //container.Register<ITestServiceB, TestServiceB>();
            //container.Register<ITestServiceB, TestServiceB>("myB");//注册带别名的
            //container.Register<ITestServiceA, TestServiceA>(paraList:new object[]{ 3});
            //container.Register<ITestServiceC, TestServiceC>();
            //依赖抽象
            //ISqlHelper sqlHelper = container.Resolve<ISqlHelper>();
            //方法注入
            //单接口多实现
            //ISqlHelper mysqlHelper = container.Resolve<ISqlHelper>("mysql");
            // IProductService productService = container.Resolve<IProductService>();



            //注入参数是string 或int
            //对象生命周期管理--是否重用对象
            //瞬时、单例、作用域单例Scope

            //container.Register<ITestServiceC, TestServiceC>(lifetimeType: LifetimeType.Singleton);

            //ITestServiceC a = container.Resolve<ITestServiceC>();

            //ITestServiceC b = container.Resolve<ITestServiceC>();

            //var f = object.ReferenceEquals(a,b);



            //瞬时、单例、作用域单例Scope
            //Http请求时，一个请求处理过程中，创建一个实例，不同的请求过程处理，就是不同的实例
            //区分请求，Http请求--Asp.NetCore内置Kestrel,初始化一个容器实例，然后每一次请求Http,就clone一个容器实例，或者就叫做创建子容器（包含注册关系），
            //然后一个请求就是一个子容器实例，那么就可以做到请求单例
            //container.Register<ITestServiceC, TestServiceC>(lifetimeType: LifetimeType.Scope);
            //ITestServiceC a1 = container.Resolve<ITestServiceC>();
            //ITestServiceC a2 = container.Resolve<ITestServiceC>();
            //Console.WriteLine(object.ReferenceEquals(a1,a2));

            //IMJDContainer container1 = container.CreateChildContainer();
            //ITestServiceC a11 = container1.Resolve<ITestServiceC>();
            //ITestServiceC a12 = container1.Resolve<ITestServiceC>();
            //Console.WriteLine(object.ReferenceEquals(a11, a12));

            //IMJDContainer container2 = container.CreateChildContainer();
            //ITestServiceC a21 = container2.Resolve<ITestServiceC>();
            //ITestServiceC a22 = container2.Resolve<ITestServiceC>();
            //Console.WriteLine(object.ReferenceEquals(a21, a22));

            //Console.WriteLine(object.ReferenceEquals(a1, a11));
            //Console.WriteLine(object.ReferenceEquals(a1, a21));
            //Console.WriteLine(object.ReferenceEquals(a11, a21));
            //Console.WriteLine(object.ReferenceEquals(a11, a22));
            //Console.WriteLine(object.ReferenceEquals(a12, a21));
            //Console.WriteLine(object.ReferenceEquals(a12, a22));


            {
                //IOC -AOP
                container.Register<ITestServiceB, TestServiceB>();
                container.Register<ITestServiceC, TestServiceC>();

                ITestServiceC testServiceC = container.Resolve<ITestServiceC>();
                testServiceC.show();
                //testServiceC = (ITestServiceC)CustomAopExtend.AOP(testServiceC, typeof(ITestServiceC));
                //testServiceC.show();
            
            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
