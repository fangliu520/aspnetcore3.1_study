using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
using AppNetCore.Utility.CustomAop;
using AppNetCore.Utility.Validate;
using AppNetCore.Utility.Redis;
using Newtonsoft.Json;

namespace WebCoreTest3._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBConnectionOption _Options;
        private readonly ILogger<HomeController> _logger;
        private IBaseService _baseService;
        private IRedisHelper _redis;
        public HomeController(ILogger<HomeController> logger, IBaseService baseService, IOptionsMonitor<DBConnectionOption> options, IRedisHelper redisHelper)
        {
            Console.WriteLine($"This is {typeof(HomeController)} 初始化构造函数");
            _logger = logger;
            _baseService = baseService;
            _Options = options.CurrentValue;
            _redis = redisHelper;
        }
        private static int _iSet = 0;

        //[ResourceFilter]
        //[ResultFilter]
        //[ActionFilter]
        //[TypeFilter(typeof(ExceptionFilterAttribute))]
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

            //Student mod = new Student();

            //Expression<Func<Student, bool>> expression = e => e.Id == 1 && e.Name.Contains("json") && e.Age > 1;
            //Student mod = _baseService.Get(expression);
            //_baseService = (IBaseService)_baseService.AOP(typeof(IBaseService));//针对整个接口方法进行AOP

            //// var r=_baseService.Get(expression);

            //// mod = r;

            //Student mod = _baseService.GetStudent(1);
            //expression = e => e.Name.Contains("李四") && e.Age > 1;
            //PagedResult<Student> students = _baseService.GetPagedResult(new Student(), expression);

            #region redis 测试
            //_redis.Add("studentPaged", students);

            //PagedResult<Student> modnew = _redis.Get<PagedResult<Student>>("studentPaged");

            //批量插入
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "m_name", "liu1" }, { "m_age", "11" }, { "sex", "男" } };

            _redis.SetAll(dic);


            Dictionary<string, Student> disStudent = new Dictionary<string, Student>() {
                { "stud1", new Student() { Name = "小王", Age = 12 } }  ,
                { "stud2", new Student() { Name = "小王2", Age = 13 } }
            };
            _redis.SetAll(disStudent);
            //批量获取
            var dics = _redis.GetAll<string>(new string[] { "m_name", "m_age" });
            var dicstu = _redis.GetAll<Student>(new string[] { "stud1", "stud2" });
            Console.WriteLine($"Redis 操作成功！");
            _logger.LogError("LogInformation:" + $"Redis 操作成功！" + DateTime.Now.ToString());
            #endregion
            ////Student mod = _baseService.GetStudent(1);
            ////mod.Name = "";// $"{mod.Name}_insertfdsdweddddddddwewwdcf";
            ////mod.Age = 200;

            ////ViewBag.Status = _baseService.InsertStudent(mod);

            //mod.Name = "妞儿";
            //mod.Phone = "1366666111";
            //mod.Email = "22222@QQ.COM";
            ////if (!DataValidateExtend.ValidateModel(mod))
            ////{
            ////    string msg = DataValidateExtend.ErrorMsg;
            ////    Console.WriteLine(msg);
            ////}
            ViewBag.Status = "";//_baseService.InsertStudent(mod);
            ViewBag.Strategy = _Options.strategy.ToString();
            //expression = e => e.Name.Contains("李四") && e.Age > 1;
            //expression=null;// e=>e.Id>1;
            //ViewBag.DataList = _baseService.GetPagedResult(mod,expression);
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

        [HttpPost]
        public IActionResult AddRedis(string content)
        {
            Student stu = new Student()
            {
                Name = "liu",
                Age = 10,
                Email = content
            };


            //_redis.AddItemToList("redis_listlog", JsonConvert.SerializeObject(stu));
            _redis.AddItemToList("redis_listlog", content);


            return Redirect("index");
        }
    }
}
