using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppNetCore.Utility.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebCoreTest_ELK_Logstash_Redis.Models;

namespace WebCoreTest_ELK_Logstash_Redis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRedisHelper _redis;

        public HomeController(ILogger<HomeController> logger, IRedisHelper redisHelper)
        {
            _logger = logger;
            _redis = redisHelper;
        }

        public IActionResult Index()
        {
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
                      
            _redis.AddItemToList("redis_listlog", content);


            return Redirect("index");
        }
    }
}
