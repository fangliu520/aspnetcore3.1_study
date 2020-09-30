using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppNetCore.Interface.Extend;
using AppNetCore.Model;
using AppNetCore.Service;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebCoreTest3._1.Models;
using WebCoreTest3._1.Utility.Filter;

namespace WebCoreTest3._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBConnectionOption _Options ;
        private readonly ILogger<HomeController> _logger;
        private readonly IBaseService _baseService;

        public HomeController(ILogger<HomeController> logger, IBaseService baseService, IOptionsMonitor<DBConnectionOption> options)
        {
            Console.WriteLine($"This is {typeof(HomeController)} 初始化构造函数");
            _logger = logger;
            _baseService = baseService;
            _Options = options.CurrentValue;
        }
        private static int _iSet = 0;

        [ResourceFilter]
        [ResultFilter]
        [ActionFilter]
        [TypeFilter(typeof(ExceptionFilterAttribute))]
       // [ServiceFilter(typeof(ExceptionFilterAttribute))]//还有一种全局注册到StartUp.cs中
        public IActionResult Index()
        {
            Console.WriteLine($"This is {typeof(HomeController)} Index方法");
            _logger.LogInformation("LogInformation:" + DateTime.Now.ToString());
           // _logger.LogWarning("LogWarning:" + DateTime.Now.ToString());
           // _logger.LogError("LogError:" + DateTime.Now.ToString());
            _logger.LogDebug("LogDebug:" + DateTime.Now.ToString());
            int s = 0;
            //int d = 123 / s;


            #region 获取全部服务
            //string str = "";
            //using (ConsulClient client = new ConsulClient(c =>
            //  {
            //      c.Address = new Uri("http://localhost:8500");
            //      c.Datacenter = "dc1";
            //  }))
            //{
            //    var dic = client.Agent.Services().Result.Response;
            //    foreach (var keyValuesPair in dic)
            //    {
            //        AgentService agentService = keyValuesPair.Value;
            //        str += $"|{agentService.Address}|{agentService.Port}|{agentService.ID}|{agentService.Service}";
            //    }
            //}
            #endregion

            #region consul控制器手动请求控制
            //string content = string.Empty;
            //string url = $"http://webapicore/WeatherForecast";
            //{
            //    Uri uri = new Uri(url);
            //    string groupName = uri.Host;
            //    using (ConsulClient client = new ConsulClient(c =>
            //    {
            //        c.Address = new Uri("http://localhost:8500");
            //        c.Datacenter = "dc1";
            //    }))
            //    {
            //        var dic = client.Agent.Services().Result.Response;
            //        List<KeyValuePair<string,AgentService>> list = dic.Where(k => k.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToList<KeyValuePair<string, AgentService>>();
            //        KeyValuePair<string, AgentService> r = new KeyValuePair<string, AgentService>();
            //        //策略
            //        //随机策略
            //        // r = list[new Random(_iSet++).Next(0, list.Count())];

            //        ////轮询策略
            //        r = list[_iSet++%list.Count()];

            //        ////权重策略
            //        //List<KeyValuePair<string, AgentService>> listPairs = new List<KeyValuePair<string, AgentService>>();
            //        //foreach (var pair in list)
            //        //{
            //        //    int count = int.Parse(pair.Value.Tags?[0]);
            //        //    for (int i = 0; i < count; i++)
            //        //    {
            //        //        listPairs.Add(pair);
            //        //    }
            //        //}
            //        //r = listPairs[new Random().Next(0, listPairs.Count)];


            //        string targetUrl = $"{uri.Scheme}://{r.Value.Address}:{r.Value.Port}{uri.PathAndQuery}";
            //        content = WebApiHelper.InvokeApi(targetUrl);
            //        ViewBag.conn = $"{_baseService.Get()}|{content}|请求路径：{targetUrl}";
            //    }
            //}
            #endregion

            Student mod= _baseService.GetStudent(1);
            mod.Name = "";// $"{mod.Name}_insertfdsdweddddddddwewwdcf";
            mod.Age = 200;

            ViewBag.Status = _baseService.InsertStudent(mod);

            mod.Name = "妞儿";
            ViewBag.Status = _baseService.InsertStudent(mod);
            ViewBag.Strategy = _Options.strategy.ToString();
            return View();
        }

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
