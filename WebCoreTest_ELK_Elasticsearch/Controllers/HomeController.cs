using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using WebCoreTest_ELK_Elasticsearch.Models;
using AppNetCore.Model;

namespace WebCoreTest_ELK_Elasticsearch.Controllers
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

            string url = "http://127.0.0.1:9200";

            var settings = new ConnectionSettings(uri: new Uri(url)).DefaultIndex("student");
            var client = new ElasticClient(settings);
            List<Student> stu_list = new List<Student>();

            for (int i = 0; i < 10; i++)
            {
                stu_list.Add(new Student() {  Id=i,Name=$"Demo{i}",Age=20+i, CreateTime=DateTime.Now});
            }
            client.IndexMany<Student>(stu_list);
            return View();
        }

        public IActionResult Privacy()
        {
            string url = "http://127.0.0.1:9200";

            var settings = new ConnectionSettings(uri: new Uri(url)).DefaultIndex("student");
            var client = new ElasticClient(settings);
            var searchResult = client.Search<Student>(s=>s.From(0).Size(10)
                                                          .Query(q=>q
                                                                    .Match(m=> m
                                                                               .Field(f=>f.Name)
                                                                               .Query("Demo1"))));

            var r = searchResult.Documents;
            foreach (var item in r)
            {
                Console.WriteLine($"{item.Id}|{item.Name}|{item.Age}");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
