using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebCoreTest_ELK_Logstash_TCP.Models;

namespace WebCoreTest_ELK_Logstash_TCP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
           

            _logger = logger;
        }
        private static int visitNum = 0;
        public IActionResult Index()
        {
           
            _logger.LogInformation(1, $"127.0.0.1:8002  20:13***这是tcp传送日志接口,访问次数{visitNum++}");
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
