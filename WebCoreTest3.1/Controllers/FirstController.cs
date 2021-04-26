using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreTest3._1.Models;

namespace WebCoreTest3._1.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;

        private readonly IConfiguration _Configuratin;

        private readonly DbConnectionOptions _optionsCurrent;

        public FirstController(ILogger<FirstController> logger, IConfiguration configuration, IOptions<DbConnectionOptions> options)
        {
            _logger = logger;
            _Configuratin = configuration;
            _optionsCurrent = options.Value;
            _logger.LogWarning("FirstController被构造。。。");
        }

        // GET: FirstController
        public IActionResult Index()
        {
            ViewBag.ParameterInfo = _Configuratin["port"]; 
            ViewBag.Id = _Configuratin["Id"];
            ViewBag.WriteConnection    = _Configuratin["ConnectionString:WriteConnection"]; 
            ViewBag.WriteConnection01 = _Configuratin["ConnectionString:ReadConnectionList:0"];
            ViewBag.WriteConnection02 = _Configuratin["ConnectionString:ReadConnectionList:1"];  
            ViewBag.WriteConnection03 = _Configuratin["ConnectionString:ReadConnectionList:2"];  
            object strResult= Newtonsoft.Json.JsonConvert.SerializeObject(_optionsCurrent); 
            return View(strResult);
        }

        // GET: FirstController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FirstController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FirstController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FirstController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FirstController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FirstController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FirstController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
