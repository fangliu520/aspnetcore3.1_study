using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApiAuthentication.Utility;

namespace WebApiAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILogger<AuthenticationController> _logger = null;

        private IJWTService _iJWTService = null;

        private readonly IConfiguration _iConfiguration;
        public AuthenticationController(ILoggerFactory factory,ILogger<AuthenticationController> logger,IConfiguration configuration, IJWTService iJWTService)
        {
            _logger = logger;
            _iConfiguration = configuration;
            _iJWTService = iJWTService;
        }

        [Route("Get")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int>() { 1,2,3,4};
        }
        [Route("Login")]
        [HttpGet]
        public string Login(string name, string pwd)
        {

            if ("mjd".Equals(name))//需要获取数据库校验
            {
                string token = this._iJWTService.GetToken(name);
                return JsonConvert.SerializeObject(new { result = true ,token}) ;
            }
            else
            {
                return JsonConvert.SerializeObject(new { result = true, token="" });
            }
        }
    }
}